using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPC2_201700841.Models.ViewModels
{
    public class ObtenerContenidoA
    {
        public int id { get; set; }
        public string color { get; set; }
        public string columna { get; set; }
        public Nullable<int> fila { get; set; }

       
        //prop coloca de una vez public string nombre {get;set;}
    }
}