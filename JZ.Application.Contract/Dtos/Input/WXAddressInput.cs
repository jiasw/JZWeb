using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Input
{
    /// <summary>
    /// 小程序端编辑保存常用地址
    /// </summary>
    public class WXAddressInput
    {
        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }


        /// <summary>
        /// Desc:用户id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UserID { get; set; }

        /// <summary>
        /// 微信的openid
        /// </summary>
        public string openID { get; set; }

        

        /// <summary>
        /// Desc:用户地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Address { get; set; }

        /// <summary>
        /// Desc:用户电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Phone { get; set; }

        /// <summary>
        /// Desc:默认标志
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DefaultFlag { get; set; }
    }
}
