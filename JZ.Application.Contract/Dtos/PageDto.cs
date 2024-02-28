using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos
{
    /// <summary>
    /// 分页集合
    /// </summary>
    public class PageDto<T>
    {
        public int TotalCount { get; set; }

        public List<T> Data { get; set; }

        public PageDto(int totalCount, List<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}
