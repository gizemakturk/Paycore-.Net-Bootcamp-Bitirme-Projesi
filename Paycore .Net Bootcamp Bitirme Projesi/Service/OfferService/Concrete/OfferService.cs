using AutoMapper;
using Base.Response;
using Data.Model;
using Data.Repository;
using Dto;
using FluentNHibernate.Data;
using NHibernate;
using Service.AuthenticatedUserServices.Abstract;
using Service.Base.Concrete;
using Service.CategoryService.Abstract;
using Service.OfferService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.OfferService.Concrete
{
    public class OfferService : BaseService<OfferDto, Offer>, IOfferService
    {

        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Offer> hibernateOfferRepository;
        protected readonly IHibernateRepository<Product> hibernateProductRepository;
        protected readonly IHibernateRepository<User> hibernateUserRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        public OfferService(ISession session, IMapper mapper, IAuthenticatedUserService authenticatedUser) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;
            _authenticatedUser = authenticatedUser;
            hibernateOfferRepository = new HibernateRepository<Offer>(session);
            hibernateProductRepository = new HibernateRepository<Product>(session);
        }

        public override BaseResponse<OfferDto> Insert(OfferDto insertResource)
        {
            Console.WriteLine(_authenticatedUser.UserId);
            
            var product = hibernateProductRepository.Where(x => x.Id.Equals(insertResource.ProductId)).FirstOrDefault();
            if (product == null)
                return new BaseResponse<OfferDto>("Product not found");
            if (product.isOfferable)
            {
                var tempEntity = mapper.Map<OfferDto, Offer>(insertResource);
                tempEntity.UserId = _authenticatedUser.UserId;
                hibernateRepository.BeginTransaction();
                hibernateRepository.Save(tempEntity);
                hibernateRepository.Commit();

                hibernateRepository.CloseTransaction();
                return new BaseResponse<OfferDto>(mapper.Map<Offer, OfferDto>(tempEntity));
            }
            return new BaseResponse<OfferDto>("Product can not be offerable.");
        }

    }
}