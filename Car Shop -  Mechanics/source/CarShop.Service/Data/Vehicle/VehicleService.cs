namespace CarShop.Service.Data.Vehicle
{
    //Public
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Vehicle;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Exceptions;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Reflection;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Common.Providers.Cloudinary;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Local
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VehicleService : BaseService, IVehicleService
    {
        private readonly ICloudinaryService cloudinaryService;

        public VehicleService(CarShopDbContext db, ICloudinaryService cloudinaryService)
            : base(db)
        {
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Response<Paginate<VehicleResponseModel>>> GetAllAsync(PaginationRequestModel request)
        {
            var vehicles = VehicleQueries.GetAllVehicleResponse(this.db.Vehicles);

            var payload = await Paginate<VehicleResponseModel>.ToPaginatedCollection(vehicles, request.Page, request.PerPage);

            var response = new Response<Paginate<VehicleResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Vehicles));

            return response;
        }

        public async Task<Response<VehicleResponseModel>> GetByIdAsync(long id)
        {
            var response = new Response<VehicleResponseModel>();

            var vehicle = await VehicleQueries.VehicleByIdAsync(id, this.db);

            response.Payload = vehicle;

            EntityValidator.ValidateForNull(response, ResponseMessages.Entity_Get_Succeed, Constants.Vehicle);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(VehicleCreateRequestModel requestModel)
        {
            var response = new InfoResponse();
            response.IsSuccess = true;

            EntityValidator.CheckUser(response, requestModel.OwnerId, this.db, Constants.User);
            EntityValidator.CheckVehicleBrand(response, requestModel.BrandId, this.db,  Constants.VehicleBrand);
            EntityValidator.CheckVehicleType(response, requestModel.VehicleTypeId, this.db, Constants.VehicleType);

            if (response.IsSuccess)
            {
                var vehicle = Mapper.ToVehicle(requestModel);

                if (requestModel.VehiclePhoto != null)
                {
                    var user = await this.db.Users
                        .Where(u => u.Id == requestModel.OwnerId)
                        .Select(u => new Models.Base.User
                        {
                            Username = u.Username
                        })
                        .FirstOrDefaultAsync();

                    var fileName = requestModel.VehiclePhoto.FileName;
                    var uploadResults = await this.cloudinaryService.UploadPictureAsync(requestModel.VehiclePhoto, fileName, user.Username);

                    vehicle.PicturePath = uploadResults[0];
                    vehicle.PictureId = uploadResults[1];
                }

                await this.db.Vehicles.AddAsync(vehicle);
                await this.db.SaveChangesAsync();

                ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Vehicle));
            }

            return response;
        }

        public async Task<InfoResponse> UpdateAsync(long id, VehicleUpdateRequestModel requestModel)
        {
            var response = new InfoResponse();
            response.IsSuccess = true;

            var vehicle = await this.db.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);

            EntityValidator.ValidateForNull(vehicle, response, Constants.Vehicle);

            if (response.IsSuccess)
            {
                var isChangesDone = false;

                if (requestModel.VehicleTypeId.HasValue && requestModel.VehicleTypeId != vehicle.VehicleTypeId)
                {
                    EntityValidator.CheckVehicleType(response, (long)requestModel.VehicleTypeId, this.db, Constants.VehicleType);

                    if (response.IsSuccess)
                    {
                        vehicle.VehicleTypeId = (long)requestModel.VehicleTypeId;
                        isChangesDone = true;
                    }
                }

                if (requestModel.BrandId.HasValue && requestModel.BrandId != vehicle.BrandId)
                {
                    EntityValidator.CheckVehicleBrand(response, (long)requestModel.BrandId, this.db, Constants.VehicleBrand);

                    if (response.IsSuccess)
                    {
                        vehicle.BrandId = (long)requestModel.BrandId;
                        isChangesDone = true;
                    }
                }

                if (requestModel.OwnerId.HasValue && requestModel.OwnerId != vehicle.OwnerId)
                {
                    EntityValidator.CheckUser(response, (long)requestModel.OwnerId, this.db, Constants.User);

                    if (response.IsSuccess)
                    {
                        vehicle.OwnerId = (long)requestModel.OwnerId;
                        isChangesDone = true;
                    }
                }

                if (requestModel.Year.HasValue && requestModel.Year != vehicle.Year)
                {
                    vehicle.Year = (byte)requestModel.Year;
                    isChangesDone = true;
                }

                if (EntityValidator.IsStringPropertyValid(requestModel.Model, vehicle.Model))
                {
                    vehicle.Model = requestModel.Model;
                    isChangesDone = true;
                }

                if (EntityValidator.IsStringPropertyValid(requestModel.PlateNumber, vehicle.PlateNumber))
                {
                    if (!await this.db.Vehicles.AnyAsync(vehicle => vehicle.PlateNumber == requestModel.PlateNumber))
                    {
                        vehicle.PlateNumber = requestModel.PlateNumber;
                        isChangesDone = true;
                    }
                    else
                    {
                        var sb = new StringBuilder(response.Message);
                        sb.AppendLine(string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.PlateNumber), requestModel.PlateNumber));
                        response.Message = sb.ToString();
                    }
                }

                var user = await this.db.Users
                    .Where(u => u.Id == requestModel.OwnerId)
                    .Select(u => new Models.Base.User
                    {
                        Username = u.Username
                    })
                    .FirstOrDefaultAsync();

                if (requestModel.VehiclePhoto != null && user != null)
                {
                    if (EntityValidator.IsStringPropertyValid(vehicle.PictureId))
                    {
                        await this.cloudinaryService.DeleteImageAsync(vehicle.PictureId);
                    }

                    var fileName = requestModel.VehiclePhoto.FileName;
                    var uploadResults = await this.cloudinaryService.UploadPictureAsync(requestModel.VehiclePhoto, fileName, user.Username);

                    vehicle.PicturePath = uploadResults[0];
                    vehicle.PictureId = uploadResults[1];
                }

                if (isChangesDone)
                {
                    response.IsSuccess = true;
                    var sb = new StringBuilder(response.Message);
                    sb.AppendLine(string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Vehicle));
                    response.Message = sb.ToString();

                    await this.db.SaveChangesAsync();
                }
                else
                {
                    throw new BadRequestException(ExceptionMessages.Arguments_Are_Invalid);
                }
            }

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var vehicle = await this.db.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);

            var response = new InfoResponse();

            EntityValidator.ValidateForNull(vehicle, response, ResponseMessages.Entity_Delete_Succeed, Constants.Vehicle);

            if (response.IsSuccess)
            {
                this.db.Vehicles.Remove(vehicle);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<VehicleResponseModel>>> FilterByAsync(VehicleFilterRequestModel requestModel)
        {
            var IsSortingNeeded = ClassScanner.IsThereAnyTrueProperty(requestModel);

            IQueryable<Models.Base.Vehicle> query = this.db.Vehicles.AsQueryable();

            query = VehicleQueries.Filter(requestModel, query);

            var response = new Response<Paginate<VehicleResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Vehicles));

            if (IsSortingNeeded)
            {
                var sortByResponse = await this.SortByAsync(Mapper.ToRequest(requestModel), query);

                var sb = new StringBuilder();
                sb.AppendLine(response.Message);
                sb.AppendLine(sortByResponse.Message);

                response.Message = sb.ToString();

                response.Payload = sortByResponse.Payload;
            }
            else
            {
                var filtered = VehicleQueries.GetAllVehicleResponse(query);

                var payload = await Paginate<VehicleResponseModel>.ToPaginatedCollection(filtered, requestModel.Page, requestModel.PerPage);

                response.Payload = payload;
            }

            return response;
        }

        public async Task<Response<Paginate<VehicleResponseModel>>> SortByAsync(VehicleSortRequestModel requestModel, IQueryable<Models.Base.Vehicle> vehicles = null)
        {
            IQueryable<Models.Base.Vehicle> query;

            if (vehicles != null)
            {
                query = vehicles;
            }
            else
            {
                query = this.db.Vehicles.AsQueryable();
            }

            query = VehicleQueries.SortBy(requestModel, query);

            var responses = VehicleQueries.GetAllVehicleResponse(query);

            var payload = await Paginate<VehicleResponseModel>.ToPaginatedCollection(responses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<VehicleResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Vehicles));

            return response;
        }
    }
}