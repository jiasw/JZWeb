using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.WXAPP
{
    /// <summary>
    /// 微信jsapi 下单成功返回模型
    /// </summary>
    public class WXJSAPIPAYDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderID { get; set; }
    }
}
