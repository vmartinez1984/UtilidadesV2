using Bogus;

namespace BogusConsole
{
    public class Persona
    {
        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }
    }
    internal class PersonaFake: Faker<Persona>
    {
        public PersonaFake() : base("es_MX")
        {
            RuleFor(x => x.Nombre, f => f.Name.FirstName(0));
            RuleFor(x => x.PrimerApellido, f => f.Name.LastName());
            RuleFor(x => x.SegundoApellido, f => f.Name.LastName());
        }
    }
}
