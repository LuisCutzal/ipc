using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace IPC2_201700841.Controllers
{
    public class ArchivoCargaController : Controller
    {
        
        // GET: FileUpload
        public ActionResult Index()
        {
            //var items = GetFiles();
            //return View(items);
            
            return View();
        }
        
        [HttpPost]
        
        public ActionResult Index(HttpPostedFileBase file) 
        {

            XmlReader reader = XmlReader.Create(new StreamReader(file.InputStream));
            //List<string> datos = new List<string>();
            string datos = "";
            while (reader.Read()) 
            {
                if (reader.IsStartElement()) 
                {
                    switch (reader.Name.ToString())
                    {
                        case "ficha":
                            datos += reader.ReadString();
                            break;
                        
                    }
                }
            }
            return View("Index");
        }
    //    [HttpPost]
    //    public ActionResult Index(HttpPostedFileBase file)
    //    {
    //        if (file != null && file.ContentLength > 0) 
    //        { 
    //            try
    //            {
    //                string path = Path.Combine(Server.MapPath("~/Archivo"),Path.GetFileName(file.FileName));
    //                file.SaveAs(path); //guardar archivo fisicamente
    //                ViewBag.Message = "Archivo Cargado";
    //            }
    //            catch (Exception ex)
    //            {
    //                ViewBag.Message = "ERROR:" + ex.Message.ToString();
    //            }
    //        }
    //        else
    //        {
    //            ViewBag.Message = "Error al Guardar"; //esto es al momento de guardar
    //        }

    //        var items = GetFiles();
    //        return View(items);
    //    }

    //    public FileResult Download(string ArchivoName)
    //    {
    //        var FileVirtualPath = "~/Archivo/" + ArchivoName;
    //        return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
    //    }

    //    private List<string> GetFiles()
    //    {
    //        var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Archivo"));
    //        System.IO.FileInfo[] fileNames = dir.GetFiles("*.xml");

    //        List<string> items = new List<string>();
    //        foreach (var file in fileNames)
    //        {
    //            items.Add(file.Name);
    //        }

    //        return items;
    //    }
}
}