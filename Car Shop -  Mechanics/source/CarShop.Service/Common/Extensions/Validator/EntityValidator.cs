namespace CarShop.Service.Common.Extensions.Validator
{
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;

    public static class EntityValidator
    {
        public static void ValidateForNull<T>(Response<T> responseModel, string succeedMessage, string entityType)
        {
            if (responseModel.Payload == null)
            {
                responseModel.Message = string.Format(ExceptionMessages.DOESNT_EXIST, entityType);
            }
            else
            {
                responseModel.Message = string.Format(succeedMessage, entityType);
                responseModel.IsSuccess = true;
            }
        }

        public static void ValidateForNull<T>(T entity, InfoResponse responseModel, string succeedMessage, string entityType)
        {
            if (entity == null)
            {
                responseModel.Message = string.Format(ExceptionMessages.DOESNT_EXIST, entityType);
            }
            else
            {
                responseModel.Message = string.Format(succeedMessage, entityType);
                responseModel.IsSuccess = true;
            }
        }

        public static bool IsStringPropertyValid(string property, string currentProperty)
        {
            return !string.IsNullOrEmpty(property)
                && !string.IsNullOrWhiteSpace(property)
                && !currentProperty.Equals(property);
        }
    }
}