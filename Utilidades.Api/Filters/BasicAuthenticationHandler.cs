using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using VMtz84.Pizzas.Services;

namespace Utilidades.Api.Filters
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly PizzaService _pizzaService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            PizzaService pizzaService)
            : base(options, logger, encoder, clock)
        {
            _pizzaService = pizzaService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (!"Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
                    return AuthenticateResult.NoResult();

                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                if (credentials.Length != 2)
                    return AuthenticateResult.Fail("Invalid Authorization Header");

                var correo = credentials[0];
                var contraseña = credentials[1];

                var esValido = await _pizzaService.Cliente.ValidarCredencialesAsync(correo, contraseña);
                if (!esValido)
                    return AuthenticateResult.Fail("Invalid username or password");

                var cliente = await _pizzaService.Cliente.ObtenerPorCorreoAsync(correo);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, cliente.NombreCompleto),
                    new Claim(ClaimTypes.Role, "Cliente"),
                    new Claim("id", cliente.Encodedkey)
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}
