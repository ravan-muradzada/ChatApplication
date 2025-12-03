using ChatApplication.Application.DTOs.Auth.Request;
using ChatApplication.Application.DTOs.Auth.Response;
using ChatApplication.Application.InternalServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly IAccountService _accountService;
        #endregion

        #region Constructor
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        #region Register
        [HttpPost]
        public async Task<IActionResult> Register(AuthRequest request, CancellationToken ct = default)
        {
            UserResponse response = await _accountService.RegisterAsync(request, ct);
            return Ok(response);
        }
        #endregion

        #region Login
        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest request, CancellationToken ct = default)
        {
            UserResponse response = await _accountService.LoginAsync(request, ct);
            return Ok(response);
        }
        #endregion
    }
}
