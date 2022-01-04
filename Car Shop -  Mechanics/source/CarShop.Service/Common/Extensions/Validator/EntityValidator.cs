namespace CarShop.Service.Common.Extensions.Validator
{
    using CarShop.Models.Response;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Messages;

    public static class EntityValidator
    {
        public static void ValidateForNull<T>(Response<T> responseModel, string succeedMessage, string entityType)
        {
            if (responseModel.Payload == null)
            {
                ResponseSetter.SetResponse(responseModel, false, string.Format(ExceptionMessages.DOESNT_EXIST, entityType));
            }
            else
            {
                ResponseSetter.SetResponse(responseModel, true, string.Format(succeedMessage, entityType));
            }
        }

        public static void ValidateForNull<T>(T entity, InfoResponse responseModel, string succeedMessage, string entityType)
        {
            if (entity == null)
            {
                ResponseSetter.SetResponse(responseModel, true, string.Format(ExceptionMessages.DOESNT_EXIST, entityType));
            }
            else
            {
                ResponseSetter.SetResponse(responseModel, true, string.Format(succeedMessage, entityType));
            }
        }

        public static bool IsStringPropertyValid(string property, string currentProperty)
        {
            return !string.IsNullOrEmpty(property)
                && !string.IsNullOrWhiteSpace(property)
                && !currentProperty.Equals(property);
        }

        public static bool IsStringPropertyValid(string property)
        {
            return !string.IsNullOrEmpty(property)
                && !string.IsNullOrWhiteSpace(property);
        }
    }
}