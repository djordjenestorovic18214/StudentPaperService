using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentPaperService.Models;
using StudentPaperService.Models.Context;

namespace StudentPaperService.Logic
{
    public class SubjectLogic : ISubjectLogic
    {
        private readonly StudentPaperServiceContext _context;

        public SubjectLogic(StudentPaperServiceContext context)
        {
            _context = context;
        }

        public Subject Delete(long subjectId)
        {
            try
            {
                Subject requestedSubject = _context.Subjects
                    .FirstOrDefault(s => s.SubjectId == subjectId);
                if (requestedSubject != null)
                {
                    _context.Remove(requestedSubject);
                    _context.SaveChanges();
                    return requestedSubject;
                }
                throw new Exception("Subject that you want to delete doesn't exist!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to find subject! Error: {ex.Message}");
            }
        }

        public List<Subject> GetAll()
        {
            try
            {
                return _context.Subjects
                .Include(s => s.ProfessorSubjects)
                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all subjects! Error: {ex.Message}");
            }
        }

        public Subject GetById(long subjectId)
        {
            try
            {
                return _context.Subjects
                        .Include(s => s.ProfessorSubjects)
                        .ToList()
                        .Find(s => s.SubjectId == subjectId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all seminar papers! Error: {ex.Message}");
            }
        }
    }
}
