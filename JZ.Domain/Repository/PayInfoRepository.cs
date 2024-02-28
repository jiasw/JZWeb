using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 支付信息
    /// </summary>
    public class PayInfoRepository : BaseRepository<Domain.Entitys.JZ_YW_PAYINFO>
    {
        private readonly ISqlSugarClient db;

        public PayInfoRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
