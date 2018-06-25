using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class FinalPaperViewModel
    {
        public long FinalPaperId { get; set; }
        public FinalPaper FinalPaper { get; set; }
        public List<FinalPaper> AllFinalPapers { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public List<Student> AllStudents { get; set; }
        public long ProfessortId { get; set; }
        public Professor Professor { get; set; }
        public List<Professor> AllProfessors { get; set; }
    }
}
