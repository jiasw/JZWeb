using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class SysMangerRepository : BaseRepository<Domain.Entitys.JZ_SYS_MANGER>
    {
        private readonly ISqlSugarClient sugarClient;

        public SysMangerRepository(ISqlSugarClient db) : base(db)
        {
            this.sugarClient = db;
        }
    }
}
