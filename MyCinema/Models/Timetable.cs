using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCinema.Models
{
    public class Timetable
    {

        [Key]
        public int id { get; set; }

        [Required]
        public int MovieId { get; set; } //  FOREIGN KEY (MovieId) REFERENCES Movies(Id)

        [Required]
        public int RoomId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public TimeSpan StartTime { get; set; }

        public int[,] Matrix { get; set; }

     
    }

}
