using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.Logic
{
    public interface ISeminarPapersLogic
    {
        List<SeminarPaper> GetAll();

        SeminarPaper GetById(int seminarPaperId);

        SeminarPaper Delete(int seminarPaperId);

        SeminarPaper Update(SeminarPaper odrednica);

        void Insert(SeminarPaper seminarPaper, byte[] seminarPaperFile);

        //void CheckOdrednice(OdrednicaViewModel odrednicaViewModel, int[] izabraneOdrednice);
    }
}