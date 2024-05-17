using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Models
{
    public class AppUser : IdentityUser
    {
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

    }
}
