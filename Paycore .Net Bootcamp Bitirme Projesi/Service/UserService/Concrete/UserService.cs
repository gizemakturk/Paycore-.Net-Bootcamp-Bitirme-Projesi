using AutoMapper;
using Base.Response;
using Base.Token;
using Data.Model;
using Data.Repository;
using Dto;
using NHibernate;
using Service.AuthenticatedUserServices.Abstract;
using Service.Base.Concrete;
using Service.UserService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Service.UserService.Concrete
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {

        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<User> hibernateRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public UserService(ISession session, IMapper mapper, IAuthenticatedUserService authenticatedUser) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<User>(session);
            _authenticatedUser = authenticatedUser;
        }

        public BaseResponse<IEnumerable<OfferDto>> GetAllOffers()
        {
            var user = hibernateRepository.GetById(Convert.ToInt32(_authenticatedUser.UserId));
            var offers = user.Products.Select(x => x.Offers).Select(x => x.Where(y => y.UserId == user.Id));
            // mapper.Map<IList<Offer>, IList<OfferDto>>(offers);
            return new BaseResponse<IEnumerable<OfferDto>>("hata");
        }

        public override BaseResponse<UserDto> Insert(UserDto insertResource)
        {
            var account = hibernateRepository.Where(x => x.Email.Equals(insertResource.Email)).FirstOrDefault();
            if (account != null)
                return new BaseResponse<UserDto>("User has already exists");
            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(insertResource.Password));
            }
            insertResource.Password = Encoding.UTF8.GetString(hash);
            return base.Insert(insertResource);
        }
    }
}
