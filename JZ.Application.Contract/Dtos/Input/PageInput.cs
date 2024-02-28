using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos.Input
{
    public class PageInput
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public PageInput(int index, int size)
        {
            PageIndex = index;
            PageSize = size;
        }
    }
}
