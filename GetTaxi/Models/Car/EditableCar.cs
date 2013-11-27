using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Car
{
    public class EditableCar
    {
        public int? CarId { get; set; }
        [Required]
        public string Mark { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int? NrOfSeats { get; set; }
        public string DriverName { get; set; }
        public string CarNumber { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}