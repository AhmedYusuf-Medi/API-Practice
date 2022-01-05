﻿namespace CarShop.Service.Data.VehicleBrand
{
    //Local
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.VehicleBrand;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleBrand;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using System.Linq;
    using System.Threading.Tasks;

    public class VehicleBrandService : BaseService, IVehicleBrandService
    {
        public VehicleBrandService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<VehicleBrandResponseModel>> GetByIdAsync(long id)
        {
            var vehicleBrand = await VehicleBrandQueries.VehicleBrandByIdAsync(id, this.db);

            var response = new Response<VehicleBrandResponseModel>();
            response.Payload = vehicleBrand;

            EntityValidator.ValidateForNull(vehicleBrand, response, ResponseMessages.Entity_Get_Succeed, Constants.VehicleBrand);

            return response;
        }

        public async Task<Response<Paginate<VehicleBrandResponseModel>>> GetAllAsync(PaginationRequestModel requestModel)
        {
            var result = VehicleBrandQueries.GetAllVehicleBrandResponse(this.db.VehicleBrands);

            var payload = await Paginate<VehicleBrandResponseModel>.ToPaginatedCollection(result, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<VehicleBrandResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.VehicleBrands), payload);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(VehicleBrandCreateRequestModel requestModel)
        {
            var brand = Mapper.ToVehicleBrand(requestModel);
            await this.db.VehicleBrands.AddAsync(brand);
            await this.db.SaveChangesAsync();

            var response = new InfoResponse();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.VehicleBrand));

            return response;
        }

        public async Task<InfoResponse> UpdateAsync(long id, VehicleBrandCreateRequestModel requestModel)
        {
            var response = new InfoResponse();

            var brand = await this.db.VehicleBrands.Where(vb => vb.Id == id)
                .FirstOrDefaultAsync();

            brand.Brand = requestModel.BrandName;
            await this.db.SaveChangesAsync();

            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.VehicleBrand));

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();

            var brand = await this.db.VehicleBrands.Where(vb => vb.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(brand, response, ResponseMessages.Entity_Delete_Succeed, Constants.VehicleBrand);

            if (response.IsSuccess)
            {
                this.db.VehicleBrands.Remove(brand);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<VehicleBrandResponseModel>>> SortByAsync(VehicleBrandSortRequestModel requestModel)
        {
            var vehicleBrands = VehicleBrandQueries.Sort(requestModel, this.db.VehicleBrands).AsQueryable();

            var userResponses = VehicleBrandQueries.GetAllVehicleBrandResponse(vehicleBrands);

            var payload = await Paginate<VehicleBrandResponseModel>.ToPaginatedCollection(userResponses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<VehicleBrandResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.VehicleBrands), payload);

            return response;
        }
    }
}