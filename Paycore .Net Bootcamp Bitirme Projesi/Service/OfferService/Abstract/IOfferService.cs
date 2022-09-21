using Base.Response;
using Data.Model;
using Dto;
using Service.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OfferService.Abstract
{
    public interface IOfferService : IBaseService<OfferDto, Offer>
    {
        public BaseResponse<IEnumerable<OfferDto>> GetAllOffers();
        public BaseResponse<ProductDto> AcceptOffer(int id);
        public BaseResponse<string> DeclineOffer(int id);
    }
}