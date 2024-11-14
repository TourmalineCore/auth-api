namespace Api.Services.Models
{
    public readonly struct PasswordChangeModel
    {
        public string CorporateEmail { get; init; }

        public string PasswordResetToken { get; init; }

        public string NewPassword { get; init; }
    }
}