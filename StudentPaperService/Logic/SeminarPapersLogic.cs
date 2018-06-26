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
                List<SeminarPaper> lista = _context.SeminarPapers
                    .Include(s => s.ProfessorSubject)
                    .Include(s => s.Student)
                    .ToList();
                
                foreach(SeminarPaper sp in lista)
                {
                    sp.ProfessorSubject.Professor = _context.Professors.SingleOrDefault(p => p.Id.Equals(sp.ProfessorSubject.ProfessorId));
                    sp.ProfessorSubject.Subject = _context.Subjects.SingleOrDefault(s => s.SubjectId == sp.ProfessorSubject.SubjectId);
                }
                return lista;

            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all seminar papers! Error: {ex.Message}");
            }
        }

        public SeminarPaper GetById(long seminarPaperId)
        {
            try
            {
                List<SeminarPaper> lista = _context.SeminarPapers
                    .Include(s => s.ProfessorSubject)
                    .Include(s => s.Student)
                    .ToList();

                foreach (SeminarPaper sp in lista)
                {
                    sp.ProfessorSubject.Professor = _context.Professors.SingleOrDefault(p => p.Id.Equals(sp.ProfessorSubject.ProfessorId));
                    sp.ProfessorSubject.Subject = _context.Subjects.SingleOrDefault(s => s.SubjectId == sp.ProfessorSubject.SubjectId);
                }

                return lista.Find(s => s.SeminarPaperId == seminarPaperId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all seminar papers! Error: {ex.Message}");
            }
        }

        public void Insert(SeminarPaper seminarPaper, byte[] seminarPaperFile)
        {
            throw new NotImplementedException();
        }

        public SeminarPaper Update(SeminarPaper odrednica)
        {
            throw new NotImplementedException();
        }

        public SeminarPaper Delete(long seminarPaperId)
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
    }
}
