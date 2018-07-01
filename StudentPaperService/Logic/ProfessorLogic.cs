using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentPaperService.Models;
using StudentPaperService.Models.Context;

namespace StudentPaperService.Logic
{
    public class ProfessorLogic : IProfessorLogic
    {
        private readonly StudentPaperServiceContext _context;

        public ProfessorLogic(StudentPaperServiceContext context)
        {
            _context = context;
        }

        public Professor Delete(string professorId)
        {
            try
            {
                Professor requestedProfessor = _context.Professors                    
                    .FirstOrDefault(p => p.Id.Equals(professorId));
                if (requestedProfessor != null)
                {
                    _context.Remove(requestedProfessor);
                    _context.SaveChanges();
                    return requestedProfessor;
                }
                throw new Exception("Professor that you want to delete doesn't exist!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete professor! Error: {ex.Message}");
            }
        }

        public List<Professor> GetAll()
        {
            try
            {
                return _context.Professors
                .Include(p => p.FinalPapers)
                .Include(p => p.ProfessorSubjects)
                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot return all professors! Error: {ex.Message}");
            }            
        }

        public Professor GetById(string professorId)
        {
            try
            {
                return _context.Professors
                    .Include(prof => prof.FinalPapers)
                    .Include(prof => prof.ProfessorSubjects)                    
                    .ToList()
                    .Find(prof => prof.Id.Equals(professorId));                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to find professor! Error: {ex.Message}");
            }
        }
    }
}
