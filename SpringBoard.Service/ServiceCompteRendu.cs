using SpringBoard.Data.Infrastructure;
using SpringBoard.Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringBoard.Service
{
    public class ServiceCompteRendu : Service<CompteRendu>, IServiceCompteRendu
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork utwk = new UnitOfWork(dbf);

        protected ServiceCompteRendu() : base(utwk)
        {
        }

        



    }
}
