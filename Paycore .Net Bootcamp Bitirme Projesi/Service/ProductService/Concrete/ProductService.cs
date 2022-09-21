using AutoMapper;
using Base.Response;
using Data.Model;
using Data.Repository;
using Dto;
using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Transform;
using Serilog;
using Service.AuthenticatedUserServices.Abstract;
using Service.Base.Concrete;
using Service.ProductService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ProductService.Concrete
{
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {

        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Product> hibernateRepository;
        protected readonly IHibernateRepository<Category> hibernateCategoryRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;


        public ProductService(ISession session, IMapper mapper, IAuthenticatedUserService authenticatedUser) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Product>(session);
            _authenticatedUser = authenticatedUser;
            this.hibernateCategoryRepository = new HibernateRepository<Category>(session);
        }

        public override BaseResponse<IEnumerable<ProductDto>> GetAll()
        {
            try
            {
                var tempEntity = session.CreateSQLQuery("SELECT * FROM product WHERE userid='" + _authenticatedUser.UserId + "'").SetResultTransformer(Transformers.AliasToBean<Product>()).List<Product>().ToList();
                var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(tempEntity);
                return new BaseResponse<IEnumerable<ProductDto>>(result);
           
            }
            catch (Exception ex)
            {
                Log.Error("BaseService.GetAll", ex);
                return new BaseResponse<IEnumerable<ProductDto>>(ex.Message);
            }
        }

        public BaseResponse<IEnumerable<ProductDto>> GetAllProductsByCategoryId(int categoryId)
        {
            try
            {
                var tempEntity = session.CreateSQLQuery("SELECT * FROM product WHERE userid='" + _authenticatedUser.UserId + "' AND categoryid=" + categoryId).SetResultTransformer(Transformers.AliasToBean<Product>()).List<Product>().ToList();
                var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(tempEntity);
                return new BaseResponse<IEnumerable<ProductDto>>(result);
                //var products = hibernateRepository.Entities.Where(x => x.Category != null && x.CategoryId == categoryId && x.UserId != null && x.UserId.Equals(_authenticatedUser.UserId)).ToList();
                //var result = mapper.Map<List<Product>, List<ProductDto>>(products);
                //return new BaseResponse<IEnumerable<ProductDto>>(result);
            }
            catch (Exception ex)
            {
                Log.Error("BaseService.GetAll", ex);
                return new BaseResponse<IEnumerable<ProductDto>>(ex.Message);
            }
        }

        public override BaseResponse<ProductDto> Insert(ProductDto insertResource)
        {
            try
            {
                var tempEntity = mapper.Map<ProductDto, Product>(insertResource);
                tempEntity.CategoryId = insertResource.CategoryId;
                tempEntity.Category = hibernateCategoryRepository.GetById(insertResource.CategoryId);
                tempEntity.UserId = _authenticatedUser.UserId;
                tempEntity.User = session.Get<User>(_authenticatedUser.UserId);
                var result = mapper.Map<Product, ProductDto>(tempEntity);
                hibernateRepository.BeginTransaction();
                hibernateRepository.Save(tempEntity);
                hibernateRepository.Commit();

                hibernateRepository.CloseTransaction();
                return new BaseResponse<ProductDto>(result);
            }
            catch (Exception ex)
            {
                Log.Error("BaseService.Insert", ex);
                return new BaseResponse<ProductDto>(ex.Message);
            }
        }

        public BaseResponse<ProductDto> Sold(int productId)
        {
            try
            {
                var tempEntity = hibernateRepository.GetById(productId);

                tempEntity.UserId = _authenticatedUser.UserId;
                tempEntity.isSold = true;
                tempEntity.isOfferable = true;
                ProductDto resource = mapper.Map<Product, ProductDto>(tempEntity);

                hibernateRepository.BeginTransaction();
                hibernateRepository.Update(tempEntity);
                hibernateRepository.Commit();
                hibernateRepository.CloseTransaction();
                return new BaseResponse<ProductDto>(resource);
            }
            catch (Exception ex)
            {
                Log.Error("BaseService.GetById", ex);
                return new BaseResponse<ProductDto>(ex.Message);
            }
        }
    }
}
