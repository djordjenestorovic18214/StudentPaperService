using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class SeminarPaperViewModel
    {
        public long SeminarPaperId { get; set; }
        public SeminarPaper SeminarPaper { get; set; }
        public List<SeminarPaper> AllSeminarPapers { get; set; }
        public Student Student { get; set; }
        public List<Student> AllStudents { get; set; }
        public ProfessorSubject ProfessorSubject { get; set; }
        public List<Professor> AllProfessors { get; set; }
        public List<Subject> AllSubjects { get; set; }
    }
}

