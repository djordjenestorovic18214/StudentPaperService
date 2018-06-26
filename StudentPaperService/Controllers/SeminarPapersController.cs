using Microsoft.AspNetCore.Mvc;
using StudentPaperService.Logic;
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


