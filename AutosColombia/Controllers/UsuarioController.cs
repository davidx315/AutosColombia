using AutosColombia.ModeloDatos.ModeloMensajes;
using AutosColombia.ModeloDatos.ModeloUsuario;
using AutosColombia.Negocio.ManejadorUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutosColombia.Controllers
{
    public class UsuarioController : Controller
    {

        public ActionResult RegistroUsuario()
        {
            return View("../Administrador/RegistroUsuario");
        }

        public JsonResult CrearTramitesBiometria(Usuario usuario)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                Usuario usuarioExistente = ManejadorUsuario.ObtenerInstancia.ConsultarPorDocumento(usuario.Documento_Usuario);
                if (usuarioExistente == null || !usuarioExistente.Documento_Usuario.Equals(usuario.Documento_Usuario))
                {
                    // TODO: Add insert logic here
                    bool Response = ManejadorUsuario.ObtenerInstancia.CrearUsuario(usuario);
                    if (Response)
                    {
                        return Json(mensaje = new Mensaje { Codigo = "", Mensaje_Generico = "Usuario Creado Exitosamente" + "", Tipo = Mensaje_Tipo.SUCCESS }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(mensaje = new Mensaje { Codigo = "", Mensaje_Generico = "El Tramite a Crear ya se encuntra en la coleccion MDM_TRAMITES_GESTION_BIOMETRIA" + "", Tipo = Mensaje_Tipo.ERROR }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = new Mensaje { Codigo = "", Mensaje_Generico = "El usuario ya se encuentra registrado" + "", Tipo = Mensaje_Tipo.ERROR };
                    return Json(mensaje, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                mensaje = new Mensaje { Codigo = "", Mensaje_Generico = "El Tramite a Crear ya se encuntra en la coleccion MDM_TRAMITES_GESTION_BIOMETRIA" + "", Tipo = Mensaje_Tipo.ERROR }; //No se pudo realizar la carga del archivo
                mensaje.Mensaje_Generico += " " + ex.Message;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }
        }
    }
}