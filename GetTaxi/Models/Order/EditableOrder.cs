using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Order
{
    public class EditableOrder
    {
        public int CityFrom { get; set; }
        [Required]
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public int PlannedDay { get; set; }
        public int PlannedHour { get; set; }
        public int PlannedMinute { get; set; }
        public bool Childer { get; set; }
        public bool NoSmoking { get; set; }
        public bool Animal { get; set; }
        public bool English { get; set; }
        public bool Card { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }

        public List<int> Companies { get; set; }
    }
}