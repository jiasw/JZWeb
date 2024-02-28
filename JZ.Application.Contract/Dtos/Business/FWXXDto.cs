using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    public class FWXXDto
    {
        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:服务类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FWLX { get; set; }

        /// <summary>
        /// Desc:服务封面
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Pic { get; set; }


        /// <summary>
        /// Desc:服务详情banner，用分号分隔
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Banner { get; set; }


        /// <summary>
        /// 服务类型名称
        /// </summary>
        public string FWLXMC { get; set; }

        /// <summary>
        /// Desc:服务项目
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:服务简介
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:服务详情
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Context { get; set; }

        /// <summary>
        /// Desc:服务价格
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? Price { get; set; }

        /// <summary>
        /// Desc:价格单位，小时
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Unit { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitMC { get; set; }

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
        /// Desc:排列顺序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? SX { get; set; }

        /// <summary>
        /// Desc:是否为优质服务
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IsGood { get; set; }

        /// <summary>
        /// Desc:删除标志，0未删除；1已删除
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte Del { get; set; } = 0;

        /// <summary>
        /// 评论图片
        /// </summary>
        public string[] listBanner
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Banner))
                {
                    return this.Banner.Split(';');
                }
                return new string[] { };
            }
        }
    }
}
