using System.Collections.Generic;

namespace StudentPaperService.Models
{
    public class Professor : ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<FinalPaper> FinalPapers { get; set; }
        public List<ProfessorSubject> ProfessorSubjects { get; set; }        
    }
}
