using AutoMapper;
using Base.Response;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ProductService.Abstract;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;


        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "viewer")]

        public BaseResponse<IEnumerable<ProductDto>> GetAll()
        {
            var response = productService.GetAll();
            return response;
        }
        [HttpGet("GetAllProductsByCategoryId")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<IEnumerable<ProductDto>> GetAllProductsByCategoryId(int categoryId)
        {
            var response = productService.GetAllProductsByCategoryId(categoryId);
            return response;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<ProductDto> GetById(int id)
        {
            var response = productService.GetById(id);
           
            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<ProductDto> Delete(int id)
        {
            var response = productService.Remove(id);
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "viewer")]

        public BaseResponse<ProductDto> Create([FromBody] ProductDto dto)
        {
            var response = productService.Insert(dto);
            return response;

        }
        [HttpPost("Sold")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<ProductDto> Sold(int productId)
        {
            var response = productService.Sold(productId);
            return response;

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "viewer")]

        public BaseResponse<ProductDto> Update(int id, [FromBody] ProductDto dto)
        {
            var response = productService.Update(id, dto);
            return response;
        }
    }
}