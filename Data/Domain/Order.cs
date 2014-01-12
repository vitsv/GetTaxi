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
    
    public partial class Order
    {
        public Order()
        {
            this.OrderNote = new HashSet<OrderNote>();
        }
    
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int OrderPropertiesId { get; set; }
        public Nullable<int> CarId { get; set; }
        public System.DateTime Deadline { get; set; }
        public int AddressId { get; set; }
        public Nullable<int> Tariff { get; set; }
        public Nullable<double> EstimatedPrice { get; set; }
        public bool IsPrepaid { get; set; }
        public bool IsPlanned { get; set; }
        public Nullable<System.DateTime> PlannedOn { get; set; }
        public int Status { get; set; }
        public System.DateTime TimeCreated { get; set; }
        public Nullable<System.DateTime> TimeAssigned { get; set; }
        public Nullable<System.DateTime> TimeArrived { get; set; }
        public Nullable<System.DateTime> TimeInCar { get; set; }
        public Nullable<System.DateTime> TimeDone { get; set; }
        public Nullable<System.DateTime> TimeCanceled { get; set; }
        public Nullable<int> CanceledBy { get; set; }
        public Nullable<int> CancelCause { get; set; }
        public Nullable<double> FinalPrice { get; set; }
        public string UserComment { get; set; }
        public string TaxiComment { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Car Car { get; set; }
        public virtual OrderProperties OrderProperties { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderNote> OrderNote { get; set; }
    }
}
