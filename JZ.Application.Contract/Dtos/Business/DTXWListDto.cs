using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    public class DTXWListDto
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
        /// Desc:封面图片
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Pic { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AddTime { get; set; }
    }
}
