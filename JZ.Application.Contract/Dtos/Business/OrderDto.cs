using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    public class OrderDto
    {
        /// <summary>
        /// Desc:雪花ID;订单id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:微信支付订单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WXPayOrderID { get; set; }

        /// <summary>
        /// Desc:微信预支付id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WXPrepayId { get; set; }

        /// <summary>
        /// Desc:下单人id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UserID { get; set; }

        /// <summary>
        /// Desc:微信的openid
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WXID { get; set; }

        /// <summary>
        /// Desc:订单标题（用户名+服务项目名）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:预约订单码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Code { get; set; }

        /// <summary>
        /// Desc:订单服务地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Address { get; set; }

        /// <summary>
        /// Desc:订单服务时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OrderTime { get; set; }

        /// <summary>
        /// Desc:下单人姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OrderUserName { get; set; }

        /// <summary>
        /// Desc:下单人电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OrderPhone { get; set; }

        /// <summary>
        /// Desc:订单备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remarks { get; set; }

        /// <summary>
        /// Desc:订单状态，已下单，已支付，已开始，已完成
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OrderState { get; set; }

        /// <summary>
        /// Desc:服务项目ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? FWID { get; set; }

        /// <summary>
        /// Desc:服务数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? FWSL { get; set; }

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
        /// 下单人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string FWMC { get; set; }

        /// <summary>
        /// Desc:订单单价
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? Price { get; set; }

        /// <summary>
        /// Desc:订单总结
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? Amount { get; set; }

        /// <summary>
        /// 是否评价
        /// </summary>
        public bool IsPJ { get; set; }

    }
}
