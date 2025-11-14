namespace Api.Services.Models;

public readonly struct PasswordSetModel
{
    public string CorporateEmail { get; init; }

    public string NewPassword { get; init; }
}