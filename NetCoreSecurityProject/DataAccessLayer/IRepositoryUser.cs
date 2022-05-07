using EntityLayer;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryUser<T> : IRepository<User>
    {
        Task<List<User>> IsUserExist(Guid ID);
        Task<User> GetUserForLogin(string email);
        Task<User> GetUserFromAccessToken(int userID);
        Task<User> GetAllUsersByEmailAndPhone(string email);
        User GetUserByGuidID(Guid userID);
    }
    public class RepositoryUser<T> : Repository<User>, IRepositoryUser<T>
    {
        public RepositoryUser(ApiDbContext context) : base(context) { }

        public async Task<List<User>> IsUserExist(Guid ID)
        {
            return await ApiDbContext.Users.Where(x => x.UserGuidID == ID)
                .Select(x => new User
                {
                    UserID = x.UserID,
                    UserGuidID = x.UserGuidID
                }).AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserForLogin(string email)
        {
            return await ApiDbContext.Users.Where(x => x.UserEmail == email)
                .Select(x => new User
                {
                    UserID = x.UserID,
                    UserGuidID = x.UserGuidID,
                    UserEmail = x.UserEmail,
                    UserPassword = x.UserPassword
                }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<User> GetUserFromAccessToken(int userID)
        {
            return await ApiDbContext.Users.Where(x => x.UserID == userID)
                .AsNoTracking()
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

        public User GetUserByGuidID(Guid userID)
        {
            return  ApiDbContext.Users.Where(x => x.UserGuidID == userID)
                .Select(x => new User
                {
                    UserID = x.UserID,
                    UserGuidID=x.UserGuidID
                }).AsNoTracking().Single();
        }
    }
}
