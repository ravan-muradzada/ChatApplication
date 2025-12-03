using ChatApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.ExternalServiceInterfaces
{
    public interface IAccessTokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}
