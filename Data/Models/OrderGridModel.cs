using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderGridModel
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public int ClientId { get; set; }
        [ScaffoldColumn(false)]
        public int Status { get; set; }
        [Display(Name = "Utworzono")]
        public System.DateTime TimeCreated { get; set; }
        [Display(Name = "Adres początkowy")]
        public string AddressFrom { get; set; }
        [Display(Name = "Adres końcowy")]
        public string AddressTo { get; set; }
        [Display(Name = "Zgłaszający")]
        public string Client { get; set; }
        [Display(Name = "Szacowana cena")]
        public double? EstimatedPrice { get; set; }
        [Display(Name = "Priorytet")]
        public int Priority { get; set; }

        [Display(Name = "Status")]
        public string StatusName
        {
            get
            {
                string statusName;
                switch (Status)
                {
                    case 0:
                        statusName = "Nowe";
                        break;
                    case 1:
                        statusName = "Przypisane";
                        break;
                    case 2:
                        statusName = "Taxi na miejscu";
                        break;
                    case 3:
                        statusName = "Klient w Taxi";
                        break;
                    case 4:
                        statusName = "Zakończone";
                        break;
                    default:
                        statusName = "Inny";
                        break;
                }
                return statusName;
            }
        }
    }
}
