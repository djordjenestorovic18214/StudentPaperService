using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class SubjectViewModel
    {
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<ProfessorSubject> AllProfessorSubjects { get; set; }
    }
}
