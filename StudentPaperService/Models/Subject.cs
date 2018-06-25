using System.Collections.Generic;

namespace StudentPaperService.Models
{
    public class Subject
    {
        public long SubjectId { get; set; }
        public string Name { get; set; }
        public List<ProfessorSubject> ProfessorSubjects { get; set; }
    }
}
