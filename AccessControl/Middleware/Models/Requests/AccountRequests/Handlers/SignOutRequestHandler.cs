using System.Net;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;

using AccessControl.Responses;
using AccessControl.Options;
using AccessControl.Requests;
using AccessControl.Domain.Aggregates.Account;
using AccessControl.Passport.Api.Requests.Handlers;


namespace AccessControl.Passport.Api.Requests.AccountRequests.Handlers;

public class SignOutRequestHandler //: IRequestHandler<SignOutRequest, SignOutResponse>
{ 
    private readonly JWTOptions _jwtOptions;
    private readonly IAccountRepo _accountRepo;
    //private readonly ISignOutTokenService _signOutTokenService;
    private readonly ILogger<SignInEmailRequestHandler> _logger;

    public SignOutRequestHandler(
        IAccountRepo accountRepo,
        //ISignOutTokenService signOutTokenService,
        IOptions<JWTOptions> jwtOptions,
        ILogger<SignInEmailRequestHandler> logger)
    {
        _accountRepo = accountRepo;
        //_signOutTokenService = signOutTokenService;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<SignOutResponse> Handle(SignOutRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();

            var dataToken = handler.ReadJwtToken(request.Token);

            var id = dataToken.Payload["_id"];

            var account = await _accountRepo.FindByIdAsync((Guid)id);

            if (account is null) throw new BadHttpRequestException("Do not touch token, my junior hacker))");

            //по идее тут логика внесения токена в хранилище токенов
            //также по задумке, можно развить юзая редис, и сделать храниения некого числа токенов в ОЗУ, а остальные в редис (типа многоуровневый кэш))) )
            //так как это не обязательное требование, а лишь способ повысить безопастность, я пока не реализовал это
            //await _accountRepo.UnitOfWork.SaveChangesAsync(cancellationToken);          

            return new SignOutResponse(Message: "signout");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting signout. Ex: {ex}");

            throw new HttpRequestException("Error with attempting signout.", ex, HttpStatusCode.InternalServerError);
        }
    }
}
