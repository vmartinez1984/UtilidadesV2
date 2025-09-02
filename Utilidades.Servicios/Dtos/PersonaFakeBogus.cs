using Bogus;
using Bogus.DataSets;
namespace Utilidades.Servicios.Dtos
{
    internal class Persona
    {
        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }
    }

    internal class PersonaFakeBogus : Faker<Persona>
    {
        public PersonaFakeBogus(int sexo) : base("es_MX")
        {
            Name.Gender gender = sexo == 0 ? Name.Gender.Female : Name.Gender.Male;
            RuleFor(x => x.Nombre, f => f.Name.FirstName(gender));
            RuleFor(x => x.PrimerApellido, f => f.Name.LastName());
            RuleFor(x => x.SegundoApellido, f => f.Name.LastName());
        }
    }
}
