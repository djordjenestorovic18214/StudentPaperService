using StudentPaperService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPaperService.Logic
{
    public interface IProfessorLogic
    {
        List<Professor> GetAll();

        Professor GetById(long professorId);

        Professor Delete(long professorId);

        //Professor Update(Professor professor);

        //void Insert(Professor professor);
    }
}
