using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class LXWMRepository : BaseRepository<Domain.Entitys.JZ_YW_LXWM>
    {
        private readonly ISqlSugarClient db;

        public LXWMRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
