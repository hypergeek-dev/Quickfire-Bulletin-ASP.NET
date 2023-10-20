using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quickfire_Bulletin_ASP.NET.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Quickfire_Bulletin_ContextConnection") ?? throw new InvalidOperationException("Connection string 'Quickfire_Bulletin_ContextConnection' not found.");

builder.Services.AddDbContext<Quickfire_Bulletin_Context>(options =>
    options.UseSqlServer(connectionString));

// Add Identity configuration
builder.Services.AddDefaultIdentity<Quickfire_Bulletin_ASPNETUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<Quickfire_Bulletin_Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity Role and User creation
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Member" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
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
        await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
