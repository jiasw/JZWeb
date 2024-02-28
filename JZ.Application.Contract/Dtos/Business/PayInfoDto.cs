using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    /// <summary>
    /// 支付信息
    /// </summary>
    public class PayInfoDto
    {
        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:用户ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UserID { get; set; }

        /// <summary>
        /// Desc:本地订单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? OrderID { get; set; }

        /// <summary>
        /// Desc:微信支付订单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PayOrderNO { get; set; }

        /// <summary>
        /// Desc:支付方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PayType { get; set; }

        /// <summary>
        /// Desc:支付状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PayState { get; set; }

        /// <summary>
        /// Desc:回调状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CallState { get; set; }

        /// <summary>
        /// Desc:支付标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PayTitle { get; set; }

        /// <summary>
        /// Desc:支付备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PayRemark { get; set; }

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

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
    }
}
