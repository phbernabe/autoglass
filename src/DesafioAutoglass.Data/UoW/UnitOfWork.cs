namespace DesafioAutoglass.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool isRollbackOnly;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            isRollbackOnly = false;
        }

        public bool Commit()
        {
            if (!isRollbackOnly)
            {
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        public void Rollback()
        {
            isRollbackOnly = true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
