using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyCinema.Models
{
    public class ChangeAccountDetailsModel
    {

        [Required(ErrorMessage = "First Name is required", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.", AllowEmptyStrings = false)]
        public string LastName { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string NewPassword { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
    }
}