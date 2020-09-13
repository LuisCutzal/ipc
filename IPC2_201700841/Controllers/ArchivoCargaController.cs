using IPC2_201700841.Models;
using IPC2_201700841.Models.ViewModels;
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
        public ActionResult Tab()
        {
            List<ObtenerContenidoA> datos;

            using (FaseIpc2_201700841Entities db = new FaseIpc2_201700841Entities())
            {
                datos = (from linea in db.Archivo
                       select new ObtenerContenidoA
                       {
                           id = linea.id,
                           color = linea.color,
                           columna = linea.columna,
                           fila = linea.fila
                       }
                    ).ToList();
            }
            return View(datos);
        }
        [HttpPost]
        public ActionResult Index(ArchivoModel file)
        {
            string ruta = Server.MapPath("~/");//raiz del proyecto
            string RutaArchivo = Path.Combine(ruta + "/Archivo/ejemplo.xml");
            if (!ModelState.IsValid)//si el modelo que estoy pasando es valido
            {
                return View("Index",file); //model invalido
            }
            file.Archivo.SaveAs(RutaArchivo);
            XmlReader reader = XmlReader.Create(RutaArchivo);
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
        
        [HttpGet]
        public FileResult CrearArchivo()
        {
            using (FaseIpc2_201700841Entities db = new FaseIpc2_201700841Entities())
            {
                var estar = new MemoryStream();
                XmlWriterSettings config = new XmlWriterSettings();
                config.Indent = true;
                XmlWriter escribirxmml = XmlWriter.Create(estar, config);
                escribirxmml.WriteStartDocument();
                escribirxmml.WriteStartElement("tablero");
                foreach (var busca in db.Archivo)
                {
                    escribirxmml.WriteStartElement("ficha");
                    escribirxmml.WriteStartElement("color");
                    escribirxmml.WriteString(busca.color);
                    escribirxmml.WriteEndElement();
                    escribirxmml.WriteStartElement("columna");
                    escribirxmml.WriteString(busca.columna);
                    escribirxmml.WriteEndElement();
                    escribirxmml.WriteStartElement("fila");
                    escribirxmml.WriteString(busca.fila.ToString());
                    escribirxmml.WriteEndElement();
                    escribirxmml.WriteEndElement();
                }
                escribirxmml.WriteEndElement();
                escribirxmml.WriteEndDocument();
                escribirxmml.Close();
                estar.Position = 0;
                var ArchivoResultado = File(estar, "application/octet-stream", " ");
                return ArchivoResultado;

            }
        }
    }
}