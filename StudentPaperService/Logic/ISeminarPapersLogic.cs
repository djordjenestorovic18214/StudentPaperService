using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.Logic
{
    public interface ISeminarPapersLogic
    {
        List<SeminarPaper> GetAll();

        SeminarPaper GetById(long seminarPaperId);

        SeminarPaper Delete(long seminarPaperId);

        SeminarPaper Update(SeminarPaper seminarPaper);

        void Insert(SeminarPaper seminarPaper);        
    }
}