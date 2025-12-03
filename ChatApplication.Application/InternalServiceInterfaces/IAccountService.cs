using ChatApplication.Application.DTOs.Auth.Request;
using ChatApplication.Application.DTOs.Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.InternalServiceInterfaces
{
    public interface IAccountService
    {
        Task<UserResponse> RegisterAsync(AuthRequest request, CancellationToken ct = default);
        Task<UserResponse> LoginAsync(AuthRequest request, CancellationToken ct = default);
    }
}
