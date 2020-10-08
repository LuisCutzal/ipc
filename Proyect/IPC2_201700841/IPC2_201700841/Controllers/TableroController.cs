﻿using System;
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
                    Session["turno"] =cero;

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

        [HttpPost]
        public ActionResult InicioVersus(int fila,string columna) //tablero modo versus
        {
            //try
            
                if (Session["turno"] == null) //fichas color negro
                {
                    ViewBag.Mensaje = "Turno fichas blancas";
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
                    if (contadorf1==0)
                    {
                        if (NfilaV == 4 && NcolumnaV == "C") //ficha blanca en la primera posicion del tablero (4D)
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "D" && item.color == "blanco")
                                {
                                    item.color="negro";
                            }
                        }
                    }
                        if (NfilaV == 3 && NcolumnaV == "D")//ficha blanca en la primera posicion del tablero (4D)
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "D" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            //contadorf1++;
                            //System.Diagnostics.Debug.WriteLine(contadorf1);
                            }
                        }
                        if (NfilaV == 5 && NcolumnaV == "F")//ficha blanca en la primera posicion del tablero (5E)
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        //contadorf1++;
                        //System.Diagnostics.Debug.WriteLine(contadorf1);
                        }
                        if (NfilaV == 6 && NcolumnaV == "E")//ficha blanca en la primera posicion del tablero (5E)
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }

                            }
                        }
                        contadorf1++;
                        System.Diagnostics.Debug.WriteLine(contadorf1+"primer turno");
                    }
                    if (contadorf1==1)
                    {
                        //INICIO PRIMER TURNO NEGRO 4C Y PRIMER TURNO BLANCO 3C
                        if (NfilaV==2 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 3 && item.columna == "C" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV==3 && NcolumnaV=="D")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>) 
                            {
                                if (item.fila == 4 && item.columna == "D" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV == 5 && NcolumnaV == "F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV == 6 && NcolumnaV == "E")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                                //if (item.fila == 5 && item.columna == "D" && item.color == "blanco") //veremos
                                //{
                                //    item.color = "negro";
                                //}
                            }
                        }
                        //FIN PRIMER TURNO NEGRO 4C Y PRIMER TURNO BLANCO 3C

                        //INICIO PRIMER TURNO NEGRO 4C Y PRIMER TURNO BLANCO 3E
                        if (NfilaV==2 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 3 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV==4 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }

                        }
                        if (NfilaV==6 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV==3 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "E" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }

                        }
                        //FIN PRIMER TURNO NEGRO 4C Y PRIMER TURNO BLANCO 3E

                        //INICIO PRIMER TURNO NEGRO 4C Y PRIMER TURNO BLANCO 5C

                        if (NfilaV==6 && NcolumnaV=="E")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                           {
                                if (item.fila==4 && item.columna=="C" && item.color=="negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 5 && item2.columna == "D" && item2.color == "blanco") //veremos
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            if (item.fila==4 && item.columna=="E" && item.color=="negro")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "E" && item2.color == "blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                }
                            }
                            }
                        }
                        if (NfilaV==6 && NcolumnaV=="D")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "D" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }

                        }
                        if (NfilaV == 6 && NcolumnaV == "C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                //if (item.fila==3 && item.columna=="D" && item.color=="negro")
                                //{
                                //    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                //    {
                                        if (item.fila == 5 && item.columna == "D" && item.color == "blanco")
                                        {
                                            item.color = "negro";
                                        }
                                //    }
                                //}
                            }
                        }
                        if (NfilaV == 6 && NcolumnaV == "C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="C" && item.color=="negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 5 && item2.columna == "C" && item2.color == "blanco") //veremos
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            }
                        }
                        if (NfilaV==6 && NcolumnaV=="B") //valores que ingreso
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "C" && item.color == "blanco") //valor de la ficha que busco
                                {
                                    item.color = "negro";
                                }
                            }

                        }
                        //FIN PRIMER TURNO NEGRO 4C Y PRIMER TURNO BLANCO 5C

                        // INICIO PRIMER TURNO NEGRO 3D Y PRIMER TURNO BLANCO 3C
                        if (NfilaV==3 && NcolumnaV=="B")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 3 && item.columna == "C" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        //FIN PRUMER TURNO NEGRO 3D Y PRIMER TURNO BLANCO 3C
                        if (NfilaV == 5 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==5 && item.columna == "D" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            }

                        }
                        if (NfilaV==7 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 6 && item.columna == "D" && item.color == "blanco")
                                {
                                    item.color = "negro";
                                }
                            if (item.fila==4 && item.columna=="F" && item.color=="negro")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==5 && item2.columna=="E" && item.color=="blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                    if (item2.fila == 4 && item2.columna == "D" && item.color == "blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                }
                            }
                            }

                        }
                        if (NfilaV==6 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==6 && item.columna=="E" && item.color=="negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==6 && item2.columna=="D" && item2.color=="blanco")
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            }
                        }
                        if (NfilaV==3 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="D" && item.color=="blanco")
                                {
                                    item.color = "negro";
                                }
                            }

                        }
                        //siguiente lado 
                        if (NfilaV == 6 && NcolumnaV=="G")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==6 && item.columna=="F" && item.color=="blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV==3 && NcolumnaV=="E")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="E" && item.color=="blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV==3 && NcolumnaV=="G")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="F" && item.color=="blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV==7 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==6 && item.columna=="F" && item.color=="blanco")
                                {
                                    item.color = "negro";
                                }
                            }
                        }
                        if (NfilaV==3 && NcolumnaV=="D")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==5 && item.columna=="F" && item.color=="negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==4 && item2.columna=="E" && item2.color=="blanco")
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            }
                        }
                        if (NfilaV==3 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "F" && item.color == "negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==4 && item2.columna=="F" && item2.color=="blanco")
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            }

                        }
                        //fin siguiente lado
                        contadorf1++;
                        System.Diagnostics.Debug.WriteLine(contadorf1 + "segundo turno");
                    }
                    if (contadorf1==2)
                    {
                        if (NfilaV==7 && NcolumnaV=="D")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==5 && item.columna=="D" && item.color=="negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==6 && item2.columna=="D" && item2.color=="blanco")
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            }
                        }
                        contadorf1++;
                        System.Diagnostics.Debug.WriteLine(contadorf1 + "segundo turno");
                    }
                    if (contadorf1==3)
                    {
                        if (NfilaV==3 && NcolumnaV=="E")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila== 6 && item.columna=="E" && item.color=="negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 5 && item2.columna == "E" && item2.color == "blanco")
                                        {
                                            item2.color = "negro";
                                        }
                                        //if (item2.fila == 4 && item2.columna == "E" && item2.color == "blanco")
                                        //{
                                        //    item2.color = "negro";
                                        //}
                                    }
                                }
                            }
                        }
                    if (NfilaV==6 && NcolumnaV=="F")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==4 && item.columna=="F" && item.color=="negro")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "F" && item2.color == "blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV==6 && NcolumnaV=="G")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==4 && item.columna=="E" && item.color=="negro")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "F" && item2.color == "blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV==5 && NcolumnaV=="H" )
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==5 && item.columna=="D" && item.color=="negro")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "E" && item2.color == "blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                    if (item2.fila == 5 && item2.columna == "F" && item2.color == "blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                    if (item2.fila == 5 && item2.columna == "G" && item2.color == "blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                }
                            }
                        }
                    }
                        contadorf1++;
                        System.Diagnostics.Debug.WriteLine(contadorf1 + "segundo turno");
                    }
                    if (contadorf1==4)
                    {
                        if (NfilaV==6 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="F" && item.color=="negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==5 && item2.columna=="F" && item2.color=="blanco")
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            }
                        }
                    if (NfilaV==5 && NcolumnaV=="C")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==5 && item.columna=="F" && item.color=="negro")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==5 && item2.columna=="E" && item2.color=="blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                    if (item2.fila==5 && item2.columna=="D" && item2.color=="blanco")
                                    {
                                        item2.color = "negro";
                                    }
                                }
                            }
                        }
                    }
                        contadorf1++;
                        System.Diagnostics.Debug.WriteLine(contadorf1 + "segundo turno");
                    }
                    if (contadorf1 == 5)
                    {
                        if (NfilaV == 6 && NcolumnaV == "G")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "E" && item.color == "negro")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 5 && item2.columna == "F" && item2.color == "blanco")
                                        {
                                            item2.color = "negro";
                                        }
                                    }
                                }
                            }
                        }
                    contadorf1++;
                    System.Diagnostics.Debug.WriteLine(contadorf1 + "segundo turno");
                    }




            }
                            
                else //turno para fichas blancas
                {
                    ViewBag.Mensaje = "Turno fichas negras";
                    var NfilaV = fila;
                    var NcolumnaV = columna.ToUpper();
                    ObtenerContenidoA f1 = new ObtenerContenidoA();
                    f1.color = "blanco";
                    f1.fila = NfilaV;
                    f1.columna = NcolumnaV;
                    List<ObtenerContenidoA> ficha = (List<ObtenerContenidoA>)Session["juego"];
                    ficha.Add(f1);
                    Session["turno"] = null;
                    int contadorf2 = 0;
                    if (contadorf2==0)
                    {
                        if (NfilaV == 5 && NcolumnaV == "C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 5 && item.columna == "D" && item.color == "negro")
                                {
                                    item.color = "blanco";
                                }
                            }

                        }
                        if (NfilaV==6 && NcolumnaV=="D")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="F" && item.color=="blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==5 && item2.columna=="E" && item2.color=="negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                                if (item.fila == 4 && item.columna == "D" && item.color == "blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 5 && item2.columna == "D" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }

                            }
                        }
                        if (NfilaV == 3 && NcolumnaV == "E")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "E" && item.color == "negro")
                                {
                                    item.color = "blanco";
                                }
                            }

                        }
                        if (NfilaV == 4 && NcolumnaV == "F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "E" && item.color == "negro")
                                {
                                    item.color = "blanco";
                                }
                            if (item.fila==4 && item.columna=="D" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 6 && item2.columna == "D" && item2.color == "blanco")
                                    {
                                        foreach (var item3 in Session["juego"] as List<ObtenerContenidoA>)
                                        {
                                            if (item3.fila == 4 && item3.columna == "E" && item3.color == "negro")
                                            {
                                                item3.color = "blanco";
                                            }
                                            if (item3.fila == 5 && item3.columna == "E" && item3.color == "negro")
                                            {
                                                item3.color = "blanco";
                                            }
                                        }
                                    }
                                }
                            }
                            }

                        }
                        if (NfilaV == 3 && NcolumnaV =="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "D" && item.color == "negro")
                                {
                                    item.color = "blanco";
                                }
                            }
                        }
                        if (NfilaV==6 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="D" && item.color=="blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 5 && item2.columna == "E" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            }
                        }
                        contadorf2++;
                        System.Diagnostics.Debug.WriteLine(contadorf2 + "primer turno");
                    }
                    if (contadorf2==1)
                    {
                        if (NfilaV==5 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                            if (item.fila==5 && item.columna=="E" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==3 && item2.columna=="C" && item.color=="blanco")
                                    {
                                        foreach (var item3 in Session["juego"] as List<ObtenerContenidoA>)
                                        {
                                            if (item3.fila == 4 && item3.columna == "C" && item3.color == "negro")
                                            {
                                                item3.color = "blanco";
                                            }
                                            if (item3.fila == 5 && item3.columna == "D" && item3.color == "negro") //veremos
                                            {
                                                item3.color = "blanco";
                                            }
                                        }
                                    }
                                }
                            }
                            }
                        }
                        //if (NfilaV==3 && NcolumnaV=="E")
                        //{
                        //    foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        //    {
                        //        if (item.fila==3 && item.columna=="D" && item.color=="negro")
                        //        {
                        //            item.color = "blanco";
                        //        }
                        //        if (item.fila == 4 && item.columna == "E" && item.color == "negro") //veremos
                        //        {
                        //            item.color = "blanco";
                        //        }
                        //    }
                        //}
                        if (NfilaV==5 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==5 && item.columna=="E" && item.color=="blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==4 && item2.columna =="C" && item2.color=="negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                        if (item2.fila == 5 && item2.columna == "D" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                        if (item2.fila == 2 && item2.columna == "C" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                        if (item2.fila == 3 && item2.columna == "C" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            }
                        }
                        if (NfilaV==4 && NcolumnaV=="C")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "E" && item.color == "blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 4 && item2.columna == "D" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            }
                        }
                        if (NfilaV==2 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 4 && item.columna == "F" && item.color == "blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 3 && item2.columna == "F" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            }
                        }
                        contadorf2++;
                        System.Diagnostics.Debug.WriteLine(contadorf2 + "primer turno");
                    }
                    if (contadorf2==2)
                    {
                        if (NfilaV==4 && NcolumnaV=="B")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="F" && item.color=="blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==4 &&item2.columna=="E" &&item2.color=="negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                        if (item2.fila == 4 && item2.columna == "D" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                        if (item2.fila == 4 && item2.columna == "C" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            }
                        }
                        if (NfilaV==5 && NcolumnaV=="G")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==5 && item.columna=="D" && item.color=="blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==5 && item2.columna=="E" && item2.color=="negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                        if (item2.fila==5 && item2.columna=="F" && item2.color=="negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "F" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                            }
                        }
                    if (NfilaV ==2 && NcolumnaV=="B")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==5 && item.columna=="E" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==4 && item2.columna=="D" && item2.color=="negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila==3 && item2.columna=="C" && item2.color=="negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV == 5 && NcolumnaV == "B")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila == 5 && item.columna == "E" && item.color == "blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "D" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila == 5 && item2.columna == "C" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV==3 && NcolumnaV=="G")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==5 && item.columna=="E" && item.color=="blano")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==4 && item2.columna=="F" && item2.color=="negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV==4 && NcolumnaV=="G")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==4 && item.columna=="D" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==4 && item2.columna=="E" && item2.color=="negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila == 4 && item2.columna == "F" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV==2 && NcolumnaV=="D")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==5 && item.columna=="D" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==3 && item2.columna=="D" && item2.color=="negro")
                                    {
                                        item2.color="blanco";
                                    }
                                    if (item2.fila == 4 && item2.columna == "D" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV==3 && NcolumnaV=="F")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila == 5 && item.columna == "D" && item.color == "blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 4 && item2.columna == "E" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV == 5 && NcolumnaV == "F")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila == 5 && item.columna == "D" && item.color == "blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila == 5 && item2.columna == "E" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                        contadorf2++;
                        System.Diagnostics.Debug.WriteLine(contadorf2 + "primer turno");
                    }
                    if (contadorf2==3)
                    {
                        if (NfilaV==5 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila == 3 && item.columna == "F" && item.color == "blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila == 4 && item2.columna == "F" && item2.color == "negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            }
                        }
                        contadorf2++;
                        System.Diagnostics.Debug.WriteLine(contadorf2 + "primer turno");
                    }
                    if (contadorf2==4)
                    {
                        if (NfilaV==6 && NcolumnaV=="F")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==4 && item.columna=="F" && item.color=="blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==6 && item2.columna=="D" && item2.color=="blanco")
                                        {
                                            foreach (var item3 in Session["juego"] as List<ObtenerContenidoA>)
                                            {
                                                if (item3.fila==5 && item3.columna=="F" && item3.color=="negro")
                                                {
                                                    item3.color = "blanco";
                                                }
                                                if (item3.fila == 6 && item3.columna == "E" && item3.color == "negro")
                                                {
                                                    item3.color = "blanco";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    if (NfilaV==7 && NcolumnaV=="G")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==5 && item.columna=="G" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==6 && item2.columna=="G" && item.color=="negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    if (NfilaV==5 && NcolumnaV=="B")
                    {
                        foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                        {
                            if (item.fila==5 && item.columna=="G" && item.color=="blanco")
                            {
                                foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                {
                                    if (item2.fila==5 && item2.columna=="F" && item2.color=="negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila == 5 && item2.columna == "E" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila == 5 && item2.columna == "D" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                    if (item2.fila == 5 && item2.columna == "C" && item2.color == "negro")
                                    {
                                        item2.color = "blanco";
                                    }
                                }
                            }
                        }
                    }
                    contadorf2++;
                    System.Diagnostics.Debug.WriteLine(contadorf2 + "primer turno");
                    }
                    if (contadorf2==5)
                    {
                        if (NfilaV==3 && NcolumnaV=="D")
                        {
                            foreach (var item in Session["juego"] as List<ObtenerContenidoA>)
                            {
                                if (item.fila==5 && item.columna=="D" && item.color=="blanco")
                                {
                                    foreach (var item2 in Session["juego"] as List<ObtenerContenidoA>)
                                    {
                                        if (item2.fila==4 && item2.columna=="D" && item2.color=="negro")
                                        {
                                            item2.color = "blanco";
                                        }
                                    }
                                }
                            }
                        }
                        contadorf2++;
                        System.Diagnostics.Debug.WriteLine(contadorf2 + "primer turno");
                    }
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