using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.ModeloDatos.ModeloUsuario
{
    public class UsuariosRequest
    {
        [BsonIgnoreIfNull]
        public string Nombre_Usuario { get; set; }
        [BsonIgnoreIfNull]
        public string Apellido_Usuario { get; set; }
        [BsonIgnoreIfNull]
        public string Documento_Usuario { get; set; }
        [BsonIgnoreIfNull]
        public string Contraseña { get; set; }
        [BsonIgnoreIfNull]
        public string Modelo_Vehiculo { get; set; }
        [BsonIgnoreIfNull]
        public string Placa { get; set; }
        [BsonIgnoreIfNull]
        public string Nombre_Rol { get; set; }
    }
}
