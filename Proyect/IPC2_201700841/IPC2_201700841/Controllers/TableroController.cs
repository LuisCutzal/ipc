using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPC2_201700841.Controllers
{
    public class TableroController : Controller
    {
        // GET: Tablero
        public ActionResult TableroSolitario()
        {
            return View();
        }

        public ActionResult TableroVersus()
        {
            return View();
        }

        public ActionResult TableroTorneo()
        {
            return View();
        }
    }
}