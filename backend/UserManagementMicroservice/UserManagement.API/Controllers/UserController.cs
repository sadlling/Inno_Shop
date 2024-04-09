using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.UserFeatures.Authenticate;
using UserManagement.Application.Features.UserFeatures.CreateUser;
using UserManagement.Application.Features.UserFeatures.RefreshTokens;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]CreateUserRequest request)
        {
            var response  = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]AuthenticateUserRequest request)
        {
            var response = await _mediator.Send(request);
            if(response != null)
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
        [Authorize]
        public async Task<IActionResult>RefreshTokens()
        {
            var jwtToken = Request.Cookies["JWT"];
            var refreshToken = Request.Cookies["Refresh"];

            if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Invalid access token or refresh token");
            }

            var response = await _mediator.Send(new RefreshTokensRequest(jwtToken, refreshToken));
            if (response != null)
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
