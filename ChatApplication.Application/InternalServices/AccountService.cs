using ChatApplication.Application.DTOs.Auth.Request;
using ChatApplication.Application.DTOs.Auth.Response;
using ChatApplication.Application.ExternalServiceInterfaces;
using ChatApplication.Application.InternalServiceInterfaces;
using ChatApplication.Application.Mappings;
using ChatApplication.Domain.CustomExceptions;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.InternalServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccessTokenService _accessTokenService;
        public AccountService(IAccountRepository accountRepository, IAccessTokenService accessTokenService)
        {
            _accountRepository = accountRepository;
            _accessTokenService = accessTokenService;
        }

        public async Task<UserResponse> RegisterAsync(AuthRequest request, CancellationToken ct = default)
        {
            if ((await _accountRepository.FindUsernameAsync(request.Username, ct)) is not null)
            {
                throw new ConflictException("Username has already taken!");
            }

            ApplicationUser user = new()
            {
                UserId = Guid.NewGuid(),
                Username = request.Username
            };

            await _accountRepository.CreateAccountAsync(user, request.Password, ct);
            string accessToken = _accessTokenService.GenerateToken(user);
            return user.ToUserResponse(accessToken);
        }

        public async Task<UserResponse> LoginAsync(AuthRequest request, CancellationToken ct = default)
        {
            ApplicationUser? user = await _accountRepository.FindUserAsync(request.Username, request.Password, ct) ?? throw new ObjectNotFoundException("User not found!");

            string accessToken = _accessTokenService.GenerateToken(user);
            return user.ToUserResponse(accessToken);
        }
    }
}
