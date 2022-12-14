using InnerCircle.Authentication.Service.Services.Models;

namespace InnerCircle.Authentication.Service.Services.Callbacks
{
    internal class RegisterCallbacks
    {
        public Task OnRegistrationExecuted(RegistrationModel registrationModel)
        {
            Console.WriteLine($"Registration has been successfully completed! + {registrationModel.Code}");
            return Task.CompletedTask;
        }
    }
}
