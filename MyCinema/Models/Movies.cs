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
        public string Name { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public double Price { get; set; }

        public int MinimalAge { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ReleaseDate { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
