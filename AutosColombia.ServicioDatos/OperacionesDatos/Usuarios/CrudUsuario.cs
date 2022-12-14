using AutosColombia.Comunes.BaseDeDatos;
using AutosColombia.ModeloDatos.ModeloUsuario;
using AutosColombia.ModeloDatos.ModeloVehiculo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.ServicioDatos.OperacionesDatos.Usuarios
{
    public class CrudUsuario
    {
        private static readonly Lazy<CrudUsuario> instance = new Lazy<CrudUsuario>(() => new CrudUsuario());

        public CrudUsuario() { }

        public static CrudUsuario ObtenerInstancia
        {
            get
            {
                return instance.Value;
            }
        }
        public Usuario ConsultaByDocumentoClave(string numeroDocumento, string claveUsuario = null)
        {
            Usuario registro = new Usuario();

            try
            {
                var document = MongoContext.Database().GetCollection<Usuario>("Usuario");

                if (!string.IsNullOrEmpty(claveUsuario))
                {
                    registro = (from h in document.AsQueryable()
                                where (h.Documento_Usuario.Equals(numeroDocumento) && h.Contraseña.Equals(claveUsuario))
                                select h).FirstOrDefault();
                }
                else
                {
                    registro = (from h in document.AsQueryable()
                                where (h.Documento_Usuario.Equals(numeroDocumento))
                                select h).FirstOrDefault();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return registro;
        }

        public bool CrearUsuario(Usuario usuario)
        {
            bool registro = false;
            try
            {
                var document = MongoContext.Database().GetCollection<Usuario>("Usuario");
                var query = (from h in document.AsQueryable()
                             where (h.Documento_Usuario.Equals(usuario.Documento_Usuario)) //validar
                             select h).ToList();

                if (query.Count().Equals(0))
                {
                    //Inserta
                    document.InsertOne(usuario);
                    registro = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return registro;
        }


        public bool ActualizarUsuario(Usuario usuario)
        {
            bool registro = false;
            try
            {
                var document = MongoContext.Database().GetCollection<Usuario>("Usuario");

                document.UpdateOne(
                        Builders<Usuario>.Filter.Eq(c => c.Documento_Usuario, usuario.Documento_Usuario),
                        Builders<Usuario>.Update
                            .Set(x => x.Apellido_Usuario, usuario.Apellido_Usuario)
                            .Set(x => x.Documento_Usuario, usuario.Documento_Usuario)
                            .Set(x => x.Contraseña, usuario.Contraseña)
                            .Set(x => x.Nombre_Rol, usuario.Nombre_Rol)
                    );

                registro = true;
                return registro;
            }
            catch (Exception ex)
            {
                return registro;
                throw ex;
            }
        }
    }
}
