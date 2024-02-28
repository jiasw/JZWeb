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
    /// banner图
    /// </summary>
    public class BannerDto
    {

        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:图片名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:图片地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Addr { get; set; }

        /// <summary>
        /// Desc:图片顺序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? PicOrder { get; set; }

        /// <summary>
        /// Desc:删除标志，0未删除；1已删除
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte Del { get; set; }= 0;
    }
}
