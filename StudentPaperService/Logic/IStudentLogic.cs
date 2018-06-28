using StudentPaperService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPaperService.Logic
{
    public interface IStudentLogic
    {
        List<Student> GetAll();

        Student GetById(long studentId);

        Student Delete(long studentId);

        //Student Update(Student student);

        //void Insert(Student student);
    }
}
