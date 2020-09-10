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
    }
}