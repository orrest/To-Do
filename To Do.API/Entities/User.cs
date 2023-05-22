using Microsoft.AspNetCore.Identity;

namespace To_Do.API.Entities;

public class User : IdentityUser<long>
{
    public DateTime CreateTime { get; set; }
}