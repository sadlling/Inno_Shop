﻿using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.CustomExceptions;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Domain.Entities;
using System.Net.Http.Headers;

namespace ProductManagement.Application.Features.CategoryFeatures.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var updatedCategory = await _categoryRepository.GetByIdAsync(request.id);
            if (updatedCategory is null) 
            {
                throw new NotFoundException("Category not found");
            }
            var category = _mapper.Map<Category>(request);
            await _categoryRepository.UpdateAsync(category);
            return Unit.Value;
        }
    }

}
