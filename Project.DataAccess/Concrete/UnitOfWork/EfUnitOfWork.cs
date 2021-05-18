using Project.Entities.DbContext;

namespace Project.DataAccess.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {

        private readonly ProjectDbContext _context;

        public EfUnitOfWork(ProjectDbContext context)
        {
            _context = context;
        }


        public int Commit()
        {
            return _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}