using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace JZ.Domain.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("jz_yw_assess")]
    public partial class JZ_YW_ASSESS
    {
           public JZ_YW_ASSESS(){


           }
           /// <summary>
           /// Desc:雪花ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public long ID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? UserID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? OrderID {get;set;}

           /// <summary>
           /// Desc:评价文本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Text {get;set;}

           /// <summary>
           /// Desc:评价图片，有多个图片用逗号隔开
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Pics {get;set;}

           /// <summary>
           /// Desc:评价等级
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Level {get;set;}

           /// <summary>
           /// Desc:是否是精选评价
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IsGood {get;set;}

           /// <summary>
           /// Desc:评价关键词，用逗号隔开
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string KeyWord {get;set;}

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
