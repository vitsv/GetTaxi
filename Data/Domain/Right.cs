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
    
    public partial class Right
    {
        public Right()
        {
            this.RoleRight = new HashSet<RoleRight>();
        }
    
        public int RightId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    
        public virtual ICollection<RoleRight> RoleRight { get; set; }
    }
}