using System.Collections.Generic;

namespace StudentPaperService.Models
{
    public class ProfessorSubject
    {
        public string ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<SeminarPaper> SeminarPapers { get; set; }
    }
}
