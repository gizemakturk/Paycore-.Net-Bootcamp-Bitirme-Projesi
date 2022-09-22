using AutoMapper;
using Base.Response;
using Data.Model;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.EmailService.Abstract;
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
     private readonly IEmailService _emailService;


        public UserController(IUserService userService, IMapper mapper
            , IEmailService emailService
            )
        {
            this.userService = userService;
            this.mapper = mapper;
            _emailService = emailService;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]

        public BaseResponse<IEnumerable<UserDto>> GetAll()
        {
            var response = userService.GetAll();
          
            return response;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]

        public BaseResponse<UserDto> GetById(int id)
        {
            var response = userService.GetById(id);
            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public BaseResponse<UserDto> Delete(int id)
        {
            var response = userService.Remove(id);
            return response;
        }

        [HttpPost]
        public BaseResponse<UserDto> Create([FromBody] UserDto dto)
        {
          
            var response = userService.Insert(dto);
            MailRequest mail2 = new MailRequest() { Body = "Blocked", Status = false, Subject = "Access denied", ToEmail = response.Response.Email };
            _emailService.SendEmailIntoQueue(mail2);
            return response;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "viewer")]
        public BaseResponse<UserDto> Update(int id, [FromBody] UserDto dto)
        {
            var response = userService.Update(id, dto);
            return response;
        }
    }
}