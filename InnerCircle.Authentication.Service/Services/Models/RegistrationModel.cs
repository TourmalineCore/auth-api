using TourmalineCore.AspNetCore.JwtAuthentication.Core.Models.Request;

namespace InnerCircle.Authentication.Service.Services.Models
{
    public class RegistrationModel : RegistrationRequestModel
    {
        public string Code { get; set; }
    }
}
