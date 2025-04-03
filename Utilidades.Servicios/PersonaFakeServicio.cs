using MongoDB.Bson;
using System.Security.Cryptography;
using System.Text;
using Utilidades.Repositorios;
using Utilidades.Repositorios.Entidades;
using Utilidades.Servicios.Curp;
using Utilidades.Servicios.Curp.Enums;
using Utilidades.Servicios.Dtos;
using Utilidades.Servicios.Rfc;

namespace Utilidades.Servicios
{
    public class PersonaFakeServicio
    {
        private readonly NombreRepositorio _apellidoNombreRepository;        
        private readonly RepositorioDeCodigoPostal _repositorioDeCodigoPostal;        

        public PersonaFakeServicio(        
            NombreRepositorio repositorio,            
            RepositorioDeCodigoPostal repositorioDeCodigoPostal
        )
        {            
            _apellidoNombreRepository = repositorio;            
            _repositorioDeCodigoPostal = repositorioDeCodigoPostal;
        }

        /// <summary>
        /// Crea la persona fake
        /// </summary>
        /// <returns></returns>
        public async Task<PersonaFakeDto> ObtenerAsync()
        {
            PersonaFakeDto clienteFake;
            CodigoPostalEntity codigoPostal;
            DateTime fechaDeNacimiento;
            string paterno;
            string materno;
            Estado estado;
            string nombres;
            string curp;
            Sexo sexo;
            Random random = new Random();
            /*
             Hombre = 'H', Mujer = 'M'
             */

            fechaDeNacimiento = ObtenerFechaDeNacimiento();
            sexo = ObtenerSexo();
            paterno = await _apellidoNombreRepository.ObtenerApellidoAleatorioAsync();
            materno = await _apellidoNombreRepository.ObtenerApellidoAleatorioAsync();
            nombres = await _apellidoNombreRepository.ObtenerNombresAsync(sexo == Sexo.Mujer ? 0 : 1);
            estado = ObtenerEstado();
            curp = CurpServicio.Generar(nombres, paterno, materno, sexo, fechaDeNacimiento, estado);
            codigoPostal = await ObtenerCodigoPostalAleatorioPorEstado((int)estado);
            clienteFake = new PersonaFakeDto
            {
                BirthDate = fechaDeNacimiento.ToString("yyyy-MM-dd"),
                Calle = "Conocida",
                CodigoPostal = codigoPostal.CodigoPostal,
                Colonia = $"{codigoPostal.TipoDeAsentamiento} {codigoPostal.Asentamiento}",
                Curp = curp,
                EmailAddress = $"{nombres.ToLower().Replace(" ", string.Empty).Replace("ñ", "n")}.{paterno.ToLower().Replace(" ", string.Empty).Replace("ñ", "n")}@gmail.com",
                Estado = codigoPostal.Estado,
                EstadoCivilDatosCliente = "Soltero",
                FirstName = nombres,
                Genero = sexo.ToString(),
                GeneroInicial = sexo == Sexo.Mujer ? "M" : "H",
                GeneroNombre = sexo == Sexo.Mujer ? "Mujer" : "Hombre",
                LastName = paterno + " " + materno,
                LugarDeNacimiento = estado.ToString().Replace("_", " "),
                MobilePhone = ObtenerTelefono(),
                Municipio = codigoPostal.Alcaldia,
                NumeroExterior = "Exterior",
                NumeroInterior = "Interior",
                PrimerApellido = paterno,
                SegundoApellido = materno,
                Rfc = RfcServicio.CalculateRFCHomonym(TipoPersona.Fisica, nombres, paterno, materno, fechaDeNacimiento),
                Guid = Guid.NewGuid(),
                idMongoDb = ObjectId.GenerateNewId().ToString(),
                idFirebase = GenerateFirebaseId()
            };

            return clienteFake;
        }

        private async Task<CodigoPostalEntity> ObtenerCodigoPostalAleatorioPorEstado(int estado)
            => await _repositorioDeCodigoPostal.ObtenerCodigoPostalAleatorioAsync(estado.ToString());


        private string GenerateFirebaseId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int length = 20;
            StringBuilder result = new StringBuilder(length);

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                crypto.GetBytes(data);

                foreach (byte b in data)
                {
                    result.Append(chars[b % chars.Length]);
                }
            }

            return result.ToString();
        }

        private Sexo ObtenerSexo()
        {
            Random random = new Random();
            Sexo sexo;
            int sexonInt;

            sexonInt = random.Next(0, 2);
            sexo = sexonInt == 1 ? Sexo.Hombre : Sexo.Mujer;

            return sexo;
        }

        private Estado ObtenerEstado()
        {
            Random random = new Random();
            Estado estado;

            estado = (Estado)random.Next(1, 32);

            return estado;
        }

        private DateTime ObtenerFechaDeNacimiento()
        {
            int anio;
            int mes;
            int dia;
            Random random;
            DateTime fecha;

            random = new Random();
            anio = random.Next(1943, 2004);
            mes = random.Next(1, 12);
            fecha = new DateTime(anio, mes, 1);
            dia = random.Next(1, fecha.AddMonths(1).AddDays(-1).Day);
            fecha = new DateTime(anio, mes, dia);

            return fecha;
        }

        private string ObtenerTelefono()
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;
        }
    }
}
