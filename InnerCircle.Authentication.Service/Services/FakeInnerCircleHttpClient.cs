namespace InnerCircle.Authentication.Service.Services
{
    public class FakeInnerCircleHttpClient : IInnerCircleHttpClient
    {
        private readonly ILogger<FakeInnerCircleHttpClient> _logger;

        public FakeInnerCircleHttpClient(ILogger<FakeInnerCircleHttpClient> logger)
        {
            _logger = logger;
        }

        public Task SendPasswordCreationLink(string corporateEmail, string passwordResetToken)
        {
            _logger.LogInformation($"Corporate email: {corporateEmail}, password reset token: {passwordResetToken}");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLink(string email, string token)
        {
            return Task.CompletedTask;
        }

        public async Task<List<string>> GetPrivileges(long accountId)
        {
            return new List<string>
            {
                "CanViewFinanceForPayroll",
                "CanViewAnalytic",
                "CanManageEmployees",
            };
        }
    }
}
