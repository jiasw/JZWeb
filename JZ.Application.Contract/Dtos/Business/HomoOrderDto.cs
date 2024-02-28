using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Business
{
    /// <summary>
    /// 首页显示的订单汇总详情
    /// </summary>
    public class HomoOrderDto
    {
        /// <summary>
        /// 总订单
        /// </summary>
        public int Total { get; set; } = 0;

        /// <summary>
        /// 新订单数量
        /// </summary>
        public int NewOrder { get; set; } = 0;

        /// <summary>
        /// 总收入
        /// </summary>
        public decimal Amount { get; set; } = 0;

    }
}
