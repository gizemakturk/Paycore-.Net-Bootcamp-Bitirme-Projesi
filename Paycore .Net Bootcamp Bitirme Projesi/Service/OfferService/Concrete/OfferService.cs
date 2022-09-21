using AutoMapper;
using Base.Response;
using Data.Model;
using Data.Repository;
using Dto;
using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using NHibernate.Util;
using Serilog;
using Serilog.Core;
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

        public override BaseResponse<IEnumerable<OfferDto>> GetAll()
        {
            try
            {
                var tempEntity = session.CreateSQLQuery("SELECT * FROM offer WHERE userid='" + _authenticatedUser.UserId + "'").SetResultTransformer(Transformers.AliasToBean<Offer>()).List<Offer>().ToList();
                var result = mapper.Map<IEnumerable<Offer>, IEnumerable<OfferDto>>(tempEntity);
                return new BaseResponse<IEnumerable<OfferDto>>(result);
                
            }
            catch (Exception ex)
            {
                Log.Error("BaseService.GetAll", ex);
                return new BaseResponse<IEnumerable<OfferDto>>(ex.Message);
            }
        }

        public  BaseResponse<ProductDto> AcceptOffer(int id)
        {

            var tempEntity = session.CreateSQLQuery("SELECT * FROM offer WHERE id=" + id).SetResultTransformer(Transformers.AliasToBean<Offer>()).List<Offer>().ToList().FirstOrDefault();
          //  var productResult = (from product in session.Query<Product>() join category in session.Query<Category>() on product.CategoryId equals category.Id join user in session.Query<User>() on product.UserId equals user.Id select product).ToList().Where(x => x.Id.Equals(tempEntity.ProductId)).FirstOrDefault();
            var product = session.CreateSQLQuery("SELECT p.* FROM product p, category c, \"user\" u WHERE p.id=" + tempEntity.ProductId+ " AND c.id=p.categoryid AND u.id=p.userid").SetResultTransformer(Transformers.AliasToBean<Product>()).List<Product>().ToList().FirstOrDefault();
            if (product == null)
            {
                return new BaseResponse<ProductDto>("Product not found");
            }
            
            if (product.UserId.Equals(_authenticatedUser.UserId))
            {
                product.UserId = tempEntity.UserId;
                product.User = session.Get<User>(tempEntity.UserId);
                product.isSold = true;
                product.Category = session.Get<Category>(product.CategoryId);
                product.CategoryId = product.CategoryId;
                product.isOfferable = false;
                hibernateProductRepository.BeginTransaction();
                hibernateProductRepository.Update(product);
                hibernateProductRepository.Commit();
                hibernateProductRepository.CloseTransaction();
                return new BaseResponse<ProductDto>(mapper.Map<Product, ProductDto>(product));
            }
            return new BaseResponse<ProductDto>("You are not the own this product.");
        }

        public BaseResponse<IEnumerable<OfferDto>> GetAllOffers()
        {
            var result = new List<Offer>();

            try
            {
                var tempProductEntity = session.CreateSQLQuery("SELECT * FROM product WHERE userid='" + _authenticatedUser.UserId + "'").SetResultTransformer(Transformers.AliasToBean<Product>()).List<Product>().ToList();
                foreach (var item in tempProductEntity)
                {
                    var tempEntity = session.CreateSQLQuery("SELECT * FROM offer WHERE productid=" + item.Id).SetResultTransformer(Transformers.AliasToBean<Offer>()).List<Offer>().ToList();
                    result.AddRange(tempEntity);
                
           
}
                   var resultDto = mapper.Map<IEnumerable<Offer>, IEnumerable<OfferDto>>(result);
                   return new BaseResponse<IEnumerable<OfferDto>>(resultDto);
            }
            catch (Exception ex)
            {
                Log.Error("BaseService.GetAll", ex);
                return new BaseResponse<IEnumerable<OfferDto>>(ex.Message);
            }
        }

        public override BaseResponse<OfferDto> Insert(OfferDto insertResource)
        {
            var product = hibernateProductRepository.Where(x => x.Id.Equals(insertResource.ProductId)).FirstOrDefault();
            if (product == null)
                return new BaseResponse<OfferDto>("Product not found");
            if (product.isOfferable)
            {
                var tempEntity = mapper.Map<OfferDto, Offer>(insertResource);
                tempEntity.UserId = _authenticatedUser.UserId;
                tempEntity.User = session.Get<User>(_authenticatedUser.UserId);
                tempEntity.ProductId = insertResource.ProductId;
                tempEntity.Product = product;
                hibernateRepository.BeginTransaction();
                hibernateRepository.Save(tempEntity);
                hibernateRepository.Commit();

                hibernateRepository.CloseTransaction();
                return new BaseResponse<OfferDto>(mapper.Map<Offer, OfferDto>(tempEntity));
            }
            return new BaseResponse<OfferDto>("Product can not be offerable.");
        }

        public BaseResponse<string> DeclineOffer(int id)
        {
            try
            {
                session.Query<Offer>().Where(x => x.Id.Equals(id)).Delete();
                
                return new BaseResponse<string>("Offer is declined.");
            }
            catch (Exception)
            {
                
                return new BaseResponse<string>("Offer can not be declined.");
            }
            
           
        }
    }
}