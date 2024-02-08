using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

using AccessControl.Options;
using AccessControl.Requests;
using AccessControl.Responses;
using AccessControl.Domain.Aggregates.Account;
using AccessControl.Domain.Aggregates.Account.Enums;
using AccessControl.Domain.Services.UserAgentParser;
using AccessControl.Requests.AccountRequests.Handlers;
using AccessControl.Middleware.Models.Requests.AccountRequests;

namespace AccessControl.Passport.Api.Requests.AccountRequests.Handlers;

public class ActivateNewPasswordRequestHandler : IRequestHandler<ActivateNewPasswordRequest, ActivateNewPasswordResponse>
{
    private readonly JWTOptions _jwtOptions;
    private readonly IAccountRepo _accountRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ActivateNewPasswordRequestHandler> _logger;

    public ActivateNewPasswordRequestHandler(
        IAccountRepo accountRepo,
        IHttpContextAccessor httpContextAccessor,
        IOptions<JWTOptions> jwtOptions,
        ILogger<ActivateNewPasswordRequestHandler> logger)
    {
        _accountRepo = accountRepo;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<ActivateNewPasswordResponse> Handle(ActivateNewPasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (!new Regex(@"^\S{4,23}$").IsMatch(request.Password)) throw new BadHttpRequestException("Password not valid");

            var account = await _accountRepo.FindByEmailAsync(request.Email);

            if (account is null) return new ActivateNewPasswordResponse(Token: null, Message: "Account not found");

            if (account.AccessStatus != AccessStatus.WaitChangePassword) throw new BadHttpRequestException("Not wait change password");

            var jwtToken = new JwtToken(account.Id, _jwtOptions.SecretKey, _jwtOptions.Issuer, _jwtOptions.ExpiresHours).Value;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();

            var operationSystem = UserAgentParser.GetOperatingSystem(userAgent);

            account.Password = request.Password;
            account.AccessStatus = AccessStatus.Active;

            await _accountRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new ActivateNewPasswordResponse(Token: encodedJwt, Message: $"Access true");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with signin. Error: {ex}");

            throw new HttpRequestException($"Error with attemping forgot password. Ex: {ex}", ex, HttpStatusCode.InternalServerError);
        }
    }
}