using AutosColombia.Comunes.BaseDeDatos;
using AutosColombia.ModeloDatos.ModeloUsuario;
using AutosColombia.ModeloDatos.ModeloVehiculo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.ServicioDatos.OperacionesDatos.Vehiculos
{
    public class CrudVehiculos
    {
        private static readonly Lazy<CrudVehiculos> instance = new Lazy<CrudVehiculos>(() => new CrudVehiculos());

        public CrudVehiculos() { }

        public static CrudVehiculos ObtenerInstancia
        {
            get
            {
                return instance.Value;
            }
        }

        public GestionVehiculos ConsultaByPlaca(string documento)
        {
            GestionVehiculos registro = new GestionVehiculos();

            try
            {
                var document = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos");

                if (!string.IsNullOrEmpty(documento))
                {
                    registro = (from h in document.AsQueryable()
                                where (h.Documento_Usuario.Equals(documento))
                                select h).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return registro;
        }

        public bool ActualizarVehiculo(GestionVehiculos vehiculo)
        {
            bool registro = false;
            try
            {
                var document = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos");

                document.UpdateOne(
                        Builders<GestionVehiculos>.Filter.Eq(c => c.Placa, vehiculo.Placa),
                        Builders<GestionVehiculos>.Update
                            .Set(x => x.Modelo_Vehiculo, vehiculo.Modelo_Vehiculo)
                            .Set(x => x.Placa, vehiculo.Placa)
                            .Set(x => x.Documento_Usuario, vehiculo.Documento_Usuario)
                            .Set(x => x.NumeroCelda, vehiculo.NumeroCelda)
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

        public void crearRegistroVehiculo(GestionVehiculos gestionVehiculos)
        {

            bool registro = false;
            try
            {
                var document = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos");
                var query = (from h in document.AsQueryable()
                             where (h.Placa.Equals(gestionVehiculos.Placa)) //validar
                             select h).ToList();

                if (query.Count().Equals(0))
                {
                    //Inserta
                    document.InsertOne(gestionVehiculos);
                    registro = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
