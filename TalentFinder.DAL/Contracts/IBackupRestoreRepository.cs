using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.DAL.Contracts
{
    public interface IBackupRestoreRepository<T> : IGenericRepository<T> where T : class, new()
    {
        void Restore(T obj);
    }
}
