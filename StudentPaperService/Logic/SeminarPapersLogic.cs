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

        public SeminarPaper Update(SeminarPaper seminarPaper)
        {
            throw new NotImplementedException();
        }

        public SeminarPaper Delete(long seminarPaperId)
        {
            try
            {
                SeminarPaper requestedPaper = _context.SeminarPapers                                   
                    .FirstOrDefault(s => s.SeminarPaperId == seminarPaperId);
                if(requestedPaper != null)
                {
                    _context.Remove(requestedPaper);
                    _context.SaveChanges();
                    return requestedPaper;
                }                
                throw new Exception("Paper that you want to delete doesn't exist!");               
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to find paper! Error: {ex.Message}");
            }
        }
    }
}
