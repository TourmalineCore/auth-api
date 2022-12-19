namespace InnerCircle.Authentication.Service.Services
{
    public class FakeRequestsService : IRequestsService
    {
        public Task SendPasswordCreatingLink(string email, string token)
        {
            Console.WriteLine(token);
            return Task.CompletedTask;
        }
    }
}
