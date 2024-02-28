using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    public class DTGLDto
    {

        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:封面图片
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Pic { get; set; }

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
        /// Desc:添加时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AddTime { get; set; }

        /// <summary>
        /// Desc:添加人员
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AddUser { get; set; }
    }
}
