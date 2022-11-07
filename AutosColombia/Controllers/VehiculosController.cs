using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutosColombia.Controllers
{
    public class VehiculosController : Controller
    {
        [AllowAnonymous]
        public ActionResult ConsultaVehiculo()
        {
            return View("../Administrador/ConsultaVehiculos");
        }
    }
}