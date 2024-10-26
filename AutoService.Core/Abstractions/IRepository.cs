using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Core.Abstractions
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetByName(string name);
        Task<Guid> Add(T entity);
        Task<Guid> Update(T entity);
        Task<Guid> Delete(T entity);
    }
}
