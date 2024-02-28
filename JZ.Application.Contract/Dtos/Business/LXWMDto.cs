using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    public class LXWMDto
    {
        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        
        public long ID { get; set; }

        /// <summary>
        /// Desc:简介
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:公司地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Address { get; set; }

        /// <summary>
        /// Desc:公司电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Phone { get; set; }

        /// <summary>
        /// Desc:邮箱地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// Desc:编辑时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EditTime { get; set; }

        /// <summary>
        /// Desc:编辑人员
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EditUser { get; set; }

        /// <summary>
        /// Desc:删除标志，0未删除；1已删除
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte Del { get; set; } = 0;
    }
}
