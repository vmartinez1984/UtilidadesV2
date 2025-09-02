using Microsoft.AspNetCore.Mvc;
using System.Text;
using Utilidades.Servicios.Dtos;
using Utilidades.Servicios.Rfc;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Controller del rfc
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RfcController : ControllerBase
    {        
        /// <summary>
        /// Genera el rfc con homoclave, deacuedo con el algoritmo del SAT.
        /// Fisica = 0, Moral = 1
        /// </summary>
        /// <param name="rfcDto"></param>
        /// <returns></returns>
        /// <response code="202">Create</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), 202)]
        [Produces("application/json")]
        public ActionResult GenerarRfcConHomoclave(
            RfcDto rfcDto
        )
        {
            string rfcConHomoclave;

            rfcConHomoclave = RfcServicio.CalculateRFCHomonym(
                rfcDto.TipoDePersona,
                rfcDto.Nombre,
                rfcDto.PrimerApellido,
                rfcDto.SegundoApellido,
                rfcDto.FechaDeNacimiento
            );

            rfcConHomoclave = GenerarRfcConHomoclave(rfcConHomoclave.Substring(0,10));

            return Created(string.Empty, new { Rfc = rfcConHomoclave, Fecha = DateTime.Now });
        }

        private static readonly Dictionary<char, int> TablaCaracteres = new Dictionary<char, int>()
    {
        {' ', 00},{'0', 00},{'1', 01},{'2', 02},{'3', 03},{'4', 04},{'5', 05},
        {'6', 06},{'7', 07},{'8', 08},{'9', 09},{'A', 10},{'B', 11},{'C', 12},
        {'D', 13},{'E', 14},{'F', 15},{'G', 16},{'H', 17},{'I', 18},{'J', 19},
        {'K', 20},{'L', 21},{'M', 22},{'N', 23},{'&', 24},{'O', 25},{'P', 26},
        {'Q', 27},{'R', 28},{'S', 29},{'T', 30},{'U', 31},{'V', 32},{'W', 33},
        {'X', 34},{'Y', 35},{'Z', 36},{'Ñ', 37}
    };

        // Tabla para homoclave (dos caracteres extra)
        private static readonly string TablaHomoclave = "123456789ABCDEFGHIJKLMNPQRSTUVWXYZ";


        private string GenerarRfcConHomoclave(string rfcBase)
        {
            rfcBase = rfcBase.ToUpper();

            // Paso 1: convertir cada caracter en su valor numérico
            StringBuilder valores = new StringBuilder();
            foreach (char c in rfcBase)
            {
                if (TablaCaracteres.ContainsKey(c))
                    valores.Append(TablaCaracteres[c].ToString("00"));
            }

            // Paso 2: multiplicar pares y sumar
            int suma = 0;
            for (int i = 0; i < valores.Length - 1; i++)
            {
                int a = int.Parse(valores[i].ToString());
                int b = int.Parse(valores[i + 1].ToString());
                suma += a * b;
            }

            // Paso 3: obtener el número para la homoclave
            int residuo = suma % 1000;
            int cociente = residuo / 34;
            int modulo = residuo % 34;

            // Los dos caracteres de homoclave
            string homoclave = TablaHomoclave[cociente].ToString() + TablaHomoclave[modulo].ToString();

            // Paso 4: calcular el dígito verificador
            int sumaVerificador = 0;
            string rfcHomoclave = rfcBase + homoclave;

            for (int i = 0; i < rfcHomoclave.Length; i++)
            {
                char c = rfcHomoclave[i];
                int valor = TablaCaracteres.ContainsKey(c) ? TablaCaracteres[c] : 0;
                sumaVerificador += valor * (13 - i);
            }

            int digito = 11 - (sumaVerificador % 11);
            if (digito == 11) digito = 0;
            if (digito == 10) digito = 'A'; // se representa con letra A

            // RFC completo
            return rfcHomoclave + digito.ToString();
        }
    }
}
