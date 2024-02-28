using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 评论信息管理
    /// </summary>
    public class AssessReository : BaseRepository<Domain.Entitys.JZ_YW_ASSESS>
    {
        private readonly ISqlSugarClient db;

        public AssessReository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
