namespace CarShop.Service.Account.Data
{
    //Local
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Models.Response.User;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Exceptions;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Common.Providers.Cloudinary;
    using CarShop.Service.Common.Providers.Mail.SendGrid;
    using Microsoft.AspNetCore.Identity;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountService : BaseService, IAccountService
    {
        private readonly IMailSender<InfoResponse> mailSender;
        //private readonly IMailSender<Response> mailSender;
        private readonly ICloudinaryService cloudinaryService;
        private readonly PasswordHasher<User> passwordHasher;

        public AccountService(CarShopDbContext db, IMailSender<InfoResponse> mailSender, ICloudinaryService cloudinaryService,
                              PasswordHasher<User> passwordHasher)
               : base(db)
        {
            this.mailSender = mailSender;
            this.cloudinaryService = cloudinaryService;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Response<UserResponseModel>> LoginAsync(UserLoginRequestModel userLogin)
        {
            var response = new Response<UserResponseModel>();

            var user = await this.db.Users
                .Where(u => u.Email == userLogin.Email)
                .Select(u => new User()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Password = u.Password
                })
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(user, response, ResponseMessages.Login_Succeed, Constants.User);

            if (response.IsSuccess)
            {
                var result = this.passwordHasher.VerifyHashedPassword(user, user.Password, userLogin.Password);

                if (result != PasswordVerificationResult.Success)
                {
                    ResponseSetter.SetResponse(response, false, ExceptionMessages.Invalid_Password);
                }
            }

            var userPayload = await this.db.Users
                .Where(u => u.Email == userLogin.Email)
                .Select(u => new User()
                {
                    Email = u.Email,
                    Password = u.Password
                })
                .FirstOrDefaultAsync();

            response.Payload = await UserQueries.UserByIdAsync(user.Id, this.db);

            return response;
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
                ResponseSetter.SetResponse(response, false, string.Format(ExceptionMessages.Already_Exist, Constants.User));
                return response;
            }
            else if (doesUserExist != null && doesUserExist.Roles.Any(r => r.Role.Id == Constants.Pending_Id))
            {
                response = await this.SendVerificationMail(user.Email, code);
                ResponseSetter.SetResponse(response, true, ResponseMessages.Check_Email_For_Verification);
            }

            if (!response.IsSuccess)
            {
                var newUser = Mapper.ToUser(user);
                newUser.PicturePath = Constants.Default_Avatar;
                newUser.Code = code;
                newUser.Password = this.passwordHasher.HashPassword(newUser, user.Password);
                await this.db.Users.AddAsync(newUser);
                await this.db.SaveChangesAsync();

                var userRole = new UserRole()
                {
                    RoleId = Constants.Pending_Id,
                    UserId = newUser.Id
                };

                await this.db.UserRoles.AddAsync(userRole);
                await this.db.SaveChangesAsync();

                response = await SendVerificationMail(user.Email, code);
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

            var userPendingRole = await this.db.UserRoles
                .Where(ur => ur.RoleId == Constants.Pending_Id && ur.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (response.IsSuccess)
            {
                if (user.Roles.Any(r => r.RoleId == Constants.User_Id) || userPendingRole == null)
                {
                    ResponseSetter.SetResponse(response, false, ExceptionMessages.Already_Verified);
                }
                else
                {
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

        private async Task<InfoResponse> SendVerificationMail(string email, Guid code)
        {
            var mailSenderResponse = await this.mailSender.SendEmailAsync(
                        ExternalProviders.Abv_Account,
                        ExternalProviders.Sender_Name,
                        email,
                        ExternalProviders.SendGrid_ComfirmMail,
                        string.Format(ExternalProviders.SendGrid_LinkForVerification, email, code));

            return mailSenderResponse;
        }

        //Register with sendgrid mail sender
        //public async Task<InfoResponse> RegisterUserAsync(UserRegisterRequestModel user)
        //{
        //    var response = new InfoResponse();

        //    var doesUserExist = await this.db.Users
        //        .Where(u => u.Email == user.Email || u.Username == user.Username)
        //        .Select(u => new User
        //        {
        //            Roles = u.Roles
        //        })
        //        .FirstOrDefaultAsync();


        //    var code = new Guid();
        //    code = Guid.NewGuid();

        //    if (doesUserExist != null)
        //    {
        //        response.Message = string.Format(ExceptionMessages.Already_Exist, Constants.User);
        //        return response;
        //    }
        //    else if (doesUserExist != null && doesUserExist.Roles.Any(r => r.Role.Id == Constants.Pending_Id))
        //    {
        //        response.IsSuccess = true;
        //        response.Message = ResponseMessages.Check_Email_For_Verification;
        //        await SendVerification(user.Email, response, code);
        //    }

        //    if (!response.IsSuccess)
        //    {
        //        var newUser = Mapper.ToUser(user);
        //        newUser.PicturePath = Constants.Default_Avatar;
        //        newUser.Code = code;
        //        newUser.Password = this.passwordHasher.HashPassword(newUser, user.Password);
        //        await this.db.Users.AddAsync(newUser);
        //        await this.db.SaveChangesAsync();

        //        var userRole = new UserRole()
        //        {
        //            RoleId = Constants.Pending_Id,
        //            UserId = newUser.Id
        //        };

        //        await this.db.UserRoles.AddAsync(userRole);
        //        await this.db.SaveChangesAsync();

        //        await SendVerification(user.Email, response, code);
        //    }

        //    return response;
        //}

        //private async Task SendVerification(string email, InfoResponse response, Guid code)
        //{
        //    var mailSenderResponse = await this.SendVerificationMail(email, code);

        //    if (!mailSenderResponse.IsSuccessStatusCode)
        //    {
        //        mailSenderResponse = await this.SendVerificationMail(email, code);
        //    }

        //    if (mailSenderResponse.IsSuccessStatusCode)
        //    {
        //        response.IsSuccess = true;
        //        response.Message = ResponseMessages.Check_Email_For_Verification;
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException(ExceptionMessages.Invalid_Operation_SendGrid);
        //    }
        //}


        //private async Task<SendGrid.Response> SendVerificationMail(string email, Guid code)
        //{
        //    var mailSenderResponse = await this.mailSender.SendEmailAsync(
        //                ExternalProviders.Abv_Account,
        //                ExternalProviders.Sender_Name,
        //                email,
        //                ExternalProviders.SendGrid_ComfirmMail,
        //                string.Format(ExternalProviders.SendGrid_LinkForVerification, email, code));

        //    return mailSenderResponse;
        //}
    }
}