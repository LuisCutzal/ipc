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
                for (int i = 0; i < 1; i++)
                {
                    var valor = random.Next(0, 2); //valor random entre 0 y 1
                    System.Diagnostics.Debug.WriteLine(valor);
                    if (valor == 0 || valor == 1) //fichas blancas (computadora)
                    {
                        //Session["turno"] = valor;
                        Session["turno"] = null;
                        /*si el valor = 0 entonces en la variable Session["turno"] coloco un valor
                        con el fin de que no sea nulo esta variable para poder diferenciarla 
                        de la variable nula la cual es para la ficha blanca*/
                    }
                    else //fichas negras (usuario)
                    {
                        //Session["turno"] = null;
                        Session["turno"] = valor;
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

        //[HttpPost]
        //public ActionResult Salto() //esto es para el boton saltar turno
        //{
        //    if (Session["turno"] == null)
        //    {
        //        var cero = 0;
        //        Session["turno"] = cero;
        //    }
        //    else
        //    {
        //        Session["turno"] = null;
        //    }
        //    return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
        //}

        public ActionResult Movimiento(int fila, string columna) //tablero modo versus
        {
            var inicio = (List<ObtenerContenidoA>)Session["juego"]; //lista piesas
            int i = 0;
            string turnoJugando = "";
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
            ObtenerContenidoA FichaPrimero = new ObtenerContenidoA();
            FichaPrimero.color = turnoJugando;
            FichaPrimero.fila = fila;
            FichaPrimero.columna = columna.ToUpper();
            if (PrimeraCondicion(inicio,FichaPrimero))
            {
                inicio.Add(FichaPrimero); 
            }
            return View("~/Views/Tablero/TableroVersus.cshtml", (List<ObtenerContenidoA>)Session["juego"]);
        }
        //metodo para los dos colores
        public void CambiarTurno() 
        {
            int i = 0;
            string turnoJugando = "";
            if (Session["turno"] == null)
            {
                turnoJugando = "negro";
                Session["turno"] = i;
            }
            else //blancas
            {
                turnoJugando = "blanco";
                Session["turno"] = null;
            }
        }
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

        public bool SegundaCondicion(List<ObtenerContenidoA> inicio, ObtenerContenidoA FichaPrimero, ObtenerContenidoA item)
        {
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
            if (RestandoColumnas  == -1 && RestadoFila ==1) //diagonal inferior izquirda
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

        [HttpPost]
        public ActionResult Dato(int fila, string columna) //esto es para el tablero solitario (computadora vs usuario)
        {

            /*coloco if para saber si la variable Session["turno"] == null
            que a su vez sea un numero 1 el cual es el turno de la ficha color blanca*/
            if (Session["turno"] == null) //fichas color negro
            {
                //ViewBag.Mensaje = "Turno fichas blancas";
                var NfilaV = fila;
                var NcolumnaV = columna.ToUpper();
                System.Diagnostics.Debug.WriteLine(NcolumnaV);
                ObtenerContenidoA f1 = new ObtenerContenidoA();
                f1.color = "negro";
                f1.fila = NfilaV;
                f1.columna = NcolumnaV;
                List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                ficha.Add(f1);
                int valor = 1;
                Session["turno"] = valor; //colocar un contador de los turnos para que no este ejecutando turnos que ya realizo
                int contadorf1 = 0;
                int con = 0;
                if (contadorf1 == 0)
                {
                    if (NfilaV == 4 && NcolumnaV == "C") //ficha blanca en la primera posicion del tablero (4D)
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila == 4 && item.columna == "D" && item.color == "blanco")
                            {
                                item.color = "negro";
                            }
                            //turno compu 1
                            if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "D" && item2.color == "negro")
                                    {
                                        con = 1;
                                        item2.color = "blanco";
                                    }
                                }
                            }
                            //fin turno compu 1
                            //turno compu2
                            if (item.fila == 5 && item.columna == "D" && item.color == "blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "E" && item2.color == "negro")
                                    {
                                        con = 2;
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila == 5 && item2.columna == "F" && item2.color == "negro")
                                    {
                                        con = 2;
                                        item2.color = "blanco";
                                    }
                                }
                            }
                            //fin turno compu2
                        }
                        //turno compu 1
                        if (con == 1)
                        {
                            ObtenerContenidoA f2 = new ObtenerContenidoA();
                            f2.color = "blanco";
                            f2.fila = 5;
                            f2.columna = "C";
                            List<ObtenerContenidoA> ficha2 = (List<ObtenerContenidoA>)Session["juego"];
                            ficha2.Add(f2);
                        }
                        //fin turno compu 1
                        //turno compu2
                        if (con == 2)
                        {
                            ObtenerContenidoA f2 = new ObtenerContenidoA();
                            f2.color = "blanco";
                            f2.fila = 5;
                            f2.columna = "G";
                            List<ObtenerContenidoA> ficha2 = (List<ObtenerContenidoA>)Session["juego"];
                            ficha2.Add(f2);
                        }
                        //fin turno compu2
                    }

                    if (NfilaV == 5 && NcolumnaV == "F") //ficha blanca en la primera posicion del tablero (4D)
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                            {
                                item.color = "negro";
                            }
                            if (item.fila == 5 && item.columna == "D" && item.color == "negro")
                            {

                                item.color = "blanco";
                            }

                        }
                        ObtenerContenidoA f3 = new ObtenerContenidoA();
                        f3.color = "blanco";
                        f3.fila = 6;
                        f3.columna = "D";
                        List<ObtenerContenidoA> ficha3 = (List<ObtenerContenidoA>)Session["juego"];
                        ficha3.Add(f3);

                    }


                    contadorf1++;
                    System.Diagnostics.Debug.WriteLine(contadorf1 + "primer turno");
                }

            }
            else //turno para fichas blancas (computadora)
            {
                
                var NfilaV = fila;
                var NcolumnaV = columna.ToUpper();
                System.Diagnostics.Debug.WriteLine(NcolumnaV);
                ObtenerContenidoA f1 = new ObtenerContenidoA();
                f1.color = "negro";
                f1.fila = NfilaV;
                f1.columna = NcolumnaV;
                List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                ficha.Add(f1);
                Session["turno"] = null; //colocar un contador de los turnos para que no este ejecutando turnos que ya realizo
                int contadorf1 = 0;
                int con = 0;
                if (contadorf1 == 0)
                {
                    if (NfilaV == 4 && NcolumnaV == "C") //ficha blanca en la primera posicion del tablero (4D)
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila == 4 && item.columna == "D" && item.color == "blanco")
                            {
                                item.color = "negro";
                            }
                            //turno compu 1
                            if (item.fila==5 && item.columna=="E" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "D" && item2.color == "negro")
                                    {
                                        con = 1;
                                        item2.color = "blanco";
                                    }
                                }
                            }
                            //fin turno compu 1
                            //turno compu2
                            if (item.fila==5 && item.columna=="D" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "E" && item2.color == "negro")
                                    {
                                        con = 2;
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila == 5 && item2.columna == "F" && item2.color == "negro")
                                    {
                                        con = 2;
                                        item2.color = "blanco";
                                    }
                                }
                            }
                            //fin turno compu2
                        }
                        //turno compu 1
                        if (con==1)
                        {
                            ObtenerContenidoA f2 = new ObtenerContenidoA();
                            f2.color = "blanco";
                            f2.fila = 5;
                            f2.columna = "C";
                            List<ObtenerContenidoA> ficha2 = (List<ObtenerContenidoA>)Session["juego"];
                            ficha2.Add(f2);
                        }
                        //fin turno compu 1
                        //turno compu2
                        if (con==2)
                        {
                            ObtenerContenidoA f2 = new ObtenerContenidoA();
                            f2.color = "blanco";
                            f2.fila = 5;
                            f2.columna = "G";
                            List<ObtenerContenidoA> ficha2 = (List<ObtenerContenidoA>)Session["juego"];
                            ficha2.Add(f2);
                        }
                        //fin turno compu2
                    }

                    if (NfilaV == 5 && NcolumnaV == "F") //ficha blanca en la primera posicion del tablero (4D)
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                            {
                                item.color = "negro";
                            }
                            if (item.fila == 5 && item.columna == "D" && item.color == "negro")
                            {

                                item.color = "blanco";
                            }

                        }
                        ObtenerContenidoA f3 = new ObtenerContenidoA();
                        f3.color = "blanco";
                        f3.fila = 6;
                        f3.columna = "D";
                        List<ObtenerContenidoA> ficha3 = (List<ObtenerContenidoA>)Session["juego"];
                        ficha3.Add(f3);

                    }


                    contadorf1++;
                    System.Diagnostics.Debug.WriteLine(contadorf1 + "primer turno");
                }

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
    }
}