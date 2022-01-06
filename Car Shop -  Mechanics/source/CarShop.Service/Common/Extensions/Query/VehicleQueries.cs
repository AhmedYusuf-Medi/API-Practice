using CarShop.Data;
using CarShop.Models.Base;
using CarShop.Models.Response.Vehicle;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Service.Common.Extensions.Query
{
    public static class VehicleQueries
    {
        public static Func<IQueryable<Vehicle>, IQueryable<VehicleResponseModel>> GetAllVehicleResponse
         => (IQueryable<Vehicle> vehicles) =>
         vehicles.Select(vehicle => new VehicleResponseModel()
         {
             Id = vehicle.Id,
             Owner = vehicle.Owner.Username,
             PicturePath = vehicle.PicturePath,
             VehicleType = vehicle.VehicleType.Type,
             Brand = vehicle.Brand.Brand,
             Model = vehicle.Model,
             Year = vehicle.Year,
             PlateNumber = vehicle.PlateNumber,
             IssueCount = vehicle.Issues.Count
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
                IssueCount = vehicle.Issues.Count
            })
            .FirstOrDefaultAsync();
    }
}