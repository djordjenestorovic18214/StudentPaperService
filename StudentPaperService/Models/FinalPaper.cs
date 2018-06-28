using System;

namespace StudentPaperService.Models
{
    public class FinalPaper
    {
        public long FinalPaperId { get; set; }
        public string Name { get; set; }
        public byte[] PaperFile { get; set; }
        public DateTime PublishDate { get; set; }
        public long FinalPaperTypeId { get; set; }
        public FinalPaperType FinalPaperType { get; set; }
        public Student Student { get; set; }
        public Professor Mentor { get; set; }
    }
}
