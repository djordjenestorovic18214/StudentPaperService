using System.Collections.Generic;

namespace StudentPaperService.Models
{
    public class Student : ApplicationUser
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SeminarPaper> SeminarPapers { get; set; }
        public List<FinalPaper> FinalPapers { get; set; }
    }
}
