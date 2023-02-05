using FineBlog.Data;
using FineBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FineBlog.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admi@gmail.com",
                    Email = "admi@gmail.com",
                    FirstName = "Servet",
                    LastName = "Akcadag",
                }, "admin@12345").Wait();//password=admin@12345
                var appUser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "admi@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }
               
                var listOfPages = new List<Page>()
                {
                   new Page()
                   {
                        Title="Über Uns",
                        Slug= "über uns"
                   },
                    new Page()
                    {
                        Title = "Kontakt",
                        Slug = "kontakt"
                    },
                    new Page()
                    {
                        Title = "Privacy Policy",
                        Slug = "privacy"
                    }
                };
                _context.Pages.AddRange(listOfPages);
                _context.SaveChanges();

            }
        }
    }
}
