namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response.Vehicle;
    using CarShop.Service.Common.Extensions.Validator;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class VehicleQueries
    {
        public static Func<IQueryable<Vehicle>, IQueryable<VehicleResponseModel>> GetAllVehicleResponse
         => (IQueryable<Vehicle> vehicles) =>
         vehicles.Select(vehicle => new VehicleResponseModel()
         {
             Id = vehicle.Id,
             Owner = vehicle.Owner.Username,
             OwnerMail = vehicle.Owner.Email,
             PicturePath = vehicle.PicturePath,
             VehicleType = vehicle.VehicleType.Type,
             Brand = vehicle.Brand.Brand,
             Model = vehicle.Model,
             Year = vehicle.Year,
             PlateNumber = vehicle.PlateNumber,
             IssueCount = vehicle.Issues.Count,
             RegistrationDate = vehicle.CreatedOn.Date
         });

        public static async Task<VehicleResponseModel> VehicleByIdAsync(long vehicleId, CarShopDbContext db)
         => await db.Vehicles
            .Where(vehicle => vehicle.Id == vehicleId)
            .Select(vehicle => new VehicleResponseModel()
            {
                Id = vehicle.Id,
                Owner = vehicle.Owner.Username,
                PicturePath = vehicle.PicturePath,
                VehicleType = vehicle.VehicleType.Type,
                Brand = vehicle.Brand.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                PlateNumber = vehicle.PlateNumber,
                IssueCount = vehicle.Issues.Count,
                RegistrationDate = vehicle.CreatedOn.Date
            })
            .FirstOrDefaultAsync();

        public static IQueryable<Vehicle> Filter(VehicleFilterAndSortRequestModel requestModel, IQueryable<Vehicle> query)
        {
            if (EntityValidator.IsStringPropertyValid(requestModel.OwnerEmail))
            {
                query = query.Where(vehicle => vehicle.Owner.Email.Contains(requestModel.OwnerName));
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.OwnerName))
            {
                query = query.Where(vehicle => vehicle.Owner.Username.Contains(requestModel.OwnerName));
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.Model))
            {
                query = query.Where(vehicle => vehicle.Model.Contains(requestModel.Model));
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.PlateNumber))
            {
                query = query.Where(vehicle => vehicle.PlateNumber == requestModel.PlateNumber);
            }

            if (requestModel.BrandId.HasValue)
            {
                query = query.Where(vehicle => vehicle.BrandId == requestModel.BrandId);
            }

            if (requestModel.VehicleTypeId.HasValue)
            {
                query = query.Where(vehicle => vehicle.VehicleTypeId == requestModel.VehicleTypeId);
            }

            if (requestModel.Year.HasValue)
            {
                query = query.Where(vehicle => vehicle.Year == requestModel.Year);
            }

            if (requestModel.OwnerId.HasValue)
            {
                query = query.Where(vehicle => vehicle.OwnerId == requestModel.OwnerId);
            }

            if (requestModel.IssueCount.HasValue)
            {
                query = query.Where(vehicle => vehicle.Issues.Count == requestModel.IssueCount);
            }

            return query;
        }

        public static IQueryable<Vehicle> SortBy(VehicleSortRequestModel requestModel, IQueryable<Vehicle> query)
        {
            var dummyQuery = query.OrderByDescending(x => 1);

            if (requestModel.RecentlyRegistered)
            {
                dummyQuery = dummyQuery.ThenByDescending(vehicle => vehicle.CreatedOn);
            }
            else if (requestModel.OldestRegistered)
            {
                dummyQuery = dummyQuery.ThenBy(vehicle => vehicle.CreatedOn);
            }

            if (requestModel.ByYearDesc)
            {
                dummyQuery = dummyQuery.ThenByDescending(vehicle => vehicle.Year);
            }
            else if (requestModel.ByYearAsc)
            {
                dummyQuery = dummyQuery.ThenBy(vehicle => vehicle.Year);
            }

            if (requestModel.MostIssues)
            {
                dummyQuery = dummyQuery.ThenByDescending(vehicle => vehicle.Issues.Count);
            }
            else if (requestModel.LessIssues)
            {
                dummyQuery = dummyQuery.ThenBy(vehicle => vehicle.Issues.Count);
            }

            return dummyQuery;
        }
    }
}