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

        public bool isStatusCodeSuccess()
        {
            if (StatusCode >= 200 && StatusCode < 300)
                return true;

            return false;
        }
    }
}