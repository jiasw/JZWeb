using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class MangerDto
    {

        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ID { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:登录名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LoginName { get; set; }

        /// <summary>
        /// Desc:登录密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LoginPsw { get; set; }

        /// <summary>
        /// Desc:删除标志;0未删除1已删除
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte Del { get; set; } = 0;
    }
}
