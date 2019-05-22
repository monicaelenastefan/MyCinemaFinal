namespace MyCinema.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movies
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required]
       
        public byte[] Image { get; set; }

        [Required]
        [Range(1,100)]
     
        public double Price { get; set; }


        [Required]
       

        [Display(Name = "Duration")]
       // [DataType(DataType.Duration)]
        public string Duration { get; set; }
    }
}
