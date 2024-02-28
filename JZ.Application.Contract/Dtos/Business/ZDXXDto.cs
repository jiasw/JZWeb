using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    public class ZDXXDto
    {
        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        
        public int ID { get; set; }

        /// <summary>
        /// Desc:字典分类编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZDFLBM { get; set; }

        /// <summary>
        /// Desc:字典分类名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZDFLMC { get; set; }

        /// <summary>
        /// Desc:字典编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZDBM { get; set; }

        /// <summary>
        /// Desc:字典名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZDMC { get; set; }

        /// <summary>
        /// Desc:字典顺序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ZDSX { get; set; }

        /// <summary>
        /// Desc:删除标志;0未删除1已删除
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte Del { get; set; } = 0;

        /// <summary>
        /// 字典图标
        /// </summary>
        public string BY1 { get; set; }
    }
}
