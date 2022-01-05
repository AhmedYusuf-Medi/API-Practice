namespace CarShop.Service.Common.Base
{
    using CarShop.Models.Response;

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
    }
}