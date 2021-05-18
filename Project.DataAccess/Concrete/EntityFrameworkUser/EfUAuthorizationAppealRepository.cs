using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUAuthorizationAppealRepository : EfURepository<AuthorizationAppeal>, IAuthorizationAppealRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfUAuthorizationAppealRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }





        public AuthorizationAppealDto GetIncludedAppealById(long appealId)
        {
            return  _context.AuthorizationAppeals
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Where(x => x.Id == appealId)
                    .Select(y => new AuthorizationAppealDto()
                    {
                        Id = y.Id,
                        AppealStatus = y.AppealStatus,
                        AppealDescription = y.AppealDescription,
                        Created = y.Created,

                        User = new UserDto()
                        {
                            Id = y.UserId,
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture
                        }
                    }).FirstOrDefault();
        }
    }
}