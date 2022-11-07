using AutosColombia.ModeloDatos.ModeloUsuario;
using AutosColombia.ServicioDatos.OperacionesDatos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.Negocio.ManejadorUsuario
{
    public class ManejadorUsuario
    {
        private static readonly Lazy<ManejadorUsuario> instanciaUnica = new Lazy<ManejadorUsuario>(() => new ManejadorUsuario());

        public static ManejadorUsuario ObtenerInstancia
        {
            get
            {
                return instanciaUnica.Value;
            }
        }


        public Usuario ConsultarPorDocumento(string numeroDocumento)
        {
            Usuario tramite = CrudUsuario.ObtenerInstancia.ConsultaByDocumentoClave(numeroDocumento);
            return tramite;
        }

        public bool ConsultarPorDocumentoContraseña(string numeroDocumento, string claveUsuario)
        {
            Usuario tramite = CrudUsuario.ObtenerInstancia.ConsultaByDocumentoClave(numeroDocumento, claveUsuario);
            if (tramite != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CrearUsuario(Usuario collection)
        {
            try
            {
                bool usuarioGuardado = CrudUsuario.ObtenerInstancia.CrearUsuario(collection);
                if (usuarioGuardado)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
