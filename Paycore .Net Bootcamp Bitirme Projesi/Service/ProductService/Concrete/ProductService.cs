using AutoMapper;
using Base.Response;
using Data.Model;
using Data.Repository;
using Dto;
using FluentNHibernate.Data;
using NHibernate;
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
        private readonly IAuthenticatedUserService _authenticatedUser;


        public ProductService(ISession session, IMapper mapper, IAuthenticatedUserService authenticatedUser) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Product>(session);
            _authenticatedUser = authenticatedUser;
        }

        public BaseResponse<IEnumerable<ProductDto>> GetAllProductsByCategoryId(int categoryId)
        {
            try
            {
                var tempEntity = hibernateRepository.Entities.ToList().Where(x => x.CategoryId == categoryId);
                var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(tempEntity);
                return new BaseResponse<IEnumerable<ProductDto>>(result);
            }
            catch (Exception ex)
            {
                Log.Error("BaseService.GetAll", ex);
                return new BaseResponse<IEnumerable<ProductDto>>(ex.Message);
            }
        }
      

        public BaseResponse<ProductDto> Sold(int productId)
        {
            try
            {
                var tempEntity = hibernateRepository.GetById(productId);

                tempEntity.UserId = Convert.ToInt32(_authenticatedUser.UserId);
                tempEntity.isSold = true;
                tempEntity.isOfferable = false;
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
