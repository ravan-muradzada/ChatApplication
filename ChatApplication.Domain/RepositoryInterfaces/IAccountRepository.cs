using ChatApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Domain.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task CreateAccountAsync(ApplicationUser user, string password, CancellationToken ct = default);
        Task<ApplicationUser?> FindUsernameAsync(string username, CancellationToken ct = default);
        Task<ApplicationUser?> FindUserAsync(string username, string password, CancellationToken ct);
    }
}
