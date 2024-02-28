using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Dtos
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ApiResponse(int code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public static ApiResponse Success(string message)
        {
            return new ApiResponse(200, message, "");
        }
        public static ApiResponse Success(object data)
        {
            return new ApiResponse(200, "操作成功", data);
        }
        public static ApiResponse Success(object data,string message)
        {
            return new ApiResponse(200, message, data);
        }


        public static ApiResponse Fail(string message)
        {
            return new ApiResponse(500, message, "");
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }



}
