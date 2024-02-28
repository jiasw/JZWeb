using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 用户地址管理
    /// </summary>
    public class AddressRepository : BaseRepository<Domain.Entitys.JZ_YW_ADDR>
    {
        private readonly ISqlSugarClient db;

        public AddressRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
