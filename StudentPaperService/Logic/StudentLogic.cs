using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentPaperService.Models;
using StudentPaperService.Models.Context;

namespace StudentPaperService.Logic
{
    public class StudentLogic : IStudentLogic
    {
        private readonly StudentPaperServiceContext _context;

        public StudentLogic(StudentPaperServiceContext context)
        {
            _context = context;
        }

        public Student Delete(string studentId)
        {
            try
            {
                Student requestedStudent = _context.Students
                    .FirstOrDefault(p => p.Id.Equals(studentId));
                if (requestedStudent != null)
                {
                    _context.Remove(requestedStudent);
                    _context.SaveChanges();
                    return requestedStudent;
                }
                throw new Exception("Student that you want to delete doesn't exist!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to find student! Error: {ex.Message}");
            }
        }

        public List<Student> GetAll()
        {
            try
            {
                return _context.Students
                .Include(s => s.FinalPapers)
                .Include(s => s.SeminarPapers)
                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all seminar students! Error: {ex.Message}");
            }
        }

        public Student GetById(string studentId)
        {
            try
            {
                return _context.Students
                    .Include(s => s.FinalPapers)
                    .Include(s => s.SeminarPapers)
                    .ToList()
                    .Find(s => s.Id.Equals(studentId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all seminar papers! Error: {ex.Message}");
            }
        }
    }
}

