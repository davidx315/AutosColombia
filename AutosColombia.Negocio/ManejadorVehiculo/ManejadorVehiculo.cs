using AutosColombia.ModeloDatos.ModelosGenerales;
using AutosColombia.ModeloDatos.ModeloVehiculo;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MongoDB.Driver;
using AutosColombia.ServicioDatos.OperacionesDatos.Vehiculos;

namespace AutosColombia.Negocio.ManejadorVehiculo
{
    public class ManejadorVehiculo
    {
        private static readonly Lazy<ManejadorVehiculo> instanciaUnica = new Lazy<ManejadorVehiculo>(() => new ManejadorVehiculo());

        public static ManejadorVehiculo ObtenerInstancia
        {
            get
            {
                return instanciaUnica.Value;
            }
        }

        public List<GestionVehiculos> ConsultaVehiculoPorFiltros(DataTableParameter parametrosDataTable, out long cantidadRegistros)
        {
            List<GestionVehiculos> listaUsuariosReintento = new List<GestionVehiculos>();
            var ordenamiento = new Dictionary<string, string>();

            dynamic datosFiltro = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(parametrosDataTable.customFilter);

            string tipoDocumento = datosFiltro.TipoDocumento;
            string numeroDocumento = datosFiltro.NumeroDocumento;

            var columnaSelecionada = parametrosDataTable.order.Select(x => x.column).FirstOrDefault();

            var orden = parametrosDataTable.order.FirstOrDefault(o => o.column == columnaSelecionada);
            var columna = parametrosDataTable.columns[orden.column].data;
            if (!string.IsNullOrEmpty(columna))
            {
                ordenamiento.Add(columna, orden.dir);
            }

            listaUsuariosReintento = CrudVehiculos.ObtenerInstancia.ConsultaVehiculoPorFiltros(tipoDocumento, numeroDocumento, out cantidadRegistros, OrdenarPorColumnasVehiculo(ordenamiento), parametrosDataTable.start, parametrosDataTable.length, parametrosDataTable.search.value);

            return listaUsuariosReintento;
        }

        public GestionVehiculos ConsultarVehiculoXDocumento(string numeroDocumento)
        {
            GestionVehiculos concepto = CrudVehiculos.ObtenerInstancia.ConsultaByDocumento(numeroDocumento);
            return concepto;
        }

        public bool ModificarVehiculo(GestionVehiculos usuarioListaNegra)
        {
            try
            {
                return new CrudVehiculos().ActualizarReintentosBiometria(usuarioListaNegra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SortDefinition<GestionVehiculos> OrdenarPorColumnasVehiculo(Dictionary<string, string> ordenamiento)
        {
            var builderSort = Builders<GestionVehiculos>.Sort;
            var sorts = new List<SortDefinition<GestionVehiculos>>();
            foreach (var columnaOrden in ordenamiento)
            {
                if (columnaOrden.Value.ToLower() == "asc")
                {
                    switch (columnaOrden.Key)
                    {
                        case "Documento_Usuario":
                            sorts.Add(builderSort.Ascending(a => a.Documento_Usuario));
                            break;

                        case "Placa":
                            sorts.Add(builderSort.Ascending(a => a.Placa));
                            break;
                    }
                }
                else
                {
                    switch (columnaOrden.Key)
                    {
                        case "Documento_Usuario":
                            sorts.Add(builderSort.Descending(a => a.Documento_Usuario));
                            break;

                        case "Placa":
                            sorts.Add(builderSort.Descending(a => a.Placa));
                            break;
                    }
                }
            }
            var sort = builderSort.Combine(sorts.ToArray());
            return sort;
        }
    }
}
