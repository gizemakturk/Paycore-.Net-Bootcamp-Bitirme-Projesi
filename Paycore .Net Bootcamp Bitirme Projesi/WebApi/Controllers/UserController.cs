using AutoMapper;
using Base.Response;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Service.UserService.Abstract;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;


        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }


        [HttpGet]
        public BaseResponse<IEnumerable<UserDto>> GetAll()
        {
            var response = userService.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public BaseResponse<UserDto> GetById(int id)
        {
            var response = userService.GetById(id);
            return response;
        }

        [HttpDelete("{id}")]
        public BaseResponse<UserDto> Delete(int id)
        {
            var response = userService.Remove(id);
            return response;
        }

        [HttpPost]
        public BaseResponse<UserDto> Create([FromBody] UserDto dto)
        {
            var response = userService.Insert(dto);
            return response;
        }

        [HttpPut("{id}")]
        public BaseResponse<UserDto> Update(int id, [FromBody] UserDto dto)
        {
            var response = userService.Update(id, dto);
            return response;
        }
    }
}