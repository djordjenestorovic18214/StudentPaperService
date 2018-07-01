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

        Professor GetById(string professorId);

        Professor Delete(string professorId);

        //Professor Update(Professor professor);

        //void Insert(Professor professor);
    }
}
