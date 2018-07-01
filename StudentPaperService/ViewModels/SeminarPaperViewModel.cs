using Microsoft.AspNetCore.Http;
using StudentPaperService.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPaperService.ViewModels
{
    public class SeminarPaperViewModel
    {
        [Required(ErrorMessage = "Унесите тему")]
        [Display(Name = "Тема")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Тема треба да има између 10 и 200 карактера")]
        public string Name{ get; set; }
        [Range(1, 3000, ErrorMessage = "Изаберите предмет")]
        [Required(ErrorMessage = "Изаберите предмет")]
        public long SubjectId { get; set; }
        [Range(1,3000, ErrorMessage = "Изаберите ментора")]
        [Required(ErrorMessage = "Изаберите ментора")]
        public string ProfessorId { get; set; }


        public List<Professor> AllProfessors { get; set; }
        public List<Subject> AllSubjects { get; set; }

        public SeminarPaper SeminarPaper { get; set; }
        
        public List<SeminarPaper> AllSeminarPapers { get; set; }
        public Student Student { get; set; }
        public ProfessorSubject ProfessorSubject { get; set; }
        public Models.IFormFile File { get; set; }
    }
}

