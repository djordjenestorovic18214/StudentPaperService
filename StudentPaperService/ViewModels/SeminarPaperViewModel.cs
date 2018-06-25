using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class SeminarPaperViewModel
    {
        public long SeminarPaperId { get; set; }
        public SeminarPaper SeminarPaper { get; set; }
        public List<SeminarPaper> AllSeminarPapers { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public List<Student> AllStudents { get; set; }
        public long ProfessorSubjectId { get; set; }
        public ProfessorSubject ProfessorSubject { get; set; }
        public List<ProfessorSubject> AllProfessorSubjects { get; set; }
    }
}

