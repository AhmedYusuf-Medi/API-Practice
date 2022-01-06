namespace CarShop.Service.Data.VehicleType
{
    //Local
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.VehicleType;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleType;
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

    public class VehicleTypeService : BaseService, IVehicleTypeService
    {
        public VehicleTypeService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<VehicleTypeResponseModel>> GetByIdAsync(long id)
        {
            var vehicleType = await VehicleTypeQueries.VehicleTypeByIdAsync(id, this.db);

            var response = new Response<VehicleTypeResponseModel>();
            response.Payload = vehicleType;

            EntityValidator.ValidateForNull(vehicleType, response, ResponseMessages.Entity_Get_Succeed, Constants.VehicleType);

            return response;
        }

        public async Task<Response<Paginate<VehicleTypeResponseModel>>> GetAllAsync(PaginationRequestModel requestModel)
        {
            var result = VehicleTypeQueries.GetAllVehicleTypeResponse(this.db.VehicleTypes);

            var payload = await Paginate<VehicleTypeResponseModel>.ToPaginatedCollection(result, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<VehicleTypeResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.VehicleTypes), payload);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(VehicleTypeCreateRequestModel requestModel)
        {
            var vehicleType = Mapper.ToVehicleType(requestModel);
            await this.db.VehicleTypes.AddAsync(vehicleType);
            await this.db.SaveChangesAsync();

            var response = new InfoResponse();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.VehicleType));

            return response;
        }

        public async Task<InfoResponse> UpdateAsync(long id, VehicleTypeCreateRequestModel requestModel)
        {
            var response = new InfoResponse();

            var vehicleType = await this.db.VehicleTypes
                .Where(vb => vb.Id == id)
                .FirstOrDefaultAsync();

            vehicleType.Type = requestModel.TypeName;
            await this.db.SaveChangesAsync();

            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.VehicleType));

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();

            var vehicleType = await this.db.VehicleTypes.Where(vb => vb.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(vehicleType, response, ResponseMessages.Entity_Delete_Succeed, Constants.VehicleType);

            if (response.IsSuccess)
            {
                this.db.VehicleTypes.Remove(vehicleType);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<VehicleTypeResponseModel>>> SortByAsync(VehicleTypeSortRequestModel requestModel)
        {
            var vehicleTypes = VehicleTypeQueries.Sort(requestModel, this.db.VehicleTypes).AsQueryable();

            var responses = VehicleTypeQueries.GetAllVehicleTypeResponse(vehicleTypes);

            var payload = await Paginate<VehicleTypeResponseModel>.ToPaginatedCollection(responses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<VehicleTypeResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.VehicleTypes), payload);

            return response;
        }
    }
}