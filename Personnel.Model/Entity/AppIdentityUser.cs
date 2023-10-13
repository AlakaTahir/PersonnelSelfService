using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Personnel.Model.Entity
{
    public class AppIdentityUser : IdentityUser
    {
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
    }
}
