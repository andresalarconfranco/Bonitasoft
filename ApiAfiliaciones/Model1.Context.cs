﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiAfiliaciones
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AfiliacionesSkandiaEntities : DbContext
    {
        public AfiliacionesSkandiaEntities()
            : base("name=AfiliacionesSkandiaEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Afiliacion> Afiliacion { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
    }
}
