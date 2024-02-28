using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Conf
{
    /// <summary>
    /// 
    /// </summary>
    public class TencentMailConf
    {
        public string Region { get; set; }

        public string SecretId { get; set; }

        public string SecretKey { get; set; }
        /// <summary>
        /// 发件箱
        /// </summary>
        public string FromEmailAddress { get; set; }
        /// <summary>
        /// 收件箱，多个用逗号分隔
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 模板id
        /// </summary>
        public ulong TemplateID { get; set; }
        /// <summary>
        /// 收件箱数组
        /// </summary>
        public string[] SendMails
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Destination))
                {
                    return this.Destination.Split(',');
                }
                return new string[] { };
            }
        }


    }

    public class NotifyOrderTemplateData
    {
        public string name { get; set; }
    }


}
