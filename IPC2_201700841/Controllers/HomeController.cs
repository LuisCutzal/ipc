using IPC2_201700841.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace IPC2_201700841.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]//protocolo
        public ActionResult Index(string us, string contra) //us toma el texto escrito en el index
        {
            //if (TempData["Message"] != null)
             //   ViewBag.Message = TempData["Message"].ToString();
            try
            {

                using (FaseIpc2_201700841Entities baseDatos = new FaseIpc2_201700841Entities()) //entitis es el entity framework
                {
                    var lista = from datos in baseDatos.Usuario
                                where datos.NombreUsuario == us && datos.Contra.ToString() == contra
                                select datos;
                    if (lista.Count() > 0) //si hay usuarios en la lista
                    {
                        Usuario usuarios = lista.First(); //toma el primero y crea la sesion
                        Session["usuario"] = usuarios; //sesion del usiaro con el modelo
                    }
                    else
                    {
                        //el usuario es invalido, lo redirecciona automaticamente al registro de usuarios.
                        return Redirect("/Usuarios/Create");
                      //  return Content("Usuario invalio ingrese de nuevo sus creenciales"); //se va al registro porque no entro
                    }

                }
                // el usuario ingresa a su cuenta.
                //@TempData["Message"] = "prueba";
                return RedirectToAction("Contact"); //usuario valido

            }
            catch (Exception ex)
            {
                //return View(model);
            }
            return View();
        }

        public ActionResult About() //about es la pestaña en solitario
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }
        //agregar una lista y luego recorrerla

        

        public ActionResult Contact() //about es igual que el contact
        {
            ViewBag.Message = "Your application description page.";

            return View(); //representa una vista
        }

        [HttpPost] //este bloque de codigo es para dar evento a los 3 botones "solitario", "versus" y "torneo"
        public ActionResult Contact(string a)
        {
            
            try {


                 return RedirectToAction("About");
                  
            }
            catch(Exception e) { 
            }
            
            
            return View();
        }
    }
}