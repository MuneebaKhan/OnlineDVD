﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShop4DVDS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlineDVDEntities4 : DbContext
    {
        public OnlineDVDEntities4()
            : base("name=OnlineDVDEntities4")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_Category> tbl_Category { get; set; }
        public virtual DbSet<tbl_Invoice> tbl_Invoice { get; set; }
        public virtual DbSet<tbl_OrderDetail> tbl_OrderDetail { get; set; }
        public virtual DbSet<tbl_Product> tbl_Product { get; set; }
        public virtual DbSet<tbl_SubCategory> tbl_SubCategory { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
    }
}
