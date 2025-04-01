using utilidadesv2.Dtos;

namespace utilidadesv2.Interfaces
{
    public interface IServicioDePersona
    {
        Task<PersonaFakeDto> ObtenerAsync();
    }
}
