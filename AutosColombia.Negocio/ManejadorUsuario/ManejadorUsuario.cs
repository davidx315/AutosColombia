using AutosColombia.ModeloDatos.ModeloUsuario;
using AutosColombia.ModeloDatos.ModeloVehiculo;
using AutosColombia.ServicioDatos.OperacionesDatos.Usuarios;
using AutosColombia.ServicioDatos.OperacionesDatos.Vehiculos;
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

        public UsuariosRequest ConsultarPorDocumentoContraseña(string numeroDocumento, string claveUsuario)
        {
            Usuario usuario = CrudUsuario.ObtenerInstancia.ConsultaByDocumentoClave(numeroDocumento, claveUsuario);
            GestionVehiculos vehiculos = new GestionVehiculos();
            if (!usuario.Nombre_Rol.Equals("Administrador"))
            {
                vehiculos = CrudVehiculos.ObtenerInstancia.ConsultaByPlaca(usuario.Documento_Usuario);
            }
            UsuariosRequest usuariosRequest = new UsuariosRequest();
            if (usuario.Nombre_Rol.Equals("Administrador"))
            {
                usuariosRequest = new UsuariosRequest()
                {
                    Apellido_Usuario = usuario.Apellido_Usuario,
                    Nombre_Usuario = usuario.Nombre_Usuario,
                    Documento_Usuario = usuario.Documento_Usuario,
                    Contraseña = usuario.Contraseña,
                    Modelo_Vehiculo = "",
                    Placa = "",
                    Nombre_Rol = usuario.Nombre_Rol
                };
            }
            else
            {
                usuariosRequest = new UsuariosRequest()
                {
                    Apellido_Usuario = usuario.Apellido_Usuario,
                    Nombre_Usuario = usuario.Nombre_Usuario,
                    Documento_Usuario = usuario.Documento_Usuario,
                    Contraseña = usuario.Contraseña,
                    Modelo_Vehiculo = vehiculos.Modelo_Vehiculo,
                    Placa = vehiculos.Placa,
                    Nombre_Rol = usuario.Nombre_Rol
                };
            }
            

            return usuariosRequest;
        }

        public bool CrearUsuario(UsuariosRequest collection)
        {
            try
            {
                bool usuarioGuardado = CrudUsuario.ObtenerInstancia.CrearUsuario(InformacionDatosUsuario(collection));
                if (usuarioGuardado)
                {
                    if (!string.IsNullOrEmpty(collection.Modelo_Vehiculo)  && !string.IsNullOrEmpty(collection.Placa))
                    {                     
                        CrudVehiculos.ObtenerInstancia.crearRegistroVehiculo(InformacionDatosVehiculo(collection));
                    }

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

        private GestionVehiculos InformacionDatosVehiculo(UsuariosRequest collection)
        {
            GestionVehiculos vehiculos = new GestionVehiculos()
            {
                Modelo_Vehiculo = collection.Modelo_Vehiculo,
                Placa = collection.Placa,
                Documento_Usuario = collection.Documento_Usuario
            };

            return vehiculos;
        }

        private Usuario InformacionDatosUsuario(UsuariosRequest collection)
        {
            Usuario datoUsuario = new Usuario()
            {
                Nombre_Usuario = collection.Nombre_Usuario,
                Apellido_Usuario = collection.Apellido_Usuario,
                Documento_Usuario = collection.Documento_Usuario,
                Contraseña = collection.Contraseña,
                Nombre_Rol = collection.Nombre_Rol
            };

            return datoUsuario;
        }

        public bool ModificarUsuario(UsuariosRequest usuario)
        {
            try
            {
                CrudUsuario.ObtenerInstancia.ActualizarUsuario(InformacionDatosUsuario(usuario));               
                if (CrudVehiculos.ObtenerInstancia.ConsultaByPlaca(usuario.Documento_Usuario) != null)
                {
                    CrudVehiculos.ObtenerInstancia.ActualizarVehiculo(InformacionDatosVehiculo(usuario));
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
