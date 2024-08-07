using System.ComponentModel.DataAnnotations;

namespace utilidadesv2.Entidades
{
    public class CodigoPostalEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(5)]
        public string CodigoPostal { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }


        public int EstadoId { get; set; }

        [StringLength(250)]
        public string Alcaldia { get; set; }

        public int AlcaldiaId { get; set; }

        [StringLength(50)]
        public string TipoDeAsentamiento { get; set; }

        [StringLength(100)]
        public string Asentamiento { get; set; }
    }
}