using Microsoft.AspNetCore.Mvc;

namespace Utilidades.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarjetasDeDebitoController : ControllerBase
    {
        /// <summary>
        /// Genera un número de tarjeta de débito válido según el algoritmo de Luhn y las convenciones de numeración de tarjetas.o
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            string numeroDeTarjeta;

            numeroDeTarjeta = GenerateDebitCardNumber();

            return Created("", new { numeroDeTarjeta });
        }

        private string GenerateDebitCardNumber()
        {
            // Ejemplo de IIN/BIN (los primeros 6 dígitos)
            string bin = "400000"; // Esto es un ejemplo y debería ser un BIN válido

            // Generar el número de cuenta (del 7 al 15)
            Random random = new Random();
            string accountNumber = string.Empty;
            for (int i = 0; i < 9; i++)
            {
                accountNumber += random.Next(0, 10).ToString();
            }

            // Combinar BIN y número de cuenta
            string partialCardNumber = bin + accountNumber;

            // Calcular el dígito de control usando el algoritmo de Luhn
            int checkDigit = CalculateLuhnCheckDigit(partialCardNumber);

            // Combinar todo para obtener el número de tarjeta completo
            string fullCardNumber = partialCardNumber + checkDigit.ToString();

            return fullCardNumber;
        }

        private int CalculateLuhnCheckDigit(string number)
        {
            int sum = 0;
            bool alternate = false;

            // Recorrer el número de derecha a izquierda
            for (int i = number.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(number[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }

            // El dígito de control es el número que hace que la suma sea múltiplo de 10
            int checkDigit = (10 - (sum % 10)) % 10;

            return checkDigit;
        }
    }
}