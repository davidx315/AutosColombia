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

        public List<GestionVehiculos> ConsultaVehiculoPorFiltros(string TipoDocumento, string NumeroDocumento, out long recordsTotal, SortDefinition<GestionVehiculos> ordenamiento, int start, int length, string sSearch = null)
        {
            List<GestionVehiculos> document = new List<GestionVehiculos>();

            try
            {

                var builder = Builders<GestionVehiculos>.Filter;
                var filter = builder.Empty;

                if (!string.IsNullOrEmpty(sSearch))
                {
                    filter = filter & builder.Or(builder.Regex(c => c.Placa, "/" + sSearch + "/i"),
                                                                      builder.Regex(c => c.Documento_Usuario, "/" + sSearch + "/i"));
                }

                if (!string.IsNullOrEmpty(TipoDocumento))
                {
                    filter = filter & builder.Eq(x => x.Placa, TipoDocumento);
                }
                if (!string.IsNullOrEmpty(NumeroDocumento))
                {
                    filter = filter & builder.Eq(x => x.Documento_Usuario, NumeroDocumento);
                }
                if (ordenamiento == null)
                {
                    document = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos").Find(filter).Skip(start).Limit(length).ToList();
                    recordsTotal = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos").Find(filter).CountDocuments();
                }
                else
                {
                    document = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos").Find(filter).Sort(ordenamiento).Skip(start).Limit(length).ToList();
                    recordsTotal = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos").Find(filter).CountDocuments();
                }

                return document;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return registro;
        }

        public GestionVehiculos ConsultaByDocumento(string numeroDocumento)
        {
            GestionVehiculos registro = new GestionVehiculos();

            try
            {
                var document = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos");
                registro = (from h in document.AsQueryable()
                            where (h.Documento_Usuario.Equals(numeroDocumento))
                            select h).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return registro;
        }

        public bool ActualizarReintentosBiometria(GestionVehiculos data)
        {
            bool registro = false;
            try
            {
                var document = MongoContext.Database().GetCollection<GestionVehiculos>("GestionVehiculos");

                document.UpdateOne(
                     Builders<GestionVehiculos>.Filter.Eq(c => c.Placa, data.Placa) &
                     Builders<GestionVehiculos>.Filter.Eq(c => c.Documento_Usuario, data.Documento_Usuario),
                     Builders<GestionVehiculos>.Update
                                        .Set(x => x.NumeroCelda, data.NumeroCelda)
                    );
                registro = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return registro;
        }
    }
}
