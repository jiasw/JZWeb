using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoRepository : BaseRepository<Domain.Entitys.JZ_YW_USERINFO>
    {
        private readonly ISqlSugarClient db;

        public UserInfoRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
