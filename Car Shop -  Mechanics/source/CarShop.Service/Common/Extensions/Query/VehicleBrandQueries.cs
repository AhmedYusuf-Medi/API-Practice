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
            vehicleBrands.Select(vb => new VehicleBrandResponseModel()
            {
                Id = vb.Id,
                BrandName = vb.Brand,
                RegisteredSince = vb.CreatedOn.Date,
                RegisteredVehiclesAtShop = vb.Vehicles.Count()
            });

        public static async Task<VehicleBrandResponseModel> VehicleBrandByIdAsync(long vehicleBrandId, CarShopDbContext db)
        {
            return await db.VehicleBrands
                 .Where(vb => vb.Id == vehicleBrandId)
                 .Select(vb => new VehicleBrandResponseModel()
                 {
                     Id = vb.Id,
                     BrandName = vb.Brand,
                     RegisteredSince = vb.CreatedOn.Date,
                     RegisteredVehiclesAtShop = vb.Vehicles.Count()
                 })
                 .FirstOrDefaultAsync();
        }

        public static IOrderedQueryable<VehicleBrand> Sort(VehicleBrandSortRequestModel model, IQueryable<VehicleBrand> query)
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