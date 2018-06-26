using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.Logic
{
    public interface IProfessorSubjectLogic
    {
        List<ProfessorSubject> GetByProfessorId(string professorId);

        List<ProfessorSubject> GetBySubjectId(int subjectId);

        ProfessorSubject GetOne(string professorId, int subjectId);

        ProfessorSubject Insert(string professorId, int subjectId);

        ProfessorSubject Delete(string professorId, int subjectId);
    }
}
