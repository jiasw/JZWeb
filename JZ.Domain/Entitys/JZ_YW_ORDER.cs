using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace JZ.Domain.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("jz_yw_order")]
    public partial class JZ_YW_ORDER
    {
           public JZ_YW_ORDER(){


           }
           /// <summary>
           /// Desc:雪花ID;订单id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public long ID {get;set;}

           /// <summary>
           /// Desc:微信支付订单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WXPayOrderID {get;set;}

           /// <summary>
           /// Desc:微信预支付id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WXPrepayId {get;set;}

           /// <summary>
           /// Desc:下单人id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? UserID {get;set;}

           /// <summary>
           /// Desc:微信的openid
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WXID {get;set;}

           /// <summary>
           /// Desc:订单标题（用户名+服务项目名）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:预约订单码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Code {get;set;}

           /// <summary>
           /// Desc:订单服务地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:订单服务时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrderTime {get;set;}

           /// <summary>
           /// Desc:下单人姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrderUserName {get;set;}

           /// <summary>
           /// Desc:下单人电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrderPhone {get;set;}

           /// <summary>
           /// Desc:订单单价
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Price {get;set;}

           /// <summary>
           /// Desc:订单总结
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Amount {get;set;}

           /// <summary>
           /// Desc:订单备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remarks {get;set;}

           /// <summary>
           /// Desc:订单状态，已下单，已支付，已开始，已完成
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrderState {get;set;}

           /// <summary>
           /// Desc:服务项目ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? FWID {get;set;}

           /// <summary>
           /// Desc:服务数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? FWSL {get;set;}

           /// <summary>
           /// Desc:编辑时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string EditTime {get;set;}

           /// <summary>
           /// Desc:编辑人员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string EditUser {get;set;}

           /// <summary>
           /// Desc:删除标志，0未删除；1已删除
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? Del {get;set;}

           /// <summary>
           /// Desc:删除时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DelTime {get;set;}

           /// <summary>
           /// Desc:删除人员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DelUser {get;set;}

           /// <summary>
           /// Desc:下单时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AddTime {get;set;}

           /// <summary>
           /// Desc:下单人员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AddUser {get;set;}

           /// <summary>
           /// Desc:备用1
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY1 {get;set;}

           /// <summary>
           /// Desc:备用2
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY2 {get;set;}

           /// <summary>
           /// Desc:备用3
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY3 {get;set;}

           /// <summary>
           /// Desc:备用4
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY4 {get;set;}

           /// <summary>
           /// Desc:备用5
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY5 {get;set;}

           /// <summary>
           /// Desc:备用6
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY6 {get;set;}

           /// <summary>
           /// Desc:备用7
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY7 {get;set;}

           /// <summary>
           /// Desc:备用8
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BY8 {get;set;}

    }
}
