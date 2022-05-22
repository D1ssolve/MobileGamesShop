using System.Security.Claims;
using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        if (_roleManager.FindByNameAsync(Config.Admin).Result == null)
        {
            _roleManager.CreateAsync(new IdentityRole(Config.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Config.Customer)).GetAwaiter().GetResult();
        }
        else
        {
            return;
        }

        var adminUser = new ApplicationUser
        {
            UserName = "admin1@gmail.com",
            Email = "admin1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+79002223344",
            FirstName = "Ban",
            LastName = "Admin"
        };

        _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminUser, Config.Admin).GetAwaiter().GetResult();

        var temp1 = _userManager.AddClaimsAsync(adminUser, new[]
        {
            new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
            new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            new Claim(JwtClaimTypes.Role, Config.Admin),
        }).Result;
        
        var customerUser = new ApplicationUser
        {
            UserName = "customer1@gmail.com",
            Email = "customer1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+79002223344",
            FirstName = "Ban",
            LastName = "Cust"
        };

        _userManager.CreateAsync(customerUser, "Cust123*").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(customerUser, Config.Customer).GetAwaiter().GetResult();

        var temp2 = _userManager.AddClaimsAsync(customerUser, new[]
        {
            new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
            new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            new Claim(JwtClaimTypes.Role, Config.Customer),
        }).Result;
    }
}