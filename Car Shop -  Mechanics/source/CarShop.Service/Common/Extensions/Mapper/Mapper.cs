namespace CarShop.Service.Common.Mapper
{
    using CarShop.Models.Base;
    using CarShop.Models.Request.Exception;
    using CarShop.Models.Request.IssuePriority;
    using CarShop.Models.Request.IssueStatus;
    using CarShop.Models.Request.User;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Request.VehicleBrand;
    using CarShop.Models.Request.VehicleType;

    public static class Mapper
    {
        public static Vehicle ToVehicle(VehicleCreateRequestModel requestModel)
        {
            return new Vehicle()
            {
                Year = requestModel.Year,
                Model = requestModel.Model,
                PlateNumber = requestModel.PlateNumber,
                BrandId = requestModel.BrandId,
                VehicleTypeId = requestModel.VehicleTypeId,
                OwnerId = requestModel.OwnerId
            };
        }

        public static IssuePriority ToIssuePriority(IssuePriorityCreateRequestModel requestModel)
        {
            return new IssuePriority()
            {
                Priority = requestModel.PriorityName,
                Severity = requestModel.Severity
            };
        }

        public static IssueStatus ToIssueStatus(IssueStatusCreateRequestModel requestModel)
        {
            return new IssueStatus()
            {
                Status = requestModel.StatusName,
            };
        }

        public static VehicleBrand ToVehicleBrand(VehicleBrandCreateRequestModel requestModel)
        {
            return new VehicleBrand()
            {
                Brand = requestModel.BrandName,
            };
        }

        public static VehicleType ToVehicleType(VehicleTypeCreateRequestModel requestModel)
        {
            return new VehicleType()
            {
                Type = requestModel.TypeName,
            };
        }

        public static User ToUser(UserRegisterRequestModel requestModel)
        {
            return new User()
            {
                Username = requestModel.Username,
                Email = requestModel.Email,
                Password = requestModel.Password,
            };
        }

        public static UserSortRequestModel ToRequest(UserSearchAndSortRequestModel requestModel)
        {
            return new UserSortRequestModel
            {
                Recently = requestModel.Recently,
                Oldest = requestModel.Oldest,
                MostActive = requestModel.MostActive,
                MostVehicles = requestModel.MostVehicles,
                PerPage = requestModel.PerPage,
                Page = requestModel.Page
            };
        }

        public static VehicleSortRequestModel ToRequest(VehicleFilterRequestModel requestModel)
        {
            return new VehicleSortRequestModel
            {
                MostRecentlyRegistered = requestModel.MostRecentlyRegistered,
                OldestRegistered = requestModel.OldestRegistered,
                ByYearDesc = requestModel.ByYearDesc,
                ByYearAsc = requestModel.ByYearAsc,
                MostIssues = requestModel.MostIssues,
                LessIssues = requestModel.LessIssues,
                PerPage = requestModel.PerPage,
                Page = requestModel.Page
            };
        }

        public static ExceptionSortRequestModel ToRequest(ExceptionSortAndFilterRequestModel requestModel)
        {
            return new ExceptionSortRequestModel
            {
                MostRecently = requestModel.MostRecently,
                Oldest = requestModel.Oldest,
                PerPage = requestModel.PerPage,
                Page = requestModel.Page
            };
        }
    }
}