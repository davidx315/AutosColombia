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

            var usuario =  ManejadorUsuario.ObtenerInstancia.ConsultarPorDocumentoContraseña(login.username, login.password);

            if (usuario != null)
            {
                if (usuario.Nombre_Rol.Equals("Administrador"))
                {
                    return View("../Administrador/RegistroUsuario");
                }
                else
                {
                    return View("../Clientes/GestionDatosCliente", usuario);
                }
            }
            else
            {
                return View();
            }
        }
    }
}