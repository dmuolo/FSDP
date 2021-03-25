using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FSDP.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* First Name is required. *")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Last Name is required. *")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* Email is required. *")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Subject is required. *")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* A message is required. *")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}