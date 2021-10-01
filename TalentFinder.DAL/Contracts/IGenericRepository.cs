using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.DAL.Contracts
{
    public interface IGenericRepository<T> where T : class, new()
    {
        void Create(T obj);

        void Remove(Guid id);

        void Update(T obj);

        T GetOne(Guid id);

        IEnumerable<T> GetAll();
    }
}