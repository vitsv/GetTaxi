//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class PricesRate
    {
        public int PricesRateId { get; set; }
        public int CompanyId { get; set; }
        public bool IsUsingNightRate { get; set; }
        public decimal PerKmAtDay { get; set; }
        public decimal PerKmAtNight { get; set; }
        public Nullable<decimal> Pet { get; set; }
        public decimal ClosingDoor { get; set; }
        public int DayStarstAt { get; set; }
        public int NightStartsAt { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
