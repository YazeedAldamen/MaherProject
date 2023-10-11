using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UnitOfWorkRepositories: IDisposable, IUnitOfWorkRepositories
    {
        private readonly IEfContextFactory _efContextFactory;
        public IGenericRepository<Blog> BlogRepository { get; set; }

        public UnitOfWorkRepositories(IEfContextFactory efContextFactory)
        {
            _efContextFactory = efContextFactory;
            BlogRepository= new GenericRepository<Blog>(efContextFactory);
        }


        public void Dispose()
        {
        }
    }
}
