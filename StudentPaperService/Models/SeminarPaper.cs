using System;

namespace StudentPaperService.Models
{
    public class SeminarPaper
    {
        public long SeminarPaperId { get; set; }
        public string Name { get; set; }
        public byte[] PaperFile { get; set; }
        public DateTime PublishDate { get; set; }
        public Student Student { get; set; }        
        public ProfessorSubject ProfessorSubject { get; set; }
    }
}
