using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace JZ.Domain.Entitys
{
    ///<summary>
    ///后台管理员管理
    ///</summary>
    [SugarTable("jz_sys_manger")]
    public partial class JZ_SYS_MANGER
    {
           public JZ_SYS_MANGER(){


           }
           /// <summary>
           /// Desc:雪花ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:用户名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:登录名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LoginName {get;set;}

           /// <summary>
           /// Desc:登录密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LoginPsw {get;set;}

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
