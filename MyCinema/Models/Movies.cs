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

        [DataType(DataType.ImageUrl)]
        public String Image { get; set; }

        [Required]
        [Range(1,100)]
     
        public double Price { get; set; }

        [Required]
        [Range(1,22)]
        public int MinimalAge { get; set; }

        [Required]
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        [Required]
        [Display(Name = "Duration")]
        [DataType(DataType.Duration)]
        public TimeSpan Duration { get; set; }
    }
}
