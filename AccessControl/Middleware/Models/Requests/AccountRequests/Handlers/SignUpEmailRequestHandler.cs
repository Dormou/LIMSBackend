using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using AccessControl.Requests;
using AccessControl.Responses;
using AccessControl.Domain.Aggregates.Account;
using AccessControl.Services;
using AccessControl.Middleware.Models.Requests.AccountRequests;

namespace AccessControl.Passport.Api.Requests.Handlers;


public class SignUpEmailRequestHandler : IRequestHandler<SignUpEmailRequest, SignUpResponse>
{
    private readonly IEmailService _emailService;
    private readonly IAccountRepo _accountRepo;
    private readonly ILogger<SignUpEmailRequestHandler> _logger;
    public SignUpEmailRequestHandler(
        IEmailService emailService,
        IAccountRepo accountRepo,
        ILogger<SignUpEmailRequestHandler> logger)
    {
        _emailService = emailService;
        _accountRepo = accountRepo;
        _logger = logger;
    }

    public async Task<SignUpResponse> Handle(SignUpEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (!new EmailAddressAttribute().IsValid(request.Email) && request.Email is not null) throw new BadHttpRequestException("Email not valid");

            if (!new Regex(@"^[\w$&_]{5,24}$").IsMatch(request.Nickname)) throw new BadHttpRequestException("Nick not valid");

            if (!new Regex(@"^\S{4,23}$").IsMatch(request.Password)) throw new BadHttpRequestException("Password not valid");

            if (await _accountRepo.FindByEmailAsync(request.Email) is not null) throw new BadHttpRequestException("Email is busy");

            if (await _accountRepo.FindByNickNameAsync(request.Nickname) is not null) throw new BadHttpRequestException("Nick is busy");

            var account = Account.From(request);

            await _accountRepo.AddAsync(account);

            await _emailService.SendEmailAsync(
                account.Nickname,
                account.Email,
                "SignIn Library",
                $"Activation code: {account.ActivationCode}");

           // await _accountRepo._db..SaveEntitiesAsync(cancellationToken);

            return new SignUpResponse(Message: "Code send to you email");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with sign up. Error: {ex}");

            throw;
        }
    }
}
