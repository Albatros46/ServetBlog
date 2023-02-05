using Microsoft.AspNetCore.Identity;

namespace FineBlog.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //Table Relation
        public List<Post>? Posts { get; set; }
    }
}
