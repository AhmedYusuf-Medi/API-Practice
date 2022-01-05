namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.VehicleBrand;
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
            vehicleTypes.Select(vb => new VehicleTypeResponseModel()
            {
                Id = vb.Id,
                TypeName = vb.Type,
                RegisteredSince = vb.CreatedOn.Date,
                RegisteredVehiclesAtShop = vb.Vehicles.Count()
            });

        public static async Task<VehicleTypeResponseModel> VehicleTypeByIdAsync(long vehicleBrandId, CarShopDbContext db)
        {
            return await db.VehicleTypes
                 .Where(vb => vb.Id == vehicleBrandId)
                 .Select(vb => new VehicleTypeResponseModel()
                 {
                     Id = vb.Id,
                     TypeName = vb.Type,
                     RegisteredSince = vb.CreatedOn.Date,
                     RegisteredVehiclesAtShop = vb.Vehicles.Count()
                 })
                 .FirstOrDefaultAsync();
        }

        public static IOrderedQueryable<VehicleType> Sort(VehicleTypeSortRequestModel model, IQueryable<VehicleType> query)
        {
            var sortedQuery = query.OrderBy(u => 1);

            if (model.MostPopular)
            {
                sortedQuery = sortedQuery.ThenByDescending(u => u.Vehicles.Count);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(u => u.CreatedOn);
            }

            if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(u => u.CreatedOn);
            }

            return sortedQuery;
        }
    }
}