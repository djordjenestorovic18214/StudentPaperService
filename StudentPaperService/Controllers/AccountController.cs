using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentPaperService.Models;
using StudentPaperService.Models.AccountViewModels;
using StudentPaperService.Models.Context;
using StudentPaperService.Services;
using StudentPaperService.ViewModels;

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
            RegisterProfessorViewModel rpvm = new RegisterProfessorViewModel();
            rpvm.Subjects = _context.Subjects.ToList();
            return View("RegisterProfessor", rpvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(RegisterStudentViewModel model, string returnUrl = null)
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
        public async Task<IActionResult> RegisterProfessor(RegisterProfessorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Professor()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.UserName,


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

                    for (int i = 0; i < model.SubjectsIds.Length; i++)
                    {
                        model.Subjects.Add(_context.Subjects.SingleOrDefault(s => s.SubjectId == model.SubjectsIds[i]));
                    }

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
