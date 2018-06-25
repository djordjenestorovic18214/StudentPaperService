using StudentPaperService.Models;
using System.Collections.Generic;

namespace StudentPaperService.ViewModels
{
    public class FinalPaperTypeViewModel
    {
        public long FinalPaperTypeId { get; set; }
        public FinalPaperType FinalPaperType { get; set; }
        public List<FinalPaperType> AllFinalPaperTypes { get; set; }
    }
}
