using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    public class UserInfoDto
    {

        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:用户的微信昵称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NickName { get; set; }

        /// <summary>
        /// Desc:用户预留的真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Sex { get; set; }

        /// <summary>
        /// Desc:手机号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Phone { get; set; }

        /// <summary>
        /// Desc:头像地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Photo { get; set; }

        /// <summary>
        /// Desc:微信用户id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WXID { get; set; }

        /// <summary>
        /// Desc:用户来源
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YHLY { get; set; }

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
    }
}
