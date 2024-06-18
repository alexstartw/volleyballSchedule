using System.Net;

namespace VolleyBallSchedule.Models
{
    public class ApiResult
    {
        public ApiResult()
        {
            Code = 200;
        }

        public int Code { get; set; }
        public string Msg { get; set; }
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }

        public void SetCode(HttpStatusCode httpStatusCode)
        {
            Code = (int)httpStatusCode;
            Msg = httpStatusCode.ToString();
        }

        public HttpStatusCode GetCode()
        {
            return (HttpStatusCode)Code;
        }
    }

    public class ApiResult<TModel> : ApiResult
    {
        public ApiResult()
        {
        }

        public ApiResult(TModel data)
        {
            Data = data;
        }

        public TModel Data { get; set; }
    }

    public class ApiFailedResult : ApiResult
    {
        public ApiFailedResult()
        {
            Code = 400;
            Msg = "Bad Request";
        }

        public ApiFailedResult(
            int code,
            string msg,
            Dictionary<string, IEnumerable<string>> errors = null)
        {
            Code = code;
            Msg = msg;
            Errors = errors;
        }

        public object Data { get; set; }
    }
}