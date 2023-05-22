using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using To_Do.API.Entities;

namespace To_Do.API.Contexts;

public class UserRoleDbContext: IdentityDbContext<User, Role, long>
{
    public UserRoleDbContext(DbContextOptions<UserRoleDbContext> options) : base(options)
    {
        // intend to be empty.
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}