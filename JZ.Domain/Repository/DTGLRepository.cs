using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 动态管理
    /// </summary>
    public class DTGLRepository : BaseRepository<Domain.Entitys.JZ_YW_DTGL>
    {
        private readonly ISqlSugarClient db;

        public DTGLRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
