using System.ComponentModel.DataAnnotations;

namespace Contador.BusinessLayer
{
    public class ContadorDto
    {
        [MaxLength(64)]
        public string Key { get; set; }

        [Range(0, int.MaxValue)]
        public int ValorMin { get; set; }

        [Range(0, int.MaxValue)]
        public int ValorMax { get; set; }
    }
}
