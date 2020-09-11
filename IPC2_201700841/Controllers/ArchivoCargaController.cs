using IPC2_201700841.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        string connectionString = @"Data Source=localhost\SQLEXPRESS;initial Catalog=FaseIpc2_201700841;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";
        // GET: FileUpload
        public ActionResult Index()
        {
                       
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            XmlReader reader = XmlReader.Create(new StreamReader(file.InputStream));
            using (SqlConnection sql1 = new SqlConnection(connectionString)) 
            {
                sql1.Open();
                string prueba = " ";
                while (reader.Read()) 
                {
                    if (reader.IsStartElement()) 
                    {
                        switch (reader.Name.ToString()) 
                        {
                            case "ficha":
                                prueba += "insert into Archivo values(";
                                break;
                            case "color":
                                prueba += "'" + reader.ReadString() + "'" + ",";
                                break;
                            case "columna":
                                prueba += "'" + reader.ReadString() + "'" + ",";
                                break;
                            case "fila":
                                prueba += reader.ReadString() + ");";
                                if (prueba != "") 
                                {
                                    SqlCommand sql2 = new SqlCommand(prueba, sql1);
                                    sql2.ExecuteNonQuery();
                                    prueba = "";
                                }
                                break;
                        }
                    }
                }
                sql1.Close();
            }
            return View("Index");
        }
        //[HttpGet]
        //public FileResult Generar()
        //{
        //    using (FaseIpc2_201700841Entities db = new FaseIpc2_201700841Entities())
        //    {

        //    }
        //}
        public ActionResult Tab()
        {
            using (FaseIpc2_201700841Entities db = new FaseIpc2_201700841Entities())
            {
                return View(db.Archivo.ToList());
            }

        }

    }
}