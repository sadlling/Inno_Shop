using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.AuthenticateFeatures.Login;
using UserManagement.Application.Features.UserFeatures.CreateUser;
using UserManagement.Application.Features.AuthenticateFeatures.RefreshTokens;
using UserManagement.Application.Features.AuthenticateFeatures.Register;
using UserManagement.Application.Features.AuthenticateFeatures.ConfirmEmail;
using UserManagement.Application.Features.AuthenticateFeatures.ForgotPassword;
using UserManagement.Application.Features.AuthenticateFeatures.ResetPassword;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            await _mediator.Send(new ConfirmEmailRequest(email, token));
            return Ok("Mail confirmed successfully");
            
        }
        [HttpPost]
        [Route("ForgotPassword")]
        //[Authorize]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordRequest request)
        {
            await _mediator.Send(request);
            return Ok("Check email");
        }
        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string token,string email)
        {
            var model = new ResetPasswordRequest(string.Empty,string.Empty,email,token);
            return Ok(model);
        }
        [HttpPost]
        [Route("ResetPassword")]
        //[Authorize]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            await _mediator.Send(request);
            return Ok("Password reset successfully");
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]CreateUserRequest request)
        {
            await _mediator.Send(new RegisterRequest(request));
            return Ok("Successful registration");
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var response = await _mediator.Send(request);
            if(response is not null)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1),
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Path = "/",
                    Secure = true,
                };
                Response.Cookies.Append("JWT", response.JwtToken, cookieOptions);
                Response.Cookies.Append("Refresh", response.RefreshToken, cookieOptions);
                return Ok("Login successfully");
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("RefreshTokens")]
        public async Task<IActionResult>RefreshTokens()
        {
            var jwtToken = Request.Cookies["JWT"];
            var refreshToken = Request.Cookies["Refresh"];

            if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Invalid access token or refresh token");
            }

            var response = await _mediator.Send(new RefreshTokensRequest(jwtToken, refreshToken));
            if (response is not null)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1),
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Path = "/",
                    Secure = true,
                };
                Response.Cookies.Append("JWT", response.JwtToken, cookieOptions);
                Response.Cookies.Append("Refresh", response.RefreshToken, cookieOptions);
                return Ok("Refresh successfully");
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("LogOut")]
        [Authorize]
        public IActionResult LogOut()
        {
            var jwtToken = Request.Cookies["JWT"];
            var refreshToken = Request.Cookies["Refresh"];

            if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Secure = true,
            };
            Response.Cookies.Append("JWT", "", cookieOptions);
            Response.Cookies.Append("Refresh", "", cookieOptions);
            return Ok();
        }

    }
}
