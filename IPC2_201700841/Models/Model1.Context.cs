﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPC2_201700841.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FaseIpc2_201700841Entities : DbContext
    {
        public FaseIpc2_201700841Entities()
            : base("name=FaseIpc2_201700841Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cargar_Guardar> Cargar_Guardar { get; set; }
        public virtual DbSet<DetalleModo> DetalleModo { get; set; }
        public virtual DbSet<DetallePartida> DetallePartida { get; set; }
        public virtual DbSet<Invitacion> Invitacion { get; set; }
        public virtual DbSet<Solitario> Solitario { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Torneo> Torneo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Versus> Versus { get; set; }
    }
}
