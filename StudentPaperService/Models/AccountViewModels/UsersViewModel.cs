using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPaperService.Models.AccountViewModels
{
    public class UsersViewModel
    {
        public List<Professor> AllProfessors { get; set; }
        public List<Student> AllStudents { get; set; }
    }
}
