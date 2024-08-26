using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models
{
    public class User : IdentityUser<long>
    {
        /// <summary>
        /// This is a custom user entity to which you can add the necessary properties
        /// </summary>
        public bool IsBlocked { get; set; }

        public long AccountId { get; set; }
    }
}