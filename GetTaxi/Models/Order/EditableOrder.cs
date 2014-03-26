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
        [Required(ErrorMessage = "Skąd mamy Cię odebrać?")]
        [MaxLength(150, ErrorMessage = "Proszę spróbować skrócić adres")]
        public string AddressFrom { get; set; }
        [MaxLength(10, ErrorMessage = "Nieprawidłowy numer bloku")]
        public string AddressFromBlock { get; set; }
        [MaxLength(10, ErrorMessage = "Nieprawidłowy numer klatki")]
        public string AddressFromPorch { get; set; }
        [MaxLength(150, ErrorMessage = "Proszę spróbować skrócić adres")]
        public string AddressTo { get; set; }
        [MaxLength(10, ErrorMessage = "Nieprawidłowy numer bloku")]
        public string AddressToBlock { get; set; }
        [MaxLength(10, ErrorMessage = "Nieprawidłowy numer klatki")]
        public string AddressToPorch { get; set; }
        public int PlannedDay { get; set; }
        public int PlannedHour { get; set; }
        public int PlannedMinute { get; set; }
        public bool Childer { get; set; }
        public bool NoSmoking { get; set; }
        public bool Animal { get; set; }
        public bool English { get; set; }
        public bool Card { get; set; }
        [MaxLength(400, ErrorMessage = "Proszę skrócić komentarz")]
        public string Comment { get; set; }
        public string Name { get; set; }

        public List<int> Companies { get; set; }
    }
}