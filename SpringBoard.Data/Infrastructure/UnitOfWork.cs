using SpringBoard.Data;
using SpringBoard.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringBoard.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
       
         private DatabContext dataContext;

        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            dataContext = dbFactory.DataContext;
        }

        public IRepositoryUser repositoryUser;

        public IRepositoryUser RepositoryUser => repositoryUser = repositoryUser ?? new RepositoryUser(dbFactory);


        public void Commit()
        {
            dataContext.SaveChanges();
        }
      
        public void Dispose()
        {
            dataContext.Dispose();
        }
        public IRepositoryBase<T> getRepository<T>() where T : class
        {
            return new RepositoryBase<T>(dbFactory);
        }
      
    }
}
