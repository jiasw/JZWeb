using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.WXAPP
{

    public class WaterMark
    {
        public long timestamp { get; set; }

        public string appid { get; set; }

    }
    public class PhoneInfo
    {
        public string phoneNumber { get; set; }

        public string purePhoneNumber { get; set; }

        public int countryCode { get; set; }

        public WaterMark watermark { get; set; }

    }

    public class WXGetPhoneDto: WXResponse
    {

        public PhoneInfo phone_info { get; set; }


    }
}
