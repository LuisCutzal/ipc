//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Torneo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Torneo()
        {
            this.Detalle_torneo = new HashSet<Detalle_torneo>();
            this.ListaTorneo = new HashSet<ListaTorneo>();
        }
    
        public int id_torneo { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> id_admin { get; set; }
    
        public virtual Admin Admin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_torneo> Detalle_torneo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListaTorneo> ListaTorneo { get; set; }
    }
}
