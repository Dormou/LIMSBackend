using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

using AccessControl.Responses;
using AccessControl.Options;
using AccessControl.Requests;
using AccessControl.Domain.Aggregates.Account;
using AccessControl.Domain.Aggregates.Account.Enums;
using AccessControl.Domain.Services.UserAgentParser;
using AccessControl.Requests.AccountRequests.Handlers;

namespace AccessControl.Passport.Api.Requests.Handlers;

public class ActivationRequestHandler //: IRequestHandler<ActivationRequest, ActivationResponse>
{
    private readonly JWTOptions _jwtOptions;
    private readonly IAccountRepo _accountRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ActivationRequestHandler> _logger;

    public ActivationRequestHandler(
        IAccountRepo accountRepo,
        IHttpContextAccessor httpContextAccessor,
        IOptions<JWTOptions> jwtOptions,
        ILogger<ActivationRequestHandler> logger)
    {
        _accountRepo = accountRepo;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<ActivationResponse> Handle(ActivationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _accountRepo.FindByActivationCodeAsync(request.Code);

            if (account is null) throw new BadHttpRequestException("Account not found");

            if (account.AccessStatus != AccessStatus.WaitActivate) throw new BadHttpRequestException("Account not wait activate");

            var jwtToken = new JwtToken(account.Id, _jwtOptions.SecretKey, _jwtOptions.Issuer, _jwtOptions.ExpiresHours).Value;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();

            var operationSystem = UserAgentParser.GetOperatingSystem(userAgent);

            account.ActivationCode = string.Empty;
            account.AccessStatus = AccessStatus.Active;

            //await _accountRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new ActivationResponse(Token: encodedJwt, Message: "Access true");
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error with signin. Error: {ex}");

            throw;
        }

    }
}
