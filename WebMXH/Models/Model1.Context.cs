﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMXH.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MXH_GREENZONEEntities : DbContext
    {
        public MXH_GREENZONEEntities()
            : base("name=MXH_GREENZONEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BAIVIET> BAIVIETs { get; set; }
        public virtual DbSet<BANBE> BANBEs { get; set; }
        public virtual DbSet<CALL> CALLs { get; set; }
        public virtual DbSet<COMMENT> COMMENTs { get; set; }
        public virtual DbSet<GROUP> GROUPS { get; set; }
        public virtual DbSet<MESSAGE> MESSAGEs { get; set; }
        public virtual DbSet<MESSAGE_PHONGHOP> MESSAGE_PHONGHOP { get; set; }
        public virtual DbSet<PHONGHOP> PHONGHOPs { get; set; }
        public virtual DbSet<THICH> THICHes { get; set; }
        public virtual DbSet<USER_CONNECTION> USER_CONNECTION { get; set; }
        public virtual DbSet<USER_TGHOP> USER_TGHOP { get; set; }
        public virtual DbSet<USERR> USERRs { get; set; }
        public virtual DbSet<VIDEO_CUOCHOP> VIDEO_CUOCHOP { get; set; }
    }
}
