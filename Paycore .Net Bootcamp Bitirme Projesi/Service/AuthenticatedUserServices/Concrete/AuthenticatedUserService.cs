using Microsoft.AspNetCore.Http;
using Service.AuthenticatedUserServices.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenticatedUserServices.Concrete
{
        public class AuthenticatedUserService : IAuthenticatedUserService
        {
            public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
            {
                UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            public string UserId { get; }
        }
    }

