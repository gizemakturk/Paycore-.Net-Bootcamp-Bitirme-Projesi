using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenticatedUserServices.Abstract
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
