﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPaperService.Models.AccountViewModels
{
    public class StudentViewModel
    {
        public Student Student{ get; set; }

        [Required(ErrorMessage = "Унесите име")]
        [Display(Name = "Име")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Име треба да има између 3 и 20 карактера")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Унесите  презиме")]
        [Display(Name = "Презиме")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Презиме треба да има између 3 и 20 карактера")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Унесите број индекса")]
        [Display(Name = "Број индекса")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Унесите број индекса у формату број индекса/година (нпр. 0021/2014)")]
        public string IndexNumber { get; set; }

        [Required(ErrorMessage = "Унесите е-адресу")]
        [EmailAddress(ErrorMessage = "Унесите исправну е-адресу")]
        [Display(Name = "Е-адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Унесите корисничко име")]
        [Display(Name = "Корисничко име")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Корисничко име треба да има између 7 и 20 карактера")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Унесите шифру")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Шифра треба да има између 6 и 100 карактера")]
        [DataType(DataType.Password)]
        [Display(Name = "Шифра")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди шифру")]
        [Compare("Password", ErrorMessage = "Шифре се не поклапају")]
        public string ConfirmPassword { get; set; }
    }
}
