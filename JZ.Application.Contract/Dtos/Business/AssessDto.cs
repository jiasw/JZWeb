using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    /// <summary>
    /// 用户评价
    /// </summary>
    public class AssessDto
    {
        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UserID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? OrderID { get; set; }

        /// <summary>
        /// Desc:评价文本
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Text { get; set; }

        /// <summary>
        /// Desc:评价图片
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Pics { get; set; }


        /// <summary>
        /// Desc:评价等级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int Level { get; set; } = 0;

        /// <summary>
        /// Desc:是否是精选评价
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IsGood { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AddTime { get; set; }

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
        /// Desc:删除时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DelTime { get; set; }

        /// <summary>
        /// 评论图片
        /// </summary>
        public string[] listPic 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Pics))
                {
                    return this.Pics.Split(',');
                }
                return  new string[] {};
            }
        }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserImg { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Desc:评价关键词，用逗号隔开
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string KeyWord { get; set; }
    }
}
