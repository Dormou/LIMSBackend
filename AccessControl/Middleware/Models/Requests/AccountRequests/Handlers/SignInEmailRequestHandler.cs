using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

using AccessControl.Options;
using AccessControl.Responses;
using AccessControl.Requests;
using AccessControl.Domain.Aggregates.Account;
using AccessControl.Requests.AccountRequests.Handlers;
using AccessControl.Domain.Services.UserAgentParser;
using AccessControl.Middleware.Models.Requests.AccountRequests;

namespace AccessControl.Passport.Api.Requests.Handlers;

public class SignInEmailRequestHandler : IRequestHandler<SignInEmailRequest, SignInResponse>
{
    private readonly JWTOptions _jwtOptions;
    private readonly IAccountRepo _accountRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SignInEmailRequestHandler> _logger;

    public SignInEmailRequestHandler(
        IAccountRepo accountRepo,
        IHttpContextAccessor httpContextAccessor,
        IOptions<JWTOptions> jwtOptions,
        ILogger<SignInEmailRequestHandler> logger)
    {
        _accountRepo = accountRepo;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }
    public async Task<SignInResponse> Handle(SignInEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _accountRepo.FindByEmailAsync(request.Email);

            if (account is null) throw new BadHttpRequestException("Account not found");

            if (account.Password != request.Password) throw new BadHttpRequestException("Bad password");

            var jwtToken = new JwtToken(account.Id, _jwtOptions.SecretKey, _jwtOptions.Issuer, _jwtOptions.ExpiresHours).Value;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();

            var operationSystem = UserAgentParser.GetOperatingSystem(userAgent);

            await _accountRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new SignInResponse(Token: encodedJwt, Message: "Access true");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with signin. Error: {ex}");

            throw new HttpRequestException("Error with attemping signin.", ex, HttpStatusCode.InternalServerError);
        }
    }
}
