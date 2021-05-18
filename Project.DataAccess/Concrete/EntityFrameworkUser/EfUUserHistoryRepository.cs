using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUUserHistoryRepository : EfURepository<UserHistory>, IUserHistoryRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfUUserHistoryRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }




        //public List<UserHistoryDto> GerUserHistories(long userId)
        //{

        //    return  _context.UserHistory
        //            .AsNoTracking()
        //            .Where(x => x.UserId == userId)
        //            .OrderByDescending(x => x.Created)
        //            .Take(30)
        //            .Select(y => new UserHistoryDto()
        //            {
        //                Id = y.Id,
        //                HistoryType = y.HistoryType,
        //                UserId = y.UserId,
        //                ArticleId = y.ArticleId,
        //                ArticleCommentId = y.ArticleCommentId,
        //                TopicId = y.TopicId,
        //                TopicCommentId = y.TopicCommentId,
        //                Created = y.Created,

        //            }).ToList();
        //}


    }
}