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
    }
    public class RepositoryUser<T> : Repository<User>, IRepositoryUser<T>
    {
        public RepositoryUser(ApiDbContext context) : base(context) { }

        public async Task<List<User>> IsUserExist(int id)
        {
            return await ApiDbContext.Users.Where(x=>x.UserID==id)
                .Select(x=>new User
                {
                    UserID = x.UserID
                }).ToListAsync();
        }
    }
}
