using AccessControl.Middleware.Models.Requests.AccountRequests;
using AccessControl.Passport.Api.Requests.AccountRequests.Handlers;
using AccessControl.Passport.Api.Requests.Handlers;
using AccessControl.Requests;
using AccessControl.Responses;
using AccessControl.Services;
using System.Runtime.CompilerServices;

namespace AccessControl.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services) => services
        .AddScoped<IEmailService, EmailService>();

    public static IServiceCollection ConfigureApplicationHandlers(this IServiceCollection services) => services
        .AddScoped<IRequestHandler<SignInEmailRequest, SignInResponse>, SignInEmailRequestHandler>()
        .AddScoped<IRequestHandler<SignUpEmailRequest, SignUpResponse>, SignUpEmailRequestHandler>()
        .AddScoped<IRequestHandler<ActivateNewPasswordRequest, ActivateNewPasswordResponse>, ActivateNewPasswordRequestHandler>()
        .AddScoped<IRequestHandler<ActivationRequest, ActivationResponse>, ActivationRequestHandler>()
        .AddScoped<IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>, ForgotPasswordRequestHandler>()
        .AddScoped<IRequestHandler<SignOutRequest, SignOutResponse>, SignOutRequestHandler>();
}