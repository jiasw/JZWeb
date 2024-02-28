using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.WXAPP
{
    public class WXAPPLoginDto
    {
        public string sessionKey { get; set; }

        public string openID { get; set; }
    }
}
