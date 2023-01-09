using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace APIGateway.AuthenticationMiddleware;

public class JwtConfiguration
{
    private readonly string _issuer = null!;
    public string Issuer
    {
        get => _issuer;
        init
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(Issuer));
            }

            _issuer = value;
        }
    }
}

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

    public AuthenticationMiddleware(RequestDelegate next, IOptions<JwtConfiguration> jwtConfiguration)
    {
        _next = next;
        _jwtConfiguration = jwtConfiguration.Value;

        _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            _jwtConfiguration.Issuer + "/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever());
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrWhiteSpace(token))
        {
            await AttachUserToContextAsync(context, token);
        }

        await _next(context);
    }

    private async Task AttachUserToContextAsync(HttpContext context, string token, CancellationToken cancellationToken = default)
    {
        var discoveryDocument = await _configurationManager.GetConfigurationAsync(cancellationToken);
        var signingKeys = discoveryDocument.SigningKeys;
        
        var validationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ValidateIssuer = true,
            ValidIssuer = _jwtConfiguration.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = signingKeys,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2),
            ValidateAudience = false,
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        if (tokenHandler.CanReadToken(token))
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            context.User = principal;
        }
    }
}
