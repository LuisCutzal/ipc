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
        public ActionResult IndexVersus()
        {
            return View();
        }
        public ActionResult TableroSolitario(string f)
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
                if (f == "blanco") //para cuando cualquier usuario eliga una ficha de color negro le toca segundo
                {
                    var cero = 0;
                    Session["turno"] = cero;

                }
                else //para cuando el usuario eligio una ficha de color blanco le toca primero
                {
                    Session["turno"] = null;
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
            return View("~/Views/Tablero/TableroSolitario.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
        }

        public ActionResult MovimientoSolitario(int fila, string columna) //tablero modo solitario
        {
           
            return View("~/Views/Tablero/TableroSolitario.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
        }
        public ActionResult TableroVersus(string f)
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
                if (f == "blanco") //para cuando cualquier usuario eliga una ficha de color negro le toca segundo
                {
                    var cero = 0;
                    Session["turno"] = cero;

                }
                else //para cuando el usuario eligio una ficha de color blanco le toca primero
                {
                    Session["turno"] = null;
                }
                return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
            }
            else
            {
                return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
            }
        }
        public ActionResult Movimiento(int fila, string columna) //tablero modo versus
        {
            var inicio = (List<ObtenerContenidoA>)Session["juego"]; //lista piesas
            string turnoJugando ="";
            ObtenerContenidoA FichaPrimero = new ObtenerContenidoA();
            FichaPrimero.color = CambiarTurno(turnoJugando);
            FichaPrimero.fila = fila;
            FichaPrimero.columna = columna.ToUpper();
            if (PrimeraCondicion(inicio,FichaPrimero))
            {
                inicio.Add(FichaPrimero); 
            }
            return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
        }
        //inicio metodo para cambiar turno
        public string CambiarTurno(string turnoJugando) //cambia cualquier turno
        {
            int i = 0;
            if (Session["turno"] == null)
            {
                ViewBag.Mensaje = "Turno fichas blancas";
                turnoJugando = "negro";
                Session["turno"] = i;
            }
            else //blancas
            {
                ViewBag.Mensaje = "Turno fichas negras";
                turnoJugando = "blanco";
                Session["turno"] = null;
            }
            return turnoJugando;
        }
        //fin metodo para cambiar turno


        public bool Calculando() //calcula las jugadas que pueden haber 
        {
            

        }



        //ya no tocar nada
        //
        //
        //
        public bool PrimeraCondicion(List<ObtenerContenidoA> inicio, ObtenerContenidoA FichaPrimero) //le llega todas las fichas, recive la ficha y usa la conidcion
        {
            bool valido = false;
            int columnaEntrando = ((int)char.ToUpper(char.Parse(FichaPrimero.columna))) - 64;
            foreach (var item in inicio)
            {
                int itemColumna = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
                if (FichaPrimero.fila - 1 <= item.fila && item.fila <= FichaPrimero.fila + 1)
                {
                    if (columnaEntrando - 1 <= itemColumna && itemColumna <= columnaEntrando + 1)
                    {
                        if (FichaPrimero.color != item.color)
                        {
                            if (SegundaCondicion(inicio, FichaPrimero, item))
                            {
                                valido = true;
                            }
                        }
                    }
                }
            }
            return valido;
        }
        //metodo para validar las posiciones de las fichas
        public bool SegundaCondicion(List<ObtenerContenidoA> inicio, ObtenerContenidoA FichaPrimero, ObtenerContenidoA item)
        { //ya cambia el color de la ficha que agrege
            int columnaEntrando = ((int)char.ToUpper(char.Parse(FichaPrimero.columna))) - 64;
            int itemColumna = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
            int RestandoColumnas = columnaEntrando - itemColumna;
            int RestadoFila = FichaPrimero.fila - item.fila;
            bool valido = false;
            if (RestandoColumnas == 0 && RestadoFila == 1) //abajo
            {
                int MoviendoFila = item.fila;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    //if (inicio.Find(x => x.columna)) //Find busca y debuelve un valor
                    //{

                    //}
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        if (item2.columna == FichaPrimero.columna)
                        {
                            if (item2.fila == MoviendoFila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoFila--; //abajo
                }
            }
            if (RestandoColumnas == 0 && RestadoFila == -1) //arriba
            {
                int MoviendoFila = item.fila;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        if (item2.columna == FichaPrimero.columna)
                        {
                            if (item2.fila == MoviendoFila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoFila++; //arriba
                }
            }
            if (RestandoColumnas == 1 && RestadoFila == 0) //derecha
            {
                int MoviendoColuma = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        int Colum = ((int)char.ToUpper(char.Parse(item2.columna))) - 64;
                        if (Colum == MoviendoColuma)
                        {
                            if (item2.fila == FichaPrimero.fila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoColuma--; //derecha
                }
            }
            if (RestandoColumnas == -1 && RestadoFila == 0) //izquierda
            {
                int MoviendoColuma = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        int Colum = ((int)char.ToUpper(char.Parse(item2.columna))) - 64;
                        if (Colum == MoviendoColuma)
                        {
                            if (item2.fila == FichaPrimero.fila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoColuma++; //izquierda
                }
            }
            if (RestandoColumnas == -1 && RestadoFila ==1) //diagonal inferior izquirda
            {
                int MoviendoFila = item.fila;
                int MoviendoColuma = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        int Colum= ((int)char.ToUpper(char.Parse(item2.columna))) - 64;
                        if (Colum == MoviendoColuma)
                        {
                            if (item2.fila == MoviendoFila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoFila--; //abajo
                    MoviendoColuma++; //izquierda
                }
            }
            if (RestandoColumnas == 1 && RestadoFila == 1) //diagonal inferior derecha
            {
                int MoviendoFila = item.fila;
                int MoviendoColuma = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        int Colum = ((int)char.ToUpper(char.Parse(item2.columna))) - 64;
                        if (Colum == MoviendoColuma)
                        {
                            if (item2.fila == MoviendoFila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoFila--; //abajo
                    MoviendoColuma--; //derecha
                }
            }
            if (RestandoColumnas == -1 && RestadoFila == -1) //diagonal superior izquierda
            {
                int MoviendoFila = item.fila;
                int MoviendoColuma = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        int Colum = ((int)char.ToUpper(char.Parse(item2.columna))) - 64;
                        if (Colum == MoviendoColuma)
                        {
                            if (item2.fila == MoviendoFila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoFila++; //arriba
                    MoviendoColuma++; //izquierda
                }
            }
            if (RestandoColumnas == 1 && RestadoFila == -1) //diagonal superior derecha
            {
                int MoviendoFila = item.fila;
                int MoviendoColuma = ((int)char.ToUpper(char.Parse(item.columna))) - 64;
                ObtenerContenidoA SiBusqueda = new ObtenerContenidoA();
                List<ObtenerContenidoA> GirandoFichas = new List<ObtenerContenidoA>();
                while (SiBusqueda != null)
                {
                    SiBusqueda = null;
                    foreach (var item2 in inicio)
                    {
                        int Colum = ((int)char.ToUpper(char.Parse(item2.columna))) - 64;
                        if (Colum == MoviendoColuma)
                        {
                            if (item2.fila == MoviendoFila)
                            {
                                if (item2.color != FichaPrimero.color)
                                {
                                    SiBusqueda = item2;
                                    GirandoFichas.Add(item2);
                                }
                                else if (item2.color == FichaPrimero.color)
                                {
                                    SiBusqueda = null;
                                    valido = true;
                                    CambiaColor(GirandoFichas);
                                }
                            }
                        }
                    }
                    MoviendoFila++; //arriba
                    MoviendoColuma--; //derecha
                }
            }
            return valido;
        }
        //inicio del metodo cambiar color
        public void CambiaColor(List<ObtenerContenidoA> GirandoFichas)
        {
            foreach (var item3 in GirandoFichas)
            {
                if (item3.color == "negro")
                {
                    item3.color = "blanco";
                }
                else
                {
                    item3.color = "negro";
                }
            }
        }
        //fin del metodo cambiar color
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
        [HttpPost]
        public ActionResult IndexVersus(ArchivoModel file)
        {
            string ruta = Server.MapPath("~/");//raiz del proyecto
            string RutaArchivo = Path.Combine(ruta + "/Archivos/ejemplo.xml");
            //string RutaArchivo = Path.Combine(ruta + "/Archivos/" + file + ".xml");
            if (!ModelState.IsValid)//si el modelo que estoy pasando es valido
            {
                return View("IndexVersus", file); //model invalido
            }
            file.Archivo.SaveAs(RutaArchivo);
            XmlReader reader = XmlReader.Create(RutaArchivo);
            List<ObtenerContenidoA> prueba = new List<ObtenerContenidoA>();
            Session["juego"] = prueba;
            if (RutaArchivo != null)
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
            return View("IndexVersus");
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
    }
}