using EntityLayer;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryUser<T> : IRepository<User>
    {
        Task<List<User>> IsUserExist(int id);
        Task<User> GetUserForLogin(string email);
        Task<User> GetUserFromAccessToken(int userID);
        Task<User> GetAllUsersByEmailAndPhone(string email);
    }
    public class RepositoryUser<T> : Repository<User>, IRepositoryUser<T>
    {
        public RepositoryUser(ApiDbContext context) : base(context) { }

        public async Task<List<User>> IsUserExist(int id)
        {
            return await ApiDbContext.Users.Where(x => x.UserID == id)
                .Select(x => new User
                {
                    UserID = x.UserID
                }).ToListAsync();
        }

        public async Task<User> GetUserForLogin(string email)
        {
            return await ApiDbContext.Users.Where(x => x.UserEmail == email)
                .Select(x => new User
                {
                    UserID = x.UserID,
                    UserEmail = x.UserEmail,
                    UserPassword = x.UserPassword
                }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<User> GetUserFromAccessToken(int userID)
        {
            return await ApiDbContext.Users.Where(x => x.UserID == userID)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetAllUsersByEmailAndPhone(string email)
        {
            return await ApiDbContext.Users.Where(x => x.UserEmail == email)
                .Select(x => new User
                {
                    UserID = x.UserID
                }).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
