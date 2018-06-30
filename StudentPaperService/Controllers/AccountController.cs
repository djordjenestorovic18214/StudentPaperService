using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentPaperService.Logic;
using StudentPaperService.Models;
using StudentPaperService.Models.AccountViewModels;
using StudentPaperService.Models.Context;
using StudentPaperService.Services;

namespace StudentPaperService.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly StudentPaperServiceContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger, StudentPaperServiceContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    ModelState.AddModelError("key", "Username doesn't exist!");
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Users()
        {
            UsersViewModel uvm = new UsersViewModel();
            uvm.AllProfessors = _context.Professors.Include(p => p.ProfessorSubjects).Include(p => p.FinalPapers).ToList();
            uvm.AllStudents = _context.Students.Include(s => s.SeminarPapers).Include(s => s.FinalPapers).ToList();

            return View("Users", uvm);
        }

        [HttpGet]
        public IActionResult RegisterStudent()
        {
            return View("RegisterStudent");
        }

        [HttpGet]
        public IActionResult RegisterProfessor()
        {
            ProfessorViewModel rpvm = new ProfessorViewModel();
            rpvm.Subjects = _context.Subjects.ToList();
            return View("RegisterProfessor", rpvm);
        }

        [HttpGet("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            StudentLogic studentLogic = new StudentLogic(_context);
            StudentViewModel svm = new StudentViewModel();
            Student s = studentLogic.GetById(id);
            svm.Student = s;

            return View("DeleteStudent", svm);
        }
        
        [HttpPost("{id}")]
        public IActionResult DeleteStudent(string id, string returnUrl = null)
        {
            SeminarPapersLogic seminarPapersLogic = new SeminarPapersLogic(_context);
            //FinalPapersLogic finalPapersLogic = new FinalPapersLogic(_context);
            StudentLogic studentLogic = new StudentLogic(_context);
            Student s = studentLogic.GetById(id);
            
            studentLogic.Delete(s.Id);
            s.SeminarPapers.ForEach(sp => seminarPapersLogic.Delete(sp.SeminarPaperId));
            //s.FinalPapers.ForEach(sp => finalPapersLogic.Delete(sp.FinalPaperId));

            return RedirectToAction("Users");
        }

        [HttpGet("{id}")]
        public IActionResult DeleteProfessor(string id)
        {
            ProfessorLogic professorLogic = new ProfessorLogic(_context);
            ProfessorViewModel pvm = new ProfessorViewModel();
            Professor p = professorLogic.GetById(id);
            pvm.Professor = p;

            return View("DeleteProfessor", pvm);
        }
        
        [HttpPost("{id}")]
        public IActionResult DeleteProfessor(string id, string returnUrl = null)
        {
            SeminarPapersLogic seminarPapersLogic = new SeminarPapersLogic(_context);
            ProfessorSubjectLogic professorSubjectLogic = new ProfessorSubjectLogic(_context);
            //FinalPapersLogic finalPapersLogic = new FinalPapersLogic(_context);
            ProfessorLogic professorLogic = new ProfessorLogic(_context);
            Professor p = professorLogic.GetById(id);
            List<ProfessorSubject> ps = professorSubjectLogic.GetByProfessorId(id).ToList();
            p.ProfessorSubjects = ps;

            ps.ToList().ForEach(pp => pp.SeminarPapers.ToList().ForEach(sp2 => seminarPapersLogic.Delete(sp2.SeminarPaperId)));
            ps.ToList().ForEach(p1 => professorSubjectLogic.Delete(p1.ProfessorId, p1.SubjectId));
            professorLogic.Delete(p.Id);
            //p.FinalPapers.ForEach(fp => finalPapersLogic.Delete(fp.FinalPaperId));

            return RedirectToAction("Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(StudentViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IndexNumber = model.IndexNumber,
                    Email = model.Email,
                    UserName = model.UserName

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("You have created a new account with role student.");


                    IdentityRole userRole = _context.Roles.SingleOrDefault(r => r.Name.ToLower().Equals("student"));

                    _context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = userRole.Id, UserId = user.Id });
                    _context.SaveChanges();

                    return View("RegisterStudentSuccessful", model);
                }

                return View(model);
            }
            return View("./Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterProfessor(ProfessorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Professor()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("You have created a new account with role professor.");

                    IdentityRole userRole = _context.Roles.SingleOrDefault(r => r.Name.ToLower().Equals("profesor"));

                    _context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = userRole.Id, UserId = user.Id });

                    for (int i = 0; i < model.SubjectsIds.Length; i++)
                    {
                        _context.ProfessorSubjects.Add(new ProfessorSubject() { ProfessorId = user.Id, SubjectId = model.SubjectsIds[i] });
                    }

                    _context.SaveChanges();

                    model.Subjects = new List<Subject>();

                    model.SubjectsIds.ToList().ForEach(su => model.Subjects.Add(_context.Subjects.SingleOrDefault(s => s.SubjectId == su)));

                    return View("RegisterProfessorSuccessful", model);
                }
                //TODO: hendlaj greske
                return View(model);
            }
            return View("./Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
