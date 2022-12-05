using AutosColombia.ModeloDatos.ModeloMensajes;
using AutosColombia.ModeloDatos.ModelosGenerales;
using AutosColombia.ModeloDatos.ModeloVehiculo;
using AutosColombia.Negocio.ManejadorVehiculo;
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

        [HttpPost]
        public ActionResult GeneraDatosTablaConsultasVehiculos(DataTableParameter parametrosDataTable)
        {
            try
            {
                long cantidadRegistrosTotal;
                List<GestionVehiculos> listaConsultaConceptosRelatoria = ManejadorVehiculo.ObtenerInstancia.ConsultaVehiculoPorFiltros(parametrosDataTable, out cantidadRegistrosTotal);

                return Json(new { draw = parametrosDataTable.draw, recordsFiltered = cantidadRegistrosTotal, recordsTotal = cantidadRegistrosTotal, data = listaConsultaConceptosRelatoria });
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public ActionResult EditarVehiculo(string numeroDocumento)
        {
            if (numeroDocumento != null)
            {
                GestionVehiculos concepto = ManejadorVehiculo.ObtenerInstancia.ConsultarVehiculoXDocumento(numeroDocumento);
                return PartialView("_EditarBiometriaReintentos", concepto);
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditarVehiculo(string Tipo_Documento, string Numero_Identificacion, string ID_transaccion, string Cant_Intentos)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                int intentos = 0;

                if (!string.IsNullOrEmpty(Cant_Intentos))
                {
                    intentos = Int32.Parse(Cant_Intentos);
                }

                GestionVehiculos collection = new GestionVehiculos()
                {
                    Placa = Tipo_Documento,
                    Documento_Usuario = Numero_Identificacion,
                    NumeroCelda = intentos
                };
                // TODO: Add update logic here
                var resultado = ManejadorVehiculo.ObtenerInstancia.ModificarVehiculo(collection);
                return Json(mensaje = new Mensaje { Codigo = "", Mensaje_Generico = "Usuario Creado Exitosamente" + "", Tipo = Mensaje_Tipo.SUCCESS }, JsonRequestBehavior.AllowGet);
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