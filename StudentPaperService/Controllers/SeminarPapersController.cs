using Microsoft.AspNetCore.Mvc;
using StudentPaperService.Logic;
using StudentPaperService.Models.Context;
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
                return View(seminarPaperLogic.GetAll());
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


