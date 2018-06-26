using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPaperService.Logic;
using StudentPaperService.Models;
using StudentPaperService.Models.Context;
using StudentPaperService.ViewModels;
using System;

namespace StudentPaperService.Controllers
{
    [Route("papers/seminar")]
    public class SeminarPapersController : Controller
    {
        private readonly StudentPaperServiceContext _context;

        public SeminarPapersController(StudentPaperServiceContext context)
        {
            _context = context;
        }

        [HttpGet]        
        public IActionResult Index()
        {
            try
            {
                ISeminarPapersLogic seminarPaperLogic = new SeminarPapersLogic(_context);
                SeminarPaperViewModel spvm = new SeminarPaperViewModel();
                spvm.AllSeminarPapers = seminarPaperLogic.GetAll();
                return View(spvm);
            }
            catch (Exception ex)
            {
                //TODO: Hendlaj gresku
                return View();
            }
        }

        [HttpGet("create")]
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

        [HttpGet("delete/{seminarPaperId}")]
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


