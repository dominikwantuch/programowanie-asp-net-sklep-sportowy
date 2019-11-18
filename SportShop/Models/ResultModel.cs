using Microsoft.AspNetCore.Http;

namespace SportShop.Models
{
    public class ResultModel<T>
    {
        public ResultModel(T data, int statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }
        public T Data { get; set; }
        
        public int StatusCode { get; set; }
    }
}