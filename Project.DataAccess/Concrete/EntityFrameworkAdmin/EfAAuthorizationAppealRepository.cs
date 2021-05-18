using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.EntityFramework
{
    public class EfAAuthorizationAppealRepository : EfARepository<AuthorizationAppeal>, IAuthorizationAppealRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAAuthorizationAppealRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }


        

        public AuthorizationAppealDto GetIncludedAppealById(long appealId)
        {
            return _context.AuthorizationAppeals
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Where(x => x.Id == appealId)
                .Select(y => new AuthorizationAppealDto()
                {
                    Id = y.Id,
                    AppealStatus = y.AppealStatus,
                    AppealDescription = y.AppealDescription,
                    Created = y.Created,
                    isDeleted = y.IsDeleted,

                    User = new UserDto()
                    {
                        Id = y.UserId,
                        Name = y.User.Name,
                        Lastname = y.User.Lastname,
                        Picture = y.User.Picture,
                        isDeleted = y.User.IsDeleted
                    }
                }).FirstOrDefault();
        }
    }
}