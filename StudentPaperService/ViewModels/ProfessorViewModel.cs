using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class ProfessorViewModel
    {
        public long ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public List<FinalPaper> FinalPapers { get; set; }
        public List<ProfessorSubject> AllProfessorSubjects { get; set; }
    }
}