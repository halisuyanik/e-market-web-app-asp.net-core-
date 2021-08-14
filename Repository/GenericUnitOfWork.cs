using on_e_commerce.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace on_e_commerce.Repository
{
    public class GenericUnitOfWork : IDisposable
    {
        private readonly dbEticaretEntities DBEntity = new dbEticaretEntities();
        public IRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericRepository<Tbl_EntityType>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}