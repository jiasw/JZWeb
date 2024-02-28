using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 服务信息管理
    /// </summary>
    public class FWXXRepository : BaseRepository<Domain.Entitys.JZ_YW_FWXX>
    {
        private readonly ISqlSugarClient db;

        public FWXXRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
