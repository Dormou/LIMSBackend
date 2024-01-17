using Newtonsoft.Json;

using AccessControl.Domain.Aggregates.Account.Commands;

namespace AccessControl.Requests;

public interface IRequest { };

public record SignInEmailRequest(
    [JsonProperty("email")] string Email,
    [JsonProperty("password")] string Password
): IRequest;

public record SignUpEmailRequest(
    [JsonProperty("firstname")] string Firstname,
    [JsonProperty("lastname")] string Lastname,
    [JsonProperty("email")] string Email,
    [JsonProperty("password")] string Password
) 
: IAddAccountCommand, IRequest;

public record SignInPhoneNumberRequest(
    [JsonProperty("phone-number")] string PhoneNumber,
    [JsonProperty("password")] string Password,
    [JsonProperty("captha")] string Captha
);

public record ActivationRequest(
    [JsonProperty("code")] string Code
);

public record SignOutRequest(
    [JsonProperty("token")] string Token
);

public record ForgotPasswordRequest(
    [JsonProperty("email")] string Email
);

public record ActivateNewPasswordRequest(
    [JsonProperty("password")] string Password,
    [JsonProperty("email")] string Email
);