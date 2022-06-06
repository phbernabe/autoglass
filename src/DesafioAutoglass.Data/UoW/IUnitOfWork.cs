using System;

namespace DesafioAutoglass.Data.UoW
{
    public interface IUnitOfWork: IDisposable
    {
        bool Commit();
        void Rollback();
    }
}
