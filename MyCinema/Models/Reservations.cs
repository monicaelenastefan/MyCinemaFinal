namespace MyCinema.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservations
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Movie { get; set; }

        [Required]
        [StringLength(50)]
        public string Room { get; set; }

        [Column(TypeName = "date")]
        public DateTime Day { get; set; }

        public TimeSpan Hour { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public double Price { get; set; }
    }
}
