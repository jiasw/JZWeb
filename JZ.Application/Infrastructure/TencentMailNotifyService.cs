using JZ.Application.Contract.Dtos.Conf;
using JZ.Application.Contract.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Ses.V20201002;
using TencentCloud.Ses.V20201002.Models;

namespace JZ.Application.Infrastructure
{
    public class TencentMailNotifyService:IMailNotifyService
    {
        private readonly TencentMailConf options;
        private readonly ILogger<TencentMailNotifyService> logger;

        public TencentMailNotifyService(IOptions<TencentMailConf> options,ILogger<TencentMailNotifyService> logger)
        {
            this.options = options.Value;
            this.logger = logger;
        }

        private SesClient buildClient()
        {
            // 创建SES客户端对象
            return new SesClient(new Credential
            {
                SecretId = options.SecretId,
                SecretKey = options.SecretKey,
            }, options.Region);
        }

        private SendEmailRequest buildRequest(string Subject, string templateData)
        {
          return  new SendEmailRequest
            {
                FromEmailAddress = options.FromEmailAddress,  // 发件人邮箱
                Destination = options.SendMails,  // 收件人邮箱，可以是数组
                Subject = Subject,  // 邮件主题
                Template = new Template() { TemplateData = templateData, TemplateID = options.TemplateID },  // 邮件正文
            };
        }

        public void SendOrderNotify(string openid)
        {
            try
            {
                string subject = "【小马帮帮】下单通知,用户openid："+openid;
                string template = JsonConvert.SerializeObject(new NotifyOrderTemplateData() { name="管理员"});
                SendEmailResponse response = buildClient().SendEmailSync(buildRequest(subject, template));
                logger.LogInformation("邮件发送成功，返回：" + JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "发送通知邮件出错");
                
            }
        }
    }
}
