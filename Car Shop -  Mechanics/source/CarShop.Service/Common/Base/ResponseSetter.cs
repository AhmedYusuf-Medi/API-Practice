namespace CarShop.Service.Common.Base
{
    using CarShop.Models.Response;
    using System.Linq;
    using System.Text;

    public static class ResponseSetter
    {
        public static void SetResponse<T>(Response<T> response, bool isSuccess, string message, T payload)
        {
            response.IsSuccess = isSuccess;
            response.Message = message;
            response.Payload = payload;
        }

        public static void SetResponse(InfoResponse response, bool isSuccess, string message)
        {
            response.IsSuccess = isSuccess;
            response.Message = message;
        }

        public static void ReworkMessageResult(InfoResponse response)
        {
            var sb = new StringBuilder(response.Message);
            string[] result = sb.ToString().Split("\r\n")
                               .Where(x => !string.IsNullOrEmpty(x))
                               .ToArray();

            string message = string.Join("\n", result);
            response.Message = message;
        }

    }
}