using AutoMapper;
using Base.Response;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.OfferService.Abstract;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OfferController : ControllerBase
    {
        private readonly IOfferService offerService;
        private readonly IMapper mapper;


        public OfferController(IOfferService offerService, IMapper mapper)
        {
            this.offerService = offerService;
            this.mapper = mapper;
        }


        [HttpGet]
        //[Authorize(Roles = "viewer")]
        public BaseResponse<IEnumerable<OfferDto>> GetAll()
        {
            var response = offerService.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public BaseResponse<OfferDto> GetById(int id)
        {
            var response = offerService.GetById(id);
            return response;
        }

        [HttpDelete("{id}")]
        public BaseResponse<OfferDto> Delete(int id)
        {
            var response = offerService.Remove(id);
            return response;
        }

        [HttpPost]
        public BaseResponse<OfferDto> GiveOffer([FromBody] OfferDto dto)
        {
            var response = offerService.Insert(dto);
            return response;
        }

        [HttpPut("{id}")]
        public BaseResponse<OfferDto> Update(int id, [FromBody] OfferDto dto)
        {
            var response = offerService.Update(id, dto);
            return response;
        }
    }
}