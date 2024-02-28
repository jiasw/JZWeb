using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// Banner图管理
    /// </summary>
    public class BannerRepository : BaseRepository<Domain.Entitys.JZ_YW_PIC>
    {
        private readonly ISqlSugarClient db;

        public BannerRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
