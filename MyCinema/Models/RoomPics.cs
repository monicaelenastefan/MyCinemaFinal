using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyCinema.Models
{
    public class RoomPics
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public String ImageUrl { get; set; }

    }
}