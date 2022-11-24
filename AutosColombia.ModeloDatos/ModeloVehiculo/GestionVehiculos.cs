using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.ModeloDatos.ModeloVehiculo
{
    public class GestionVehiculos
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Modelo_Vehiculo { get; set; }
        [BsonIgnoreIfNull]
        public string Placa { get; set; }
        [BsonIgnoreIfNull]
        public string Documento_Usuario { get; set; }
        [BsonIgnoreIfNull]
        public int NumeroCelda { get; set; }
    }
}
