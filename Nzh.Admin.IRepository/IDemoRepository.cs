using Nzh.Admin.IRepository.Base;
using Nzh.Admin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IRepository
{
    public interface IDemoRepository : IBaseRepository<Demo> 
    {
        string ExecExecQueryParamSP(string spName, string name, int Id);

        Task<List<Demo>> GetUsers();

        Task PostUser(Demo entity);

        Task PutUser(Demo entity);

        Task DeleteUser(Guid Id);

        Task<Demo> GetUserDetail(Guid Id);
    }
}
