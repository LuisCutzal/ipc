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
    
    public partial class Solitario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solitario()
        {
            this.Cargar_Guardar = new HashSet<Cargar_Guardar>();
            this.DetalleModo = new HashSet<DetalleModo>();
        }
    
        public string Id_solitario { get; set; }
        public string Detalle { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cargar_Guardar> Cargar_Guardar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleModo> DetalleModo { get; set; }
        public virtual DetallePartida DetallePartida { get; set; }
    }
}
