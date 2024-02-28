using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderRepository : BaseRepository<Domain.Entitys.JZ_YW_ORDER>
    {
        private readonly ISqlSugarClient db;

        public OrderRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
