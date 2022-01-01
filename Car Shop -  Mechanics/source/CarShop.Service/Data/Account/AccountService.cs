namespace CarShop.Service.Account.Data
{
    //Local
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Models.Response.User;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Common.Providers.Cloudinary;
    using CarShop.Service.Common.Providers.SendGrid;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountService : BaseService, IAccountService
    {
        private readonly IMailSender mailSender;
        private readonly ICloudinaryService cloudinaryService;

        public AccountService(CarShopDbContext db, IMailSender mailSender, ICloudinaryService cloudinaryService)
               : base(db)
        {
            this.mailSender = mailSender;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Response<UserLoginResponseModel>> LoginAsync(UserLoginRequestModel userLogin)
        {
            var responce = new Response<UserLoginResponseModel>();

            var user = await this.db.Users
                .Where(u => u.Email == userLogin.Email && u.Password == userLogin.Password)
                .Select(u => new UserLoginResponseModel()
                {
                    Id = u.Id,
                    Username = u.Username,
                    Roles = u.Roles.Select(r => r.Role.Type),
                    Avatar = u.PicturePath
                })
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(user, responce, ResponseMessages.Login_Suceed, Constants.User);

            responce.Payload = user;

            return responce;
        }

        public async Task<InfoResponse> EditProfileAsync(long id, UserEditRequestModel user)
        {
            var response = new InfoResponse();

            var forUpdate = await this.db.Users.FirstOrDefaultAsync(u => u.Id == id);

            EntityValidator.ValidateForNull(forUpdate, response, ResponseMessages.Entity_Edit_Succeed, Constants.User);

            if (response.IsSuccess)
            {
                if (user.ProfilePicture != null)
                {
                    if (!string.IsNullOrEmpty(forUpdate.PictureId))
                    {
                        await this.cloudinaryService.DeleteImageAsync(forUpdate.PictureId);
                    }

                    await UploadProfilePicture(user, forUpdate);
                }

                if (EntityValidator.IsStringPropertyValid(user.Username, forUpdate.Username))
                {
                    forUpdate.Username = user.Username;
                }
                if (EntityValidator.IsStringPropertyValid(user.Password, forUpdate.Password))
                {
                    forUpdate.Password = user.Password;
                }
                if (EntityValidator.IsStringPropertyValid(user.Email, forUpdate.Email))
                {
                    forUpdate.Email = user.Email;
                }

                await this.db.SaveChangesAsync();

            }

            return response;
        }

        private async Task UploadProfilePicture(UserEditRequestModel user, User forUpdate)
        {
            var fileName = user.ProfilePicture.FileName;
            var uploadResults = await this.cloudinaryService.UploadPictureAsync(user.ProfilePicture, fileName, forUpdate.Username);

            forUpdate.PicturePath = uploadResults[0];
            forUpdate.PictureId = uploadResults[1];
        }

        public async Task<InfoResponse> RegisterUserAsync(UserRegisterRequestModel user)
        {
            var response = new InfoResponse();

            var doesUserExist = await this.db.Users
                .Where(u => u.Email == user.Email || u.Username == user.Username)
                .Select(u => new User
                {
                    Roles = u.Roles
                })
                .FirstOrDefaultAsync();


            var code = new Guid();
            code = Guid.NewGuid();

            if (doesUserExist != null)
            {
                response.Message = string.Format(ExceptionMessages.Already_Exist, Constants.User);
                return response;
            }
            else if (doesUserExist != null && doesUserExist.Roles.Any(r => r.Role.Id == Constants.Pending_Id))
            {
                response.IsSuccess = true;
                response.Message = ResponseMessages.Check_Email_For_Verification;
                await SendVerification(user, response, code);
            }

            if (!response.IsSuccess)
            {
                var newUser = Mapper.ToUser(user);
                newUser.PicturePath = Constants.Default_Avatar;
                newUser.Code = code;
                await this.db.Users.AddAsync(newUser);
                await this.db.SaveChangesAsync();

                var userRole = new UserRole()
                {
                    RoleId = Constants.Pending_Id,
                    UserId = newUser.Id
                };

                await this.db.UserRoles.AddAsync(userRole);
                await this.db.SaveChangesAsync();

                await SendVerification(user, response, code);
            }

            return response;
        }

        public async Task<InfoResponse> VerificationAsync(string email, Guid code)
        {
            var user = await this.db.Users
                            .Where(u => u.Email == email && u.Code == code)
                            .FirstOrDefaultAsync();

            var response = new InfoResponse();

            EntityValidator.ValidateForNull(user, response, ResponseMessages.Email_Verification_Succeed, email);

            if (response.IsSuccess)
            {
                if (user.Roles.Any(r => r.RoleId == Constants.User_Id))
                {
                    response.Message = ExceptionMessages.Already_Verified;
                }
                else
                {
                    var userPendingRole = await this.db.UserRoles
                        .Where(ur => ur.RoleId == Constants.Pending_Id && ur.UserId == user.Id)
                        .FirstOrDefaultAsync();

                    this.db.UserRoles.Remove(userPendingRole);

                    var userRole = new UserRole()
                    {
                        RoleId = Constants.User_Id,
                        UserId = user.Id
                    };

                    await this.db.UserRoles.AddAsync(userRole);
                    await this.db.SaveChangesAsync();
                }
            }

            return response;
        }

        private async Task<SendGrid.Response> SendVerificationMail(string email, Guid code)
        {
            var mailSenderResponse = await this.mailSender.SendEmailAsync(
                        ExternalProviders.Abv_Account,
                        ExternalProviders.Sender_Name,
                        email,
                        ExternalProviders.SendGrid_ComfirmMail,
                        string.Format(ExternalProviders.SendGrid_LinkForVerification, email, code));

            return mailSenderResponse;
        }

        private async Task SendVerification(UserRegisterRequestModel user, InfoResponse response, Guid code)
        {
            var mailSenderResponse = await this.SendVerificationMail(user.Email, code);

            if (!mailSenderResponse.IsSuccessStatusCode)
            {
                mailSenderResponse = await this.SendVerificationMail(user.Email, code);
            }

            if (mailSenderResponse.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.Message = ResponseMessages.Check_Email_For_Verification;
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.Invalid_Operation_SendGrid);
            }
        }
    }
}