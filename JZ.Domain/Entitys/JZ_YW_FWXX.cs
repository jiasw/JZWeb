using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace JZ.Domain.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("jz_yw_fwxx")]
    public partial class JZ_YW_FWXX
    {
           public JZ_YW_FWXX(){


           }
           /// <summary>
           /// Desc:雪花ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long ID {get;set;}

           /// <summary>
           /// Desc:服务类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FWLX {get;set;}

           /// <summary>
           /// Desc:服务封面
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Pic {get;set;}

           /// <summary>
           /// Desc:服务详情banner，用逗号分隔
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Banner {get;set;}

           /// <summary>
           /// Desc:服务项目
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:服务简介
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Summary {get;set;}

           /// <summary>
           /// Desc:服务详情
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Context {get;set;}

           /// <summary>
           /// Desc:服务价格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Price {get;set;}

           /// <summary>
           /// Desc:价格单位，小时
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Unit {get;set;}

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
           /// Desc:排列顺序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SX {get;set;}

           /// <summary>
           /// Desc:是否为优质服务
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IsGood {get;set;}

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
