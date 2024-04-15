﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.ProductFeatures.CreateProduct;
using ProductManagement.Application.Features.ProductFeatures.DeleteProduct;
using ProductManagement.Application.Features.ProductFeatures.GetAllProducts;
using ProductManagement.Application.Features.ProductFeatures.GetProduct;
using ProductManagement.Application.Features.ProductFeatures.UpdateProduct;
using System.Security.Claims;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] QueryStringParameters parameters)
        {
            var response = await _mediator.Send(new GetAllProductsRequest(parameters));
            if (response is not null)
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
            return NotFound("Products not found");
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] GetProductRequest request)
        {
            var response = await _mediator.Send(request);
            if (response is not null)
            {
                return Ok(response);
            }
            return NotFound("Product not found");
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto request)
        {
            var tempUserId = "84e1d672-7b69-4de6-9f87-0d683635bba3"; //TODO: remove after tests
            await _mediator.Send(new CreateProductRequest(request, User.FindFirstValue(ClaimTypes.NameIdentifier) ?? tempUserId));
            return Created();
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto request)
        {
            var tempUserId = "84e1d672-7b69-4de6-9f87-0d683635bba3"; //TODO: remove after tests
            await _mediator.Send(new UpdateProductRequest(request, User.FindFirstValue(ClaimTypes.NameIdentifier) ?? tempUserId));
            return Ok("Product updated");
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid Id)
        {
            var tempUserId = "84e1d672-7b69-4de6-9f87-0d683635bba3"; //TODO: remove after tests
            await _mediator.Send(new DeleteProductRequest(Id, User.FindFirstValue(ClaimTypes.NameIdentifier) ?? tempUserId)); ;
            return Ok("Product deleted");
        }
    }
}
