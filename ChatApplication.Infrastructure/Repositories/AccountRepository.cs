using ChatApplication.Application.ExternalServiceInterfaces;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.RepositoryInterfaces;
using ChatApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        #region Fields
        private readonly IPasswordHasher _passwordHasher;
        private readonly ApplicationDbContext _dbContext;
        #endregion

        #region Constructor
        public AccountRepository(IPasswordHasher passwordHasher, ApplicationDbContext dbContext)
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
        }
        #endregion

        public async Task CreateAccountAsync(ApplicationUser user, string password, CancellationToken ct = default)
        {
            user.HashedPassword = _passwordHasher.Hash(password);
            await _dbContext.Users.AddAsync(user, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<ApplicationUser?> FindUserAsync(string username, string password, CancellationToken ct)
        {
            var query = _dbContext.Users.AsQueryable();

            query = query.Where(u => u.Username == username);
            query = query.Where(u => _passwordHasher.VerifyPassword(password, u.HashedPassword));
            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<ApplicationUser?> FindUsernameAsync(string username, CancellationToken ct = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username, ct);
        }
    }
}
