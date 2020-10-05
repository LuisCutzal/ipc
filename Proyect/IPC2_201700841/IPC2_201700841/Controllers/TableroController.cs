using System;
using System.Collections.Generic;
using IPC2_201700841.Models.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Web.WebSockets;
using IPC2_201700841.Models;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace IPC2_201700841.Controllers
{
    //[HttpGet] // este metodo para hacer una posicion de resivir informacion de una pagina
    //[HttpPost] //enviar informacion hacia una pagina web, cualquier tipo de informacion
    public class TableroController : Controller
    {
        // GET: Tablero
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TableroSolitario()
        {
            if (Session["juego"] == null)
            {
                ObtenerContenidoA i1 = new ObtenerContenidoA();
                i1.color = "blanco";
                i1.fila = 4;
                i1.columna = "D";
                ObtenerContenidoA i2 = new ObtenerContenidoA();
                i2.color = "negro";
                i2.fila = 4;
                i2.columna = "E";
                ObtenerContenidoA i3 = new ObtenerContenidoA();
                i3.color = "negro";
                i3.fila = 5;
                i3.columna = "D";
                ObtenerContenidoA i4 = new ObtenerContenidoA();
                i4.color = "blanco";
                i4.fila = 5;
                i4.columna = "E";
                List<ObtenerContenidoA> fichas = new List<ObtenerContenidoA>();
                fichas.Add(i1);
                fichas.Add(i2);
                fichas.Add(i3);
                fichas.Add(i4);
                Session["juego"] = fichas;
                var send = Environment.TickCount;
                var random = new Random(send);
                for (int i = 0; i <1; i++) 
                {
                    var valor = random.Next(0, 2); //valor random entre 0 y 1
                    System.Diagnostics.Debug.WriteLine(valor);
                    if (valor == 0) //fichas negras (computadora)
                    {
                        Session["turno"] = valor;
                        
                        /*si el valor = 0 entonces en la variable Session["turno"] coloco un valor
                        con el fin de que no sea nulo esta variable para poder diferenciarla 
                        de la variable nula la cual es para la ficha blanca*/
                    }
                    else //fichas blancas (usuario)
                    {
                        Session["turno"] = null;
                        
                        /*si el valor = 1 entonces en la variable Session["turno"] la creo nula
                         con el fin de que se pueda diferenciar para el color negro*/
                    }

                    //if (Session["turno"] != null)
                    //{
                    //    var prueba = true;
                    //    ViewData["valor"] = prueba;
                    //}
                    //else
                    //{
                    //    var prueba = false;
                    //    ViewData["valor"] = prueba;
                    //}

                }
                return View("~/Views/Tablero/TableroSolitario.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
            }
            else
            {
                return View("~/Views/Tablero/TableroSolitario.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
            }
        }
        [HttpPost]
        public ActionResult InicioSolitario() //esto es para cuando estamos en el modo solitario
        {
            try
            {
                if (Session["turno"] != null) //fichas negras (computadora)
                {
                    //ViewBag.Mensaje = "Turno computadora";
                    /*
                    System.Diagnostics.Debug.WriteLine("funciono cero");
                    var Nfila = fila;
                    var Ncolumna = columna;
                    ObtenerContenidoA f1 = new ObtenerContenidoA();
                    f1.color = "negro";
                    f1.fila = Nfila;
                    f1.columna = Ncolumna;
                    //List<ObtenerContenidoA> ficha = new List<ObtenerContenidoA
                    List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                    ficha.Add(f1);
                    //Session["juego"] = ficha;}
                    */
                    /*coloco un if(Session["turno"]!= null) para que si tiene un valor igual a 0
                      pueda crear una ficha de color negro*/
                    ObtenerContenidoA turno1 = new ObtenerContenidoA();
                    turno1.color = "negro";
                    turno1.fila = 6;
                    turno1.columna = "E";
                    List<ObtenerContenidoA> fichas = (List<ObtenerContenidoA>)Session["juego"];
                    fichas.Add(turno1);

                }
                else { ViewBag.Mensaje = "Turno Usuario"; }
            }
            catch (Exception)
            {

                throw;
            }
            return View("~/Views/Tablero/TableroSolitario.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
        }
        public ActionResult TableroVersus()
        {
            if (Session["juego"] == null)
            {
                ObtenerContenidoA i1 = new ObtenerContenidoA();
                i1.color = "blanco";
                i1.fila = 4;
                i1.columna = "D";
                ObtenerContenidoA i2 = new ObtenerContenidoA();
                i2.color = "negro";
                i2.fila = 4;
                i2.columna = "E";
                ObtenerContenidoA i3 = new ObtenerContenidoA();
                i3.color = "negro";
                i3.fila = 5;
                i3.columna = "D";
                ObtenerContenidoA i4 = new ObtenerContenidoA();
                i4.color = "blanco";
                i4.fila = 5;
                i4.columna = "E";
                List<ObtenerContenidoA> fichas = new List<ObtenerContenidoA>();
                fichas.Add(i1);
                fichas.Add(i2);
                fichas.Add(i3);
                fichas.Add(i4);
                Session["juego"] = fichas;
                var send = Environment.TickCount;
                var random = new Random(send);
                for (int i = 0; i < 1; i++)
                {
                    var valor = random.Next(0, 2); //valor random entre 0 y 1
                    System.Diagnostics.Debug.WriteLine(valor);
                    if (valor == 0) //fichas negras (jugador 1)
                    {
                        Session["turno"] = valor;
                        ViewBag.Mensaje = "Turno fichas negras";
                        /*si el valor = 0 entonces en la variable Session["turno"] coloco un valor
                        con el fin de que no sea nulo esta variable para poder diferenciarla 
                        de la variable nula la cual es para la ficha blanca*/

                    }
                    else //fichas blancas (usuario)
                    {
                        Session["turno"] = null;
                        ViewBag.Mensaje = "Turno fichas blancas";
                        /*si el valor = 1 entonces en la variable Session["turno"] la creo nula
                         con el fin de que se pueda diferenciar para el color negro*/

                    }
                }
                return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
            }
            else
            {
                return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
            }
        }
        [HttpPost]
        public ActionResult InicioVersus(int fila,string columna) 
        {
            try
            {
                if (Session["turno"] != null)
                {
                    var NfilaV = fila;
                    var NcolumnaV = columna;
                    ObtenerContenidoA f1 = new ObtenerContenidoA();
                    f1.color = "negro";
                    f1.fila = NfilaV;
                    f1.columna = NcolumnaV;
                    List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                    ficha.Add(f1);
                    Session["turno"] = null;
                    
                }
                else 
                {
                    var NfilaV = fila;
                    var NcolumnaV = columna;
                    ObtenerContenidoA f1 = new ObtenerContenidoA();
                    f1.color = "blanco";
                    f1.fila = NfilaV;
                    f1.columna = NcolumnaV;
                    List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                    ficha.Add(f1);
                    int valor = 1;
                    Session["turno"] = valor;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
        }
        [HttpPost]
        public ActionResult Dato(int fila, string columna) //esto es para el tablero solitario (computadora vs usuario)
        { 
            try
            {
                /*coloco if para saber si la variable Session["turno"] == null
                que a su vez sea un numero 1 el cual es el turno de la ficha color blanca*/
                if (Session["turno"] == null) //fichas blancas (usuario)
                {//fichas blancas (usuario)
                    System.Diagnostics.Debug.WriteLine("funciono uno");
                    var Nfila = fila;
                    var Ncolumna = columna;
                    ObtenerContenidoA f1 = new ObtenerContenidoA();
                    f1.color = "blanco";
                    f1.fila = Nfila;
                    f1.columna = Ncolumna;
                    //List<ObtenerContenidoA> ficha = new List<ObtenerContenidoA
                    List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                    ficha.Add(f1);
                    //Session["juego"] = ficha;
                }

                //else
                //{ //fichas blancas (usuario)
                //    System.Diagnostics.Debug.WriteLine("funciono uno");
                //    var Nfila = fila;
                //    var Ncolumna = columna;
                //    ObtenerContenidoA f1 = new ObtenerContenidoA();
                //    f1.color = "blanco";
                //    f1.fila = Nfila;
                //    f1.columna = Ncolumna;
                //    //List<ObtenerContenidoA> ficha = new List<ObtenerContenidoA
                //    List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                //    ficha.Add(f1);
                //    //Session["juego"] = ficha;
                //}
                /* esto es para el turno de la computadora
                var Nfila = fila;
                var Ncolumna = columna;
                ObtenerContenidoA f1 = new ObtenerContenidoA();
                f1.color = "negro";
                f1.fila = Nfila;
                f1.columna = Ncolumna;
                //List<ObtenerContenidoA> ficha = new List<ObtenerContenidoA
                List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                ficha.Add(f1);
                //Session["juego"] = ficha;
                */
            }
            catch (Exception)
            {
                throw;
            }
            return View("~/Views/Tablero/TableroSolitario.cshtml", (List<ObtenerContenidoA>)Session["juego"]);

        }

        //no tocar nada
        [HttpPost]
        public ActionResult Index (ArchivoModel file)
        {
            string ruta = Server.MapPath("~/");//raiz del proyecto
            string RutaArchivo = Path.Combine(ruta + "/Archivos/ejemplo.xml");
            //string RutaArchivo = Path.Combine(ruta + "/Archivos/" + file + ".xml");
            if (!ModelState.IsValid)//si el modelo que estoy pasando es valido
            {
                return View("Index", file); //model invalido
            }
            file.Archivo.SaveAs(RutaArchivo);
            XmlReader reader = XmlReader.Create(RutaArchivo);            
            List<ObtenerContenidoA> prueba = new List<ObtenerContenidoA>();
            Session["juego"] = prueba;
            if(RutaArchivo != null)
            {
                ObtenerContenidoA ficha = new ObtenerContenidoA();
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "color":
                                ficha.color = reader.ReadString();
                                //System.Diagnostics.Debug.WriteLine(ficha.color);
                                break;
                            case "columna":
                                ficha.columna = reader.ReadString();
                                //System.Diagnostics.Debug.WriteLine(ficha.columna);
                                break;
                            case "fila":
                                ficha.fila = Int32.Parse(reader.ReadString());
                                //System.Diagnostics.Debug.WriteLine(ficha.fila);
                                prueba.Add(ficha);
                                ficha = new ObtenerContenidoA();
                                //System.Diagnostics.Debug.WriteLine(prueba);
                                break;
                        }
                    }
                }
                reader.Close();
            }
            return View("Index");
        }
        [HttpGet]
        public FileResult CrearArchivo()
        {
            //List<ObtenerContenidoA> dato = new List<ObtenerContenidoA>();
            //Session["juego"] = dato;
            int cont = 0;
            var estar = new MemoryStream();
            XmlWriterSettings config = new XmlWriterSettings();
            config.Indent = true;
            XmlWriter escribirxmml = XmlWriter.Create(estar, config);
            escribirxmml.WriteStartDocument();
            escribirxmml.WriteStartElement("tablero");
            //System.Diagnostics.Debug.WriteLine("entro");
            //System.Diagnostics.Debug.WriteLine(dato);
            foreach (var busca in Session["juego"] as List<ObtenerContenidoA>)
            {
                //System.Diagnostics.Debug.WriteLine("alfin");
                escribirxmml.WriteStartElement("ficha");
                escribirxmml.WriteStartElement("color");
                escribirxmml.WriteString(busca.color);
                //System.Diagnostics.Debug.WriteLine(busca.color);
                escribirxmml.WriteEndElement();
                escribirxmml.WriteStartElement("columna");
                escribirxmml.WriteString(busca.columna);
                escribirxmml.WriteEndElement();
                escribirxmml.WriteStartElement("fila");
                escribirxmml.WriteString(busca.fila.ToString());
                escribirxmml.WriteEndElement();
                escribirxmml.WriteEndElement();
                cont++;
            }
            //System.Diagnostics.Debug.WriteLine("salio");
            escribirxmml.WriteEndElement();
            escribirxmml.WriteEndDocument();
            escribirxmml.Close();
            estar.Position = 0;
            var ArchivoResultado = File(estar, "application/octet-stream", " ");
            return ArchivoResultado;
        }
        //termina no tocar nada

        /*
        public ActionResult TableroTorneo()
        {
            return View();
        }
        
         esto es para los tableros de los otrso 2 modos*/



    }
}