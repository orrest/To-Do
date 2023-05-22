using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace To_Do.API.Contexts;

public class ApplicationDbContext: IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        // intend to be empty.
    }
}