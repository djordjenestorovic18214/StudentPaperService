using Microsoft.EntityFrameworkCore;
using StudentPaperService.Models;
using StudentPaperService.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentPaperService.Logic
{
    public class SeminarPapersLogic : ISeminarPapersLogic
    {
        private readonly StudentPaperServiceContext _context;

        public SeminarPapersLogic()
        {
            _context = new StudentPaperServiceContext(new DbContextOptions<StudentPaperServiceContext>());
        }

        public SeminarPapersLogic(StudentPaperServiceContext context)
        {
            _context = context;
        }

        public List<SeminarPaper> GetAll()
        {
            try
            {
                //return _context.SeminarPapers
                //    .Include(s => s.ProfessorSubject)
                //    //.Include(s => s.Student)
                //    .ToList();
                ProfessorSubject ps = new ProfessorSubject() { Professor = new Professor() { FirstName = "Душан Бараћ" }, Subject = new Subject() { Name = "Интернет технологије" } };
                Student s1 = new Student() { FirstName = "Валентина", LastName = "Љубисављевић", IndexNumber = "0095/2014" };
                Student s2 = new Student() { FirstName = "Ђорђе", LastName = "Несторовић", IndexNumber = "0182/2014" };
                SeminarPaper sp1 = new SeminarPaper() { Name = "Студентски сервис за радове", PublishDate = DateTime.Now, ProfessorSubject = ps, Student = s1 };
                SeminarPaper sp2 = new SeminarPaper() { Name = "Студентски сервис за радове", PublishDate = DateTime.Now, ProfessorSubject = ps, Student = s2 };
                List<SeminarPaper> list = new List<SeminarPaper>();
                list.Add(sp1);
                list.Add(sp2);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all seminar papers! Error: {ex.Message}");
            }
        }

        public SeminarPaper Delete(int seminarPaperId)
        {
            try
            {
                var requestedPaper = _context.SeminarPapers
                    .Include(s => s.ProfessorSubject)
                    //.Include(s => s.Student)                    
                    .FirstOrDefault(s => s.SeminarPaperId == seminarPaperId);

                return requestedPaper;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while finding paper with Id : {seminarPaperId}! Error: {ex.Message}");
            }
        }

        public SeminarPaper GetById(int seminarPaperId)
        {
            throw new NotImplementedException();
        }

        public void Insert(SeminarPaper seminarPaper, byte[] seminarPaperFile)
        {
            throw new NotImplementedException();
        }

        public SeminarPaper Update(SeminarPaper odrednica)
        {
            throw new NotImplementedException();
        }
    }
}
