using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutosColombia.ModeloDatos.ModeloUsuario;
using AutosColombia.Negocio.ManejadorUsuario;

namespace AutosColombia.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Index(LoginAutosColombia login)
        {
            return View();
        }

        public ActionResult ValidarUsuario(LoginAutosColombia login) {

            
            if (ManejadorUsuario.ObtenerInstancia.ConsultarPorDocumentoContraseña(login.username, login.password))
            {
                return View("../Administrador/RegistroUsuario");
            }
            else
            {
                return View();
            }
        }
    }
}