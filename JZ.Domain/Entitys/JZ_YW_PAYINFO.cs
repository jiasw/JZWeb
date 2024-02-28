using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace JZ.Domain.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("jz_yw_payinfo")]
    public partial class JZ_YW_PAYINFO
    {
           public JZ_YW_PAYINFO(){


           }
           /// <summary>
           /// Desc:雪花ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long ID {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? UserID {get;set;}

           /// <summary>
           /// Desc:本地订单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? OrderID {get;set;}

           /// <summary>
           /// Desc:微信支付订单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PayOrderNO {get;set;}

           /// <summary>
           /// Desc:支付方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PayType {get;set;}

           /// <summary>
           /// Desc:支付状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PayState {get;set;}

           /// <summary>
           /// Desc:回调状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CallState {get;set;}

           /// <summary>
           /// Desc:支付标题
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PayTitle {get;set;}

           /// <summary>
           /// Desc:支付备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PayRemark {get;set;}

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
           /// Desc:添加时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AddTime {get;set;}

           /// <summary>
           /// Desc:添加人员
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
