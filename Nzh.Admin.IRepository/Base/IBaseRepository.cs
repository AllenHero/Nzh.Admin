using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IRepository.Base
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity, string sql);

        Task Delete(Guid Id, string sql);

        Task Update(T entity, string sql);

        Task<T> Get(Guid Id, string sql);

        Task<List<T>> GetList(string sql);

        Task<List<T>> ExecuteSP(string SpName);
    }
}
