using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.ModeloDatos.ModeloUsuario
{
    public class UsuarioRoles
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfNull]
        public string Nombre_Rol { get; set; }
    }
}
