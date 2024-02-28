using JZ.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JZ.Application.Infrastructure
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        public static string GetNewPassWord(string passWord)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(passWord, HardCode.MangerSalt, 10000))
            {
                return Convert.ToBase64String( pbkdf2.GetBytes(20)); // 生成 20 字节的哈希值
            }
        }

        public static string GetDateStr()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyyMMddHHmmss");
        }

        public static string GetDateMillsecondStr()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyyMMddHHmmssfff");
        }

        public static string FormatDateTime(string strData,string fmt= "yyyy-MM-dd HH:mm:ss")
        {
            if (string.IsNullOrWhiteSpace(strData))
            {
                return "";
            }
            if (fmt.Contains("yyyy"))
            {
                fmt=fmt.Replace("yyyy", strData.Substring(0, 4));
            }
            if (fmt.Contains("MM"))
            {
                fmt=fmt.Replace("MM", strData.Substring(4, 2));
            }
            if (fmt.Contains("dd"))
            {
                fmt=fmt.Replace("dd", strData.Substring(6, 2));
            }
            if (fmt.Contains("HH"))
            {
                fmt=fmt.Replace("HH", strData.Substring(8, 2));
            }
            if (fmt.Contains("mm"))
            {
                fmt=fmt.Replace("mm", strData.Substring(10, 2));
            }
            if (fmt.Contains("ss"))
            {
                fmt=fmt.Replace("ss", strData.Substring(12, 2));
            }
            return fmt;
        }

        public static string FormatDate(string strDate)
        {
            return FormatDateTime(strDate, "yyyy-MM-dd");
        }


        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <returns></returns>
        public static long GetSnowID()
        {
            var newId = YitIdHelper.NextId();
            return newId;
        }

        

        /// <summary>
        /// 生成四位的随机数
        /// </summary>
        /// <returns></returns>
        public static string GetOrderCode()
        {
            Random random = new Random();
            return random.Next(1000, 10000).ToString();
        }
        

    }
}
