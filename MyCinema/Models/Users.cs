namespace MyCinema.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public partial class Users
    {

        [Key]
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name Required")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required")]
        [StringLength(50)]
        public string Username { get; set; }


        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string EmailID { get; set; }


        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string Password { get; set; }

        public bool IsEmailVerified { get; set; }

        public Guid ActivationCode { get; set; }

        [StringLength(100)]
        public string ResetPasswordCode { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        
        public string ConfirmPassword { get; set; }
    }
}
