using ChatApplication.Application.DTOs.Auth.Response;
using ChatApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.Mappings
{
    public static class UserMappings
    {
        public static UserResponse ToUserResponse(this ApplicationUser user, string? accessToken)
        {
            return new UserResponse(
                user.Username,
                accessToken
            );
        }
    }
}
