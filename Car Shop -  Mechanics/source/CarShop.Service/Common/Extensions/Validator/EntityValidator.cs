namespace CarShop.Service.Common.Extensions.Validator
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Messages;
    using System.Linq;
    using System.Text;

    public static class EntityValidator
    {
        private static StringBuilder sb = new StringBuilder();

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

        public static void ValidateForNull<T>(T entity, InfoResponse responseModel, string entityType)
        {
            if (entity == null)
            {
                responseModel.Message = string.Format(ExceptionMessages.DOESNT_EXIST, entityType);
                responseModel.IsSuccess = false;
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

        public static bool IsStringPropertyValid(string property)
        {
            return !string.IsNullOrEmpty(property)
                && !string.IsNullOrWhiteSpace(property);
        }

        public static bool IsStringPropertyValid(string property, string currentProperty)
        {
            return !string.IsNullOrEmpty(property)
                && !string.IsNullOrWhiteSpace(property)
                && !currentProperty.Equals(property);
        }

        public static void CheckUser(InfoResponse response, long userId, CarShopDbContext db, string entityType)
        {
            if (!db.Users.Any(user => user.Id == userId))
            {
                sb = new StringBuilder(response.Message);
                sb.AppendLine(string.Format(ExceptionMessages.DOESNT_EXIST, entityType));

                response.Message = sb.ToString();
                response.IsSuccess = false;
            }
        }

        public static void CheckVehicle(InfoResponse response, long vehicleId, CarShopDbContext db, string entityType)
        {
            if (!db.Vehicles.Any(vehicle => vehicle.Id == vehicleId))
            {
                sb = new StringBuilder(response.Message);
                sb.AppendLine(string.Format(ExceptionMessages.DOESNT_EXIST, entityType));

                response.Message = sb.ToString();
                response.IsSuccess = false;
            }
        }

        public static void CheckIssueStatus(InfoResponse response, long statusId, CarShopDbContext db, string entityType)
        {

            if (!db.IssueStatuses.Any(status => status.Id == statusId))
            {
                sb = new StringBuilder(response.Message);
                sb.AppendLine(string.Format(ExceptionMessages.DOESNT_EXIST, entityType));

                response.Message = sb.ToString();
                response.IsSuccess = false;
            }
        }

        public static void CheckIssuePriority(InfoResponse response, long priorityId, CarShopDbContext db, string entityType)

        {
            if (!db.IssuePriorities.Any(priority => priority.Id == priorityId))
            {
                sb = new StringBuilder(response.Message);
                sb.AppendLine(string.Format(ExceptionMessages.DOESNT_EXIST, entityType));

                response.Message = sb.ToString();
                response.IsSuccess = false;
            }
        }

        public static void CheckVehicleBrand(InfoResponse response, long vehicleBrandId, CarShopDbContext db, string entityType)
        {
            if (!db.VehicleBrands.Any(vehicleBrand => vehicleBrand.Id == vehicleBrandId))
            {
                sb = new StringBuilder(response.Message);
                sb.AppendLine(string.Format(ExceptionMessages.DOESNT_EXIST, entityType));

                response.Message = sb.ToString();
                response.IsSuccess = false;
            }
        }

        public static void CheckVehicleType(InfoResponse response, long vehicleTypeId, CarShopDbContext db, string entityType)
        {
            if (!db.VehicleTypes.Any(vehicleType => vehicleType.Id == vehicleTypeId))
            {
                sb = new StringBuilder(response.Message);
                sb.AppendLine(string.Format(ExceptionMessages.DOESNT_EXIST, entityType));

                response.Message = sb.ToString();
                response.IsSuccess = false;
            }
        }
    }
}