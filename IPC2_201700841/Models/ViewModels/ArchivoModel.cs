using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IPC2_201700841.Models.ViewModels
{
    public class ArchivoModel
    {
        [Required] //para hacer solo campos obligatorios
        public HttpPostedFileBase Archivo  { get; set; }

}
}