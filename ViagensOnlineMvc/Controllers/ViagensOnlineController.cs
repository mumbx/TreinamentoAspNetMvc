using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViagensOnlineMvc.Db;

namespace ViagensOnlineMvc.Controllers
{
    public class ViagensOnLineController : Controller
    {
        //
        // Início
        //
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Destinos()
        {
            using (var db = new ViagensOnLineDb())
            {
                return View(db.Destinos.ToArray());
            }
        }
    }
}