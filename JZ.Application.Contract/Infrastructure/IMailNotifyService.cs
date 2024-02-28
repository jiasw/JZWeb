using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Infrastructure
{
    /// <summary>
    /// 邮件发送服务
    /// </summary>
    public interface IMailNotifyService
    {
        /// <summary>
        /// 下单通知接口
        /// </summary>
        /// <returns></returns>
        void SendOrderNotify(string openid);
    }
}
