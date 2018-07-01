using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentPaperService.Logic;
using StudentPaperService.Models;
using StudentPaperService.Models.Context;
using StudentPaperService.Services;
using StudentPaperService.ViewModels;
using System;
using System.Collections.Generic;

namespace StudentPaperService.Controllers
{
    [Authorize(Roles = "admin,student,profesor")]
    [Route("[controller]/[action]")]
    public class SeminarPapersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly StudentPaperServiceContext _context;

        public SeminarPapersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger, StudentPaperServiceContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                ISeminarPapersLogic seminarPaperLogic = new SeminarPapersLogic(_context);
                SeminarPaperViewModel spvm = new SeminarPaperViewModel();
                List<SeminarPaper> papers = seminarPaperLogic.GetAll();
                List<SeminarPaper> papersToShow = new List<SeminarPaper>();

                if (User.IsInRole("admin"))
                    papersToShow = papers;

                else if (User.IsInRole("profesor"))
                    papersToShow = papers.FindAll(p => p.ProfessorSubject.ProfessorId.Equals(_userManager.GetUserId(User)));

                else if (User.IsInRole("student"))
                    papersToShow = papers.FindAll(p => p.Student.Id.Equals(_userManager.GetUserId(User)));

                spvm.AllSeminarPapers = papersToShow;
                return View(spvm);
            }
            catch (Exception ex)
            {
                //TODO: Hendlaj gresku
                return View();
            }
        }

        [Authorize(Roles = "student")]
        public IActionResult Insert()
        {
            try
            {
                ISeminarPapersLogic seminarPaperLogic = new SeminarPapersLogic(_context);
                ISubjectLogic subjectLogic = new SubjectLogic(_context);
                IProfessorLogic professorLogic = new ProfessorLogic(_context);
                IProfessorSubjectLogic professorSubjectLogic = new ProfessorSubjectLogic(_context);
                SeminarPaperViewModel spvm = new SeminarPaperViewModel();
                spvm.AllProfessors = professorLogic.GetAll();
                spvm.AllSubjects = subjectLogic.GetAll();
                return View(spvm);
            }
            catch (Exception ex)
            {
                //TODO: Hendlaj gresku
                return View();
            }
        }

        [Authorize(Roles = "student")]
        [HttpPost]
        public IActionResult Insert(SeminarPaperViewModel seminarPaperViewModel, byte[] paperFile)
        {
            ISeminarPapersLogic seminarPaperLogic = new SeminarPapersLogic(_context);
            IProfessorLogic professorLogic = new ProfessorLogic(_context);
            ISubjectLogic subjectLogic = new SubjectLogic(_context);
            IProfessorSubjectLogic professorSubjectLogic = new ProfessorSubjectLogic(_context);

            seminarPaperViewModel.SeminarPaper.PublishDate = DateTime.Now;
            SeminarPaperViewModel spvm = new SeminarPaperViewModel();
            spvm.SeminarPaper = new SeminarPaper();

            try
            {
                if (!ModelState.IsValid)
                {
                    spvm.AllProfessors = professorLogic.GetAll();
                    spvm.AllSubjects = subjectLogic.GetAll();
                    spvm.SeminarPaper.Name = seminarPaperViewModel.SeminarPaper.Name ?? "";
                    spvm.SeminarPaper.PaperFile = paperFile;
                    return View("Insert", spvm);
                }
                seminarPaperLogic.Insert(seminarPaperViewModel.SeminarPaper, paperFile);
                return RedirectToAction("Index", "SeminarPapers");
            }
            catch (Exception ex)
            {
                spvm.AllProfessors = professorLogic.GetAll();
                spvm.AllSubjects = subjectLogic.GetAll();
                spvm.SeminarPaper.Name = seminarPaperViewModel.SeminarPaper.Name ?? "";
                spvm.SeminarPaper.PaperFile = paperFile;
                return View("Insert", spvm);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            try
            {
                ISeminarPapersLogic seminarPaperLogic = new SeminarPapersLogic(_context);
                SeminarPaperViewModel spvm = new SeminarPaperViewModel();

                spvm.SeminarPaper = seminarPaperLogic.GetById(id);
                return View(spvm);
            }
            catch (Exception ex)
            {
                //TODO: Hendlaj gresku
                return View();
            }
        }

        [HttpGet("{seminarPaperId}")]
        public IActionResult Delete(int seminarPaperId)
        {
            try
            {
                ISeminarPapersLogic seminarPaperLogic = new SeminarPapersLogic(_context);
                seminarPaperLogic.Delete(seminarPaperId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}


