using Newtonsoft.Json;

namespace AccessControl.Responses;

public interface IResponse { };

public record SignInResponse(
    [JsonProperty("token")] string Token,
    [JsonProperty("firstname")] string Firstname,
    [JsonProperty("lastname")] string Lastname,
    [JsonProperty("email")] string Email,
    [JsonProperty("message")] string Message
) : IResponse;

public record SignUpResponse([JsonProperty("message")] string Message) : IResponse;

public record SignOutResponse([JsonProperty("message")] string Message) : IResponse;

public record ActivationResponse(
    [JsonProperty("token")] string Token,
    [JsonProperty("message")] string Message
) : IResponse;

public record ForgotPasswordResponse([JsonProperty("message")] string Message) : IResponse;

public record ActivateNewPasswordResponse(
    [JsonProperty("message")] string Message,
    [JsonProperty("token")] string Token
) : IResponse;