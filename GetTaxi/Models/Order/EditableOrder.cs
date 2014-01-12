using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Order
{
    public class EditableOrder
    {
       public int CityFrom { get; set; }
       public string AddressFrom { get; set; }
       public int? CityTo { get; set; }
       public string AddressTo { get; set; }
       public int Company { get; set; }
       public int? OrderClass { get; set; }
       public bool Childer { get; set; }
       public bool NoSmoking { get; set; }
       public bool Animal { get; set; }
       public bool English { get; set; }
       public bool Card { get; set; }
       public string PlannedOn { get; set; }
       public string Comment { get; set; }
    }
}