﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPortfolio.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyPortfolioEntities : DbContext
    {
        public MyPortfolioEntities()
            : base("name=MyPortfolioEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Abouts> Abouts { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Experiences> Experiences { get; set; }
        public virtual DbSet<Features> Features { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<SocialMedias> SocialMedias { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Testimonials> Testimonials { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
    }
}
