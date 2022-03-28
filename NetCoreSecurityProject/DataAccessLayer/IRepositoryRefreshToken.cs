using Entities_HBKSOFTWARE.JwtModels;
using EntityLayer;
using System.Linq;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryRefreshToken<T> : IRepository<RefreshToken>
    {
        RefreshToken ValidateRefreshToken(string refreshToken);
    }
    public class RepositoryRefreshToken<T> : Repository<RefreshToken>, IRepositoryRefreshToken<T>
    {
        public RepositoryRefreshToken(ApiDbContext context) : base(context) { }

        public RefreshToken ValidateRefreshToken(string refreshToken)
        {
            return ApiDbContext.RefreshTokens.Where(x => x.Token == refreshToken)
                     .OrderByDescending(x => x.ExpiryDate)
                     .FirstOrDefault();
        }
    }
}
