using System.Security.Claims;

namespace To_Do.API.Services;

public interface IUserProvider
{
    Guid GetUserId();
}

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor context;

    public UserProvider(IHttpContextAccessor context)
    {
        this.context = context;
    }

    public Guid GetUserId()
    {
        var guidStr = context.HttpContext!.User.Claims
            .First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(guidStr);
    }
}
