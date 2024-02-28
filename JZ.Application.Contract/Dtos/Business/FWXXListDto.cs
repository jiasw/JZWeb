using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    /// <summary>
    /// 小程序获取服务列表信息
    /// </summary>
    public class FWXXListDto
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
        /// Desc:服务价格
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? Price { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitMC { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Desc:排列顺序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? SX { get; set; }


    }
}
