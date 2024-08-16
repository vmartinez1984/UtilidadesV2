using System.Text;
using MongoDB.Bson;
using Renapo;
using Renapo.Enums;
using utilidadesv2.Dtos;
using utilidadesv2.Entidades;
using utilidadesv2.Repositorio;
using System.Security.Cryptography;

namespace utilidadesv2.Servicios
{
    public class ServicioDePersona
    {
        private readonly RepositorioDeNombresYApellidos _apellidoNombreRepository;
        private readonly RepositorioDeNombresYApellidosEs _repoMongo;
        private readonly RepositorioDeCodigoPostal _repositorioDeCodigoPostal;
        private readonly Curp _curp;

        public ServicioDePersona(
            Curp curp, RepositorioDeNombresYApellidos repositorio,
            RepositorioDeNombresYApellidosEs repoMongo,
            RepositorioDeCodigoPostal repositorioDeCodigoPostal
        )
        {
            _curp = curp;
            _apellidoNombreRepository = repositorio;
            _repoMongo = repoMongo;
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
            paterno = await _repoMongo.ObtenerApellidoAleatorioAsync();
            materno = random.Next(0, 1) == 0 ? string.Empty : await _repoMongo.ObtenerApellidoAleatorioAsync();
            nombres = await _repoMongo.ObtenerNombresAsync(sexo == Sexo.Mujer ? 0 : 1);
            estado = ObtenerEstado();
            curp = _curp.Generar(nombres, paterno, materno, sexo, fechaDeNacimiento, estado);
            codigoPostal = await ObtenerCodigoPostalAleatorioPorEstado((int)estado);
            clienteFake = new PersonaFakeDto
            {
                BirthDate = fechaDeNacimiento.ToString("yyyy-MM-dd"),
                Calle = "Conocida",
                CodigoPostal = codigoPostal.CodigoPostal,
                Colonia = $"{codigoPostal.TipoDeAsentamiento} {codigoPostal.Asentamiento}",
                Curp = curp,
                EmailAddress = $"{nombres.Replace(" ", string.Empty).Replace("ñ", "n")}.{paterno.Replace(" ", string.Empty).Replace("ñ", "n")}@gmail.com".ToLower(),
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
                Rfc = curp.Substring(0, 13),
                Guid = Guid.NewGuid(),
                idMongoDb = ObjectId.GenerateNewId().ToString(),
                idFirebase = GenerateFirebaseId()
            };

            return clienteFake;
        }

        private async Task<CodigoPostalEntity> ObtenerCodigoPostalAleatorioPorEstado(int estado)
        {
            CodigoPostalEntity codigoPostal;

            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, $@"https://codigospostales.vmartinez84.xyz/api/CodigosPostales/Estados/{estado}/Aleatorio");
            //request.Headers.Add("accept", "*/*");
            //var response = await client.SendAsync(request);
            //if (response.IsSuccessStatusCode)
            //    codigoPostal = Newtonsoft.Json.JsonConvert.DeserializeObject<CodigoPostalEntity>(await response.Content.ReadAsStringAsync());
            //else
            //    codigoPostal = null;
           codigoPostal = await _repositorioDeCodigoPostal.ObtenerCodigoPostalAleatorioAsync(estado.ToString());

            return codigoPostal;
        }

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

        public string ObtenerTelefono()
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

        internal async Task AgregarNombreYApellidosAsync()
        {
            List<string> datos;
            List<ApellidoNombre> lista = new List<ApellidoNombre>();

            await _repoMongo.BorrarColeccionAsync();
            datos = Apellido.ObtenerTodos();
            datos.ForEach(dato =>
            {
                lista.Add(new ApellidoNombre
                {
                    Dato = dato,
                    Tipo = "Apellido"
                });
            });
            await _repoMongo.AgregarAsync(lista);

            lista = new List<ApellidoNombre>();
            datos = Nombre.ObtenerTodas();
            datos.ForEach(dato =>
            {
                lista.Add(new ApellidoNombre
                {
                    Dato = dato,
                    Tipo = "Nombre Mujer"
                });
            });
            await _repoMongo.AgregarAsync(lista);

            lista = new List<ApellidoNombre>();
            datos = Nombre.ObtenerTodos();
            datos.ForEach(dato =>
            {
                lista.Add(new ApellidoNombre
                {
                    Dato = dato,
                    Tipo = "Nombre Hombre"
                });
            });
            await _repoMongo.AgregarAsync(lista);
        }
    }
}