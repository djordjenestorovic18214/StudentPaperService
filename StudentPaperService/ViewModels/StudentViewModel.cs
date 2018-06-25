using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class StudentViewModel
    {
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public List<SeminarPaper> SeminarPapers { get; set; }
        public List<FinalPaper> FinalPapers { get; set; }
        public List<SeminarPaper> AllSeminarPapers { get; set; }
        public List<FinalPaper> AllFinalPapers { get; set; }
    }
}
