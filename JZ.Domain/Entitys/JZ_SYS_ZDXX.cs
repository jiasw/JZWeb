using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace JZ.Domain.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("jz_sys_zdxx")]
    public partial class JZ_SYS_ZDXX
    {
           public JZ_SYS_ZDXX(){


           }
           /// <summary>
           /// Desc:雪花ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:字典分类编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDFLBM {get;set;}

           /// <summary>
           /// Desc:字典分类名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDFLMC {get;set;}

           /// <summary>
           /// Desc:字典编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDBM {get;set;}

           /// <summary>
           /// Desc:字典名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDMC {get;set;}

           /// <summary>
           /// Desc:字典顺序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ZDSX {get;set;}

           /// <summary>
           /// Desc:删除标志;0未删除1已删除
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? Del {get;set;}

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
