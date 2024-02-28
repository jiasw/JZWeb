using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    /// <summary>
    /// 用户地址
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Desc:雪花ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:用户id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UserID { get; set; }

        /// <summary>
        /// Desc:用户地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Address { get; set; }

        /// <summary>
        /// Desc:用户电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Phone { get; set; }

        /// <summary>
        /// Desc:默认标志
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DefaultFlag { get; set; }

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
        /// Desc:删除人员
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DelUser { get; set; }

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
