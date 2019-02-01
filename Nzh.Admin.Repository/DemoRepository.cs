using Dapper;
using Nzh.Admin.Common.Base;
using Nzh.Admin.IRepository;
using Nzh.Admin.Model;
using Nzh.Admin.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Repository
{
    public class DemoRepository : BaseRepository<Demo>, IDemoRepository
    {
        
    }
}
