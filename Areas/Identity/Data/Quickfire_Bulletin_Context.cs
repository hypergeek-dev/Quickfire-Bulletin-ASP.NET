using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quickfire_Bulletin_ASP.NET.Areas.Identity.Data;

namespace Quickfire_Bulletin_ASP.NET.Areas.Identity.Data;

public class Quickfire_Bulletin_Context : IdentityDbContext<Quickfire_Bulletin_ASPNETUser>
{
    public Quickfire_Bulletin_Context(DbContextOptions<Quickfire_Bulletin_Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
