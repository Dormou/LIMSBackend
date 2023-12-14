using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AccessControl.Responses;
using AccessControl.Requests;
using AccessControl.Middleware.Models.Requests.AccountRequests;

namespace AccessControl.Controllers;

[Route("access/control/account")]
public class AccountController : Controller
{

    [AllowAnonymous, HttpPost("signup")]
    public async Task<SignUpResponse> SignUp
        (
            [FromBody] SignUpEmailRequest request, 
            [FromServices] IRequestHandler<SignUpEmailRequest, SignUpResponse> handler, 
            CancellationToken cancellationToken
        ) 
        => await handler.Handle(request, cancellationToken);


    [AllowAnonymous, HttpPost("signin")]
    public async Task<SignInResponse> SignUp(
        [FromBody] SignInEmailRequest request, 
        [FromServices] IRequestHandler<SignInEmailRequest, SignInResponse> handler,
        CancellationToken cancellationToken
        ) 
        => await handler.Handle(request, cancellationToken);


    [AllowAnonymous, HttpPost("activation")]
    public async Task<ActivationResponse> Activation
        (
            [FromBody] ActivationRequest request,
            [FromServices] IRequestHandler<ActivationRequest, ActivationResponse> handler, 
            CancellationToken cancellationToken
        ) 
        => await handler.Handle(request, cancellationToken);


    [AllowAnonymous, HttpPost("forgot")]
    public async Task<ForgotPasswordResponse> Forgot
        (
            [FromBody] ForgotPasswordRequest request,
            [FromServices] IRequestHandler<ForgotPasswordRequest,  
            ForgotPasswordResponse> handler, CancellationToken cancellationToken
        ) 
        => await handler.Handle(request, cancellationToken);


    [AllowAnonymous, HttpPost("activate/password")]
    public async Task<ActivateNewPasswordResponse> PasswordActivate
        (
            [FromBody] ActivateNewPasswordRequest request, 
            [FromServices] IRequestHandler<ActivateNewPasswordRequest, ActivateNewPasswordResponse> handler, 
            CancellationToken cancellationToken
        )
        => await handler.Handle(request, cancellationToken);

    [Authorize, HttpDelete("signout")]
    public async new Task<SignOutResponse> SignOut([FromServices] IRequestHandler<SignOutRequest, SignOutResponse> handler, CancellationToken cancellationToken)
    {//тут по идее надо изменить куки клиента
        return await handler.Handle(new SignOutRequest(Request.Headers.Authorization), cancellationToken);
    }
}