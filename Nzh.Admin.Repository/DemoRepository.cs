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
        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        public async Task<List<Demo>> GetDemoList()
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo]";
            return await GetList(sql);
        }

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Demo> GetDemo(Guid ID)
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo] WHERE ID=@ID";
            return await Get(ID, sql);
        }

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddDemo(Demo entity)
        {
            string sql = @"INSERT INTO [dbo].[Demo](ID, Name, Sex, Age, Remark) VALUES(@ID, @Name, @Sex, @Age, @Remark)";
            await Add(entity, sql);
        }

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateDemo(Demo entity)
        {
            string sql = "UPDATE [dbo].[Demo] SET Name=@Name, Sex=@Sex, Age=@Age, Remark=@Remark WHERE ID=@ID";
            await Update(entity, sql);
        }

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteDemo(Guid ID)
        {
            string sql = "DELETE FROM [dbo].[Demo] WHERE ID=@ID";
            await Delete(ID, sql);
        }
    }
}
