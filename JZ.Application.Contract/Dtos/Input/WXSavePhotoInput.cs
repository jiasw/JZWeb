using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Input
{
    public class WXSavePhotoInput
    {
        public string PhotoUrl { get; set; }

        public string openID { get; set; }
    }
}
