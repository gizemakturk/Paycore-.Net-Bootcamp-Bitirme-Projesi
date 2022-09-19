using AutoMapper;
using Data.Model;
using Data.Repository;
using Dto;
using NHibernate;
using Service.Base.Concrete;
using Service.CategoryService.Abstract;
using Service.OfferService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OfferService.Concrete
{
    public class OfferService : BaseService<OfferDto, Offer>, IOfferService
    {

        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Offer> hibernateRepository;

        public OfferService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Offer>(session);
        }



    }
}