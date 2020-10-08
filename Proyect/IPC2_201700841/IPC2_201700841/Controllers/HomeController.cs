using IPC2_201700841.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPC2_201700841.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Inicio()
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
                
                using (Proyecto_IPC2 baseDatos = new Proyecto_IPC2()) //entitis es el entity framework
                {
                    var lista = from datos in baseDatos.Usuario
                                where datos.Nombre_usuario == us && datos.Contra.ToString() == contra
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
                return RedirectToAction("StartSesion"); //usuario valido
            }
            catch (Exception ex)
            {
                //return View(model);
            }
            return View();
        }
        public ActionResult StartSesion()
        {
            return View();
        }


        public ActionResult Solitario()
        {
            return View();
        }

    }
   
}