namespace JZ.WebAPI.Models
{
    public class CreateOrderByJsapiRequest
    {
        
        /// <summary>
        /// 微信openID
        /// </summary>
        public string OpenId { get; set; } = "";

        /// <summary>
        /// 用户选择的服务id
        /// </summary>
        public long FWID { get; set; } = 0;

        /// <summary>
        /// 用户选择的服务数量
        /// </summary>
        public int FWSL { get; set; } = 1;

        /// <summary>
        /// 联系人
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string LXDZ { get; set; }

        /// <summary>
        /// 上门时间
        /// </summary>
        public string SMSJ { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        public string BZ { get; set; }

    }
}
