using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.Logic
{
    public interface ISubjectLogic
    {
        List<Subject> GetAll();

        Subject GetById(long subjectId);

        Subject Delete(long subjectId);

        //Subject Update(Subject subject);

        //void Insert(Subject subject);
    }
}
