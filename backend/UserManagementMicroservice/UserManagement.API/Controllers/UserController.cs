using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.UserFeatures.GetUser;

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

        [HttpGet]
        [Route("GetUser/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var response = await _mediator.Send(new GetUserRequest(userId));
            if(response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }
       
    }
}
