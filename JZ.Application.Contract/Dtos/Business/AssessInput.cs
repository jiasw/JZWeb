using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    /// <summary>
    /// 小程序端用户评价
    /// </summary>
    public class AssessInput
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string openid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long OrderID { get; set; }

        /// <summary>
        /// Desc:评价文本
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Text { get; set; }

        /// <summary>
        /// Desc:评价图片连接地址，用逗号分隔
        /// Default:
        /// Nullable:True
        /// </summary>        
        public string Pics { get; set; }


        /// <summary>
        /// Desc:评价等级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int Level { get; set; } = 0;

    }
}
