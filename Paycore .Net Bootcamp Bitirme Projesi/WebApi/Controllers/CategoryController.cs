using AutoMapper;
using Base.Response;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.CategoryService.Abstract;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;


        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "viewer")]
        public BaseResponse<IEnumerable<CategoryDto>> GetAll()
        {
            var response = categoryService.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<CategoryDto> GetById(int id)
        {
            var response = categoryService.GetById(id);
            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<CategoryDto> Delete(int id)
        {
            var response = categoryService.Remove(id);
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "viewer")]

        public BaseResponse<CategoryDto> Create([FromBody] CategoryDto dto)
        {
            var response = categoryService.Insert(dto);
            return response;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<CategoryDto> Update(int id, [FromBody] CategoryDto dto)
        {
            var response = categoryService.Update(id, dto);
            return response;
        }
    }
}