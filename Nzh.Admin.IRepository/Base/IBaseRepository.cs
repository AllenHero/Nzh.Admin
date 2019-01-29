using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IRepository.Base
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity, string Sql);

        Task Update(T entity, string Sql);

        Task Delete(Guid Id, string Sql);

        Task<List<T>> GetList(string Sql);

        Task<T> Get(Guid Id, string Sql);

        Task<List<T>> ExecuteSP(string SpName);
    }
}
