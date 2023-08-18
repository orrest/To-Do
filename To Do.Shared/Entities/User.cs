using Microsoft.AspNetCore.Identity;

namespace To_Do.API.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime CreateTime { get; set; }
}