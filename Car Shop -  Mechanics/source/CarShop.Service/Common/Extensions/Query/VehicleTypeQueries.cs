namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.VehicleType;
    using CarShop.Models.Response.VehicleType;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class VehicleTypeQueries
    {
        public static Func<IQueryable<VehicleType>, IQueryable<VehicleTypeResponseModel>> GetAllVehicleTypeResponse
         => (IQueryable<VehicleType> vehicleTypes) =>
            vehicleTypes.Select(vehicleType => new VehicleTypeResponseModel()
            {
                Id = vehicleType.Id,
                TypeName = vehicleType.Type,
                RegisteredSince = vehicleType.CreatedOn.Date,
                RegisteredVehiclesAtShop = vehicleType.Vehicles.Count()
            });

        public static async Task<VehicleTypeResponseModel> VehicleTypeByIdAsync(long vehicleBrandId, CarShopDbContext db)
         => await db.VehicleTypes
            .Where(vehicleType => vehicleType.Id == vehicleBrandId)
            .Select(vehicleType => new VehicleTypeResponseModel()
            {
                Id = vehicleType.Id,
                TypeName = vehicleType.Type,
                RegisteredSince = vehicleType.CreatedOn.Date,
                RegisteredVehiclesAtShop = vehicleType.Vehicles.Count()
            })
            .FirstOrDefaultAsync();

        public static IOrderedQueryable<VehicleType> Sort(VehicleTypeSortRequestModel model, IQueryable<VehicleType> query)
        {
            var sortedQuery = query.OrderBy(vehicleType => 1);

            if (model.MostPopular)
            {
                sortedQuery = sortedQuery.ThenByDescending(vehicleType => vehicleType.Vehicles.Count);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(vehicleType => vehicleType.CreatedOn);
            }

            if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(vehicleType => vehicleType.CreatedOn);
            }

            return sortedQuery;
        }
    }
}