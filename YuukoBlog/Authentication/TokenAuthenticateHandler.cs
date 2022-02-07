using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace YuukoBlog.Authentication
{
    [ExcludeFromCodeCoverage]
    public class TokenAuthenticateHandler : AuthenticationHandler<TokenOptions>
    {
        public new const string Scheme = "Token";

        public static string Token { get; set; } = null;

        private IConfiguration _config;

        public TokenAuthenticateHandler(
            IOptionsMonitor<TokenOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration config)
            : base(options, logger, encoder, clock)
        {
            _config = config;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorization = Request.Headers["Authorization"].ToArray();
            if (authorization.Length == 0)
            {
                if (!string.IsNullOrEmpty(Request.Query["token"]))
                {
                    authorization = new[] { $"Token {Request.Query["token"]}" };
                }
                else
                {
                    return AuthenticateResult.NoResult();
                }
            }

            if (authorization.First().StartsWith("Token", StringComparison.OrdinalIgnoreCase))
            {
                var t = authorization.First().Substring("Token ".Length);
                if (t != Token)
                {
                    return AuthenticateResult.NoResult();
                }
            }
            else
            {
                return AuthenticateResult.NoResult();
            }

            var claimIdentity = new ClaimsIdentity(Scheme, ClaimTypes.Name, ClaimTypes.Role);
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, _config["Account"]));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimIdentity), Scheme);

            return AuthenticateResult.Success(ticket);
        }
    }
}
