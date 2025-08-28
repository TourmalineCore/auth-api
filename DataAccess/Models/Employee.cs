namespace DataAccess.Models;

public class Employee
{
    public long Id { get; set; }

    public string FullName { get; set; }

    public string CorporateEmail { get; set; }

    public long TenantId { get; set; }
}