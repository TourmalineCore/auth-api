using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class User : IdentityUser<long>
    {
        /// <summary>
        /// This is a custom user entity to which you can add the necessary properties
        /// </summary>
        public bool IsBlocked { get; set; }
        public string Code { get; set; }
    }
}