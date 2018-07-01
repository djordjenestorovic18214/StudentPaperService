using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentPaperService.Models;
using StudentPaperService.Models.Context;

namespace StudentPaperService.Logic
{
    public class ProfessorSubjectLogic : IProfessorSubjectLogic
    {
        private readonly StudentPaperServiceContext _context;

        public ProfessorSubjectLogic(StudentPaperServiceContext context)
        {
            _context = context;
        }

        public ProfessorSubject Delete(string professorId, long subjectId)
        {
            try
            {
                var professorSubject = _context.ProfessorSubjects.Find(professorId, subjectId);
                _context.ProfessorSubjects.Remove(professorSubject);
                _context.SaveChanges();

                return professorSubject;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete relation between professor and subject! Error: {ex.Message}");
            }
        }

        public List<ProfessorSubject> GetByProfessorId(string professorId)
        {
            try
            {
                return _context.ProfessorSubjects
                    .Include(ps => ps.Professor)
                    .Include(ps => ps.Subject)
                    .Include(ps => ps.SeminarPapers)
                    .Where(ps => ps.ProfessorId.Equals(professorId))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to find ProfessorSubject! Error: {ex.Message}");
            }
        }

        public List<ProfessorSubject> GetBySubjectId(long subjectId)
        {
            try
            {
                return _context.ProfessorSubjects
                    .Include(ps => ps.Subject)
                    .Include(ps => ps.Professor)
                    .Include(ps => ps.SeminarPapers)
                    .Where(ps => ps.SubjectId == subjectId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to find ProfessorSubject! Error: {ex.Message}");
            }
        }

        public ProfessorSubject GetOne(string professorId, long subjectId)
        {
            try
            {
                var professor = _context.Professors.Find(professorId);
                var subject = _context.Subjects.Find(subjectId);

                if (professor != null && subject != null)
                {
                    return _context.ProfessorSubjects
                    .Include(ps => ps.SeminarPapers)
                    .Include(ps => ps.Subject)
                    .Include(ps => ps.Professor)
                    .ToList()
                    .SingleOrDefault(ps => ps.ProfessorId.Equals(professorId) && ps.SubjectId == subjectId);
                }
                else
                {
                    throw new Exception($"Professor or subject that you have choosed doesn't exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to find ProfessorSubject! Error: {ex.Message}");
            }
        }

        public ProfessorSubject Insert(string professorId, long subjectId)
        {
            try
            {
                var professor = _context.Professors.Find(professorId);
                var subject = _context.Subjects.Find(subjectId);

                if (professor != null && subject != null)
                {
                    _context.ProfessorSubjects.Add(new ProfessorSubject()
                    {
                        ProfessorId = professorId,
                        Professor = professor,
                        SubjectId = subjectId,
                        Subject = subject
                    });
                    _context.SaveChanges();

                    return GetOne(professorId, subjectId);
                }
                else
                {
                    throw new Exception($"Professor or subject that you have choosed doesn't exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to create relation between professor and subject!");
            }
        }
    }
}
