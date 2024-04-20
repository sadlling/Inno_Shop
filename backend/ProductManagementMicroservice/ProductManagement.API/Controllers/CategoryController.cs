using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.Features.CategoryFeatures.CreateCategory;
using ProductManagement.Application.Features.CategoryFeatures.DeleteCategory;
using ProductManagement.Application.Features.CategoryFeatures.GetAllCategories;
using ProductManagement.Application.Features.CategoryFeatures.GetCategory;
using ProductManagement.Application.Features.CategoryFeatures.UpdateCategory;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]QueryStringParameters parameters) 
        {
            var response  = await _mediator.Send(new GetAllCategoriesRequest(parameters));
            if(response is not null) 
            {
                var metadata = new
                {
                    response.TotalCount,
                    response.PageSize,
                    response.CurrentPage,
                    response.TotalPages,
                    response.HasNext,
                    response.HasPrevious
                };
                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetCategoryById")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetCategoryById([FromQuery]string Id)
        {

            var response = await _mediator.Send(new GetCategoryRequest(Guid.Parse(Id)));
            if (response is not null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateCategory")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            await _mediator.Send(request);
            return Ok("Created!");
        }

        [HttpPut]
        [Route("UpdateCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            await _mediator.Send(request);
            return Ok("Updated!");
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromQuery]string Id)
        {
            await _mediator.Send(new DeleteCategoryRequest(Guid.Parse(Id)));
            return Ok("Deleted!");
        }


    }
}
