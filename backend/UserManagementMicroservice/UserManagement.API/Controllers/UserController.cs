using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.UserFeatures.CreateUser;
using UserManagement.Application.Features.UserFeatures.DeleteUser;
using UserManagement.Application.Features.UserFeatures.GetAllUsers;
using UserManagement.Application.Features.UserFeatures.GetUser;
using UserManagement.Application.Features.UserFeatures.UpdateUser;

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
        [Authorize]
        public async Task<IActionResult> GetUser(string userId)
        {
            var response = await _mediator.Send(new GetUserRequest(userId));
            if (response is not null)
            {
                return Ok(response);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersRequest());
            if (response is not null)
            {
                return Ok(response);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("CreateUser")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            await _mediator.Send(request);
            return Ok("Created!");
        }
        [HttpPut]
        [Route("UpdateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserRequest request)
        {
            await _mediator.Send(request);
            return Ok("Updated!");
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _mediator.Send(new DeleteUserRequest(userId));
            return Ok("Deleted!");
        }

    }
}
