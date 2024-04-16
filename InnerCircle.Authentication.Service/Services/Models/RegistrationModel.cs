namespace InnerCircle.Authentication.Service.Services.Models
{
    public readonly struct RegistrationModel
    {
        public string CorporateEmail { get; init; }

        public long AccountId { get; init; }
    }
}
