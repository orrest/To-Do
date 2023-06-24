using Refit;
using System.Threading.Tasks;
using To_Do.Shared;

namespace To_Do.Services;

internal interface IUserService
{
    Task<IApiResponse<string>> LoginAsync([Body] LoginDTO dto);
    Task<IApiResponse> RegisterAsync([Body] RegisterDTO dto);
    Task<IApiResponse> SendConfirmEmailAsync([Query] string email);
}

internal class UserService : IUserService
{
    private readonly IToDoApi api;

    public UserService(IToDoApi api)
    {
        this.api = api;
    }

    public Task<IApiResponse<string>> LoginAsync([Body] LoginDTO dto)
        => api.LoginAsync(dto);

    public Task<IApiResponse> RegisterAsync([Body] RegisterDTO dto)
        => api.RegisterAsync(dto);

    public Task<IApiResponse> SendConfirmEmailAsync([Query] string email)
        => api.SendConfirmEmailAsync(email);
}
