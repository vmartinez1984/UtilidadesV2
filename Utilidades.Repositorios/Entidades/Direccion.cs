using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Utilidades.Repositorios.Entidades
{
    public class Direccion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("rnbp_id")]
        public int RnbpId { get; set; }

        [BsonElement("rnbp_numero_coleccion")]
        public int NumeroColeccion { get; set; }

        [BsonElement("rnbp_nombre")]
        public string Nombre { get; set; }

        [BsonElement("rnbp_fecha_fundacion")]
        public string FechaFundacion { get; set; }

        [BsonElement("rnbp_adscripcion")]
        public string Adscripcion { get; set; }

        [BsonElement("rnbp_calle_numero")]
        public string CalleYNumero { get; set; }

        [BsonElement("rnbp_colonia")]
        public string Colonia { get; set; }

        [BsonElement("rnbp_cp")]
        public string CodigoPostal { get; set; }

        [BsonElement("rnbp_telefono1")]
        public string Telefono { get; set; }

        [BsonElement("pagina_web")]
        public string PaginaWeb { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("gmaps_latitud")]
        public double Latitud { get; set; }

        [BsonElement("gmaps_longitud")]
        public double Longitud { get; set; }

        [BsonElement("estado_id")]
        public int EstadoId { get; set; }

        [BsonElement("municipio_id")]
        public int MunicipioId { get; set; }

        [BsonElement("localidad_id")]
        public int LocalidadId { get; set; }

        [BsonElement("nom_ent")]
        public string Estado { get; set; }

        [BsonElement("nom_mun")]
        public string Municipio { get; set; }

        [BsonElement("nom_loc")]
        public string Localidad { get; set; }

        [BsonElement("link_sic")]
        public string LinkSic { get; set; }

        [BsonElement("fecha_mod")]
        public string FechaModificacion { get; set; }
    }
}
