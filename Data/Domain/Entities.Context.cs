﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GetTaxiEntities : DbContext
    {
        public GetTaxiEntities()
            : base("name=GetTaxiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Address> Address { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderNote> OrderNote { get; set; }
        public DbSet<OrderProperties> OrderProperties { get; set; }
        public DbSet<Right> Right { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}
