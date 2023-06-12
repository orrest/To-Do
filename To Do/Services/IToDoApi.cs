using Refit;
using System.Threading.Tasks;
using To_Do.Shared;

namespace To_Do.Services;

internal interface IToDoApi
{
    #region ToDo
    [Get(SecretConstants.GET_TODO_BY_ID_API)]
    Task<IApiResponse> Get(int id);
    #endregion

    #region User
    [Post(SecretConstants.LOGIN_API)]
    Task<IApiResponse<string>> LoginAsync([Body] LoginDTO dto);

    [Post(SecretConstants.REGISTER_API)]
    Task<IApiResponse> RegisterAsync([Body] RegisterDTO dto);

    [Post(SecretConstants.SEND_EMAIL_CONFIRM_TOKEN)]
    Task<IApiResponse> SendConfirmEmailAsync([Query] string email);
    #endregion
}
