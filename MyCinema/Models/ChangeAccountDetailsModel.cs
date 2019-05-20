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

        [Required(ErrorMessage = "Username is required.", AllowEmptyStrings = false)]
        public string Username { get; set; }
    }
}