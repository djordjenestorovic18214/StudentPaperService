using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.Logic
{
    public interface IProfessorSubjectLogic
    {
        List<ProfessorSubject> GetByProfessorId(string professorId);

        List<ProfessorSubject> GetBySubjectId(long subjectId);

        ProfessorSubject GetOne(string professorId, long subjectId);

        ProfessorSubject Insert(string professorId, long subjectId);

        ProfessorSubject Delete(string professorId, long subjectId);
    }
}
