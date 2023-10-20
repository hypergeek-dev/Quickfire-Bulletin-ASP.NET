using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quickfire_Bulletin_ASP.NET.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);

// Make sure this connection string is included in your appsettings.json
var connectionString = builder.Configuration.GetConnectionString("Quickfire_Bulletin_ContextConnection") ?? throw new InvalidOperationException("Connection string 'Quickfire_Bulletin_ContextConnection' not found.");

// Register database context and identity
builder.Services.AddDbContext<Quickfire_Bulletin_Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Quickfire_Bulletin_ASPNETUser>()
    .AddRoles<IdentityRole>() // Include Role services
    .AddEntityFrameworkStores<Quickfire_Bulletin_Context>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();  // Added RazorPages to services

var app = builder.Build();

await Task.Run(() => ConfigureApp(app));

async Task ConfigureApp(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();  // Enclosed in UseEndpoints
    });

    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "Admin", "Member" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    using (var scope = app.Services.CreateScope())
    {
        var _userManager = scope.ServiceProvider.GetService<UserManager<Quickfire_Bulletin_ASPNETUser>>();

        string email = "admin@admin.com";
        string password = "1234";

        if (await _userManager.FindByEmailAsync(email) == null)
        {
            var user = new Quickfire_Bulletin_ASPNETUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
            }
        }
    }

    app.Run();
}
