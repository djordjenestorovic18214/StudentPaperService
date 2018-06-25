using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class ProfessorSubjectViewModel
    {
        public long ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<SeminarPaper> SeminarPapers { get; set; }
    }
}