using System.Text.Json.Serialization;

namespace TestZadanie4.Models
{
    public interface IBaseResponse<T>
    {
        ResultCode Status { get; set; }
        string ErrorMessage { get; set; }

    }


    public enum ResultCode
    {
        OK = 200,
        NOT_FOUND = 404,
        NO_CONTENT = 204,
        NOT_SUPPORTED = 501,
        Error = 500,
        Failure = 503
    }

    public class BaseResponse<T> : IBaseResponse<T>
    {


        [JsonIgnore]
        public T Data { get; set; } 
        public ResultCode Status { get; set; }
        public string ErrorMessage { get; set; }

        public BaseResponse()
        {
            Status = ResultCode.OK;
        }

    }
}
