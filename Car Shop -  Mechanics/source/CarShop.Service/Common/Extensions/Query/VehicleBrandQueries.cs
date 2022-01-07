namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.VehicleBrand;
    using CarShop.Models.Response.VehicleBrand;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class VehicleBrandQueries
    {
        public static Func<IQueryable<VehicleBrand>, IQueryable<VehicleBrandResponseModel>> GetAllVehicleBrandResponse
          => (IQueryable<VehicleBrand> vehicleBrands) =>
            vehicleBrands.Select(vehicleBrand => new VehicleBrandResponseModel()
            {
                Id = vehicleBrand.Id,
                BrandName = vehicleBrand.Brand,
                RegisteredSince = vehicleBrand.CreatedOn.Date,
                RegisteredVehiclesAtShop = vehicleBrand.Vehicles.Count()
            });

        public static async Task<VehicleBrandResponseModel> VehicleBrandByIdAsync(long vehicleBrandId, CarShopDbContext db)
        => await db.VehicleBrands
            .Where(vehicleBrand => vehicleBrand.Id == vehicleBrandId)
            .Select(vehicleBrand => new VehicleBrandResponseModel()
            {
                Id = vehicleBrand.Id,
                BrandName = vehicleBrand.Brand,
                RegisteredSince = vehicleBrand.CreatedOn.Date,
                RegisteredVehiclesAtShop = vehicleBrand.Vehicles.Count()
            })
            .FirstOrDefaultAsync();

        public static IOrderedQueryable<VehicleBrand> Sort(VehicleBrandSortRequestModel model, IQueryable<VehicleBrand> query)
        {
            var sortedQuery = query.OrderBy(u => 1);

            if (model.MostPopular)
            {
                sortedQuery = sortedQuery.ThenByDescending(vehicleBrand => vehicleBrand.Vehicles.Count);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(vehicleBrand => vehicleBrand.CreatedOn);
            }
            else if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(vehicleBrand => vehicleBrand.CreatedOn);
            }

            return sortedQuery;
        }
    }
}