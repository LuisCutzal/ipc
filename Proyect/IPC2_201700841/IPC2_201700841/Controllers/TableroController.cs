using System;
using System.Collections.Generic;
using IPC2_201700841.Models.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Data.SqlClient;

namespace IPC2_201700841.Controllers
{
    public class TableroController : Controller
    {
        // GET: Tablero
        public ActionResult TableroSolitario()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TableroSolitario (ArchivoModel file)
        {
            string ruta = Server.MapPath("~/");//raiz del proyecto
            string RutaArchivo = Path.Combine(ruta + "/Archivo/ejemplo.xml");
            if (!ModelState.IsValid)//si el modelo que estoy pasando es valido
            {
                return View("Index", file); //model invalido
            }
            file.Archivo.SaveAs(RutaArchivo);
            XmlReader reader = XmlReader.Create(RutaArchivo);

            /*este codigo es para insertar en la bd, 
            no se usara ese connectionstring porque no usare la bd*/
            /*using (SqlConnection sql1 = new SqlConnection(connectionString)) 
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
            }*/
            return View("TableroSolitario");
        }

        /*public ActionResult TableroVersus()
        {
            return View();
        }

        public ActionResult TableroTorneo()
        {
            return View();
        }
        
         esto es para los tableros de los otrso 2 modos*/
    }
}