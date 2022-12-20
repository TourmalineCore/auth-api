namespace InnerCircle.Authentication.Service.Services.Models
{
    public class CreatePasswordModel
    {
        public string Login { get; set; }
        public string UserResetPasswordToken { get; set; }
        public string Password { get; set; }
    }
}
