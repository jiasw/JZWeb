using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace JZ.Domain.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("jz_yw_userinfo")]
    public partial class JZ_YW_USERINFO
    {
           public JZ_YW_USERINFO(){


           }
           /// <summary>
           /// Desc:雪花ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public long ID {get;set;}

           /// <summary>
           /// Desc:用户的微信昵称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string NickName {get;set;}

           /// <summary>
           /// Desc:用户预留的真实姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RealName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Sex {get;set;}

           /// <summary>
           /// Desc:手机号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Phone {get;set;}

           /// <summary>
           /// Desc:头像地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Photo {get;set;}

           /// <summary>
           /// Desc:微信用户id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WXID {get;set;}

           /// <summary>
           /// Desc:微信云平台id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WXunionid {get;set;}

           /// <summary>
           /// Desc:会话id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SessionKey {get;set;}

           /// <summary>
           /// Desc:用户来源
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YHLY {get;set;}

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
