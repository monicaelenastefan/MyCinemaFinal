namespace MyCinema.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rooms
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        [Required]
        public int NrOfSeats { get; set; }


    }
}
