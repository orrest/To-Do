using Refit;
using System.Threading.Tasks;
using To_Do.Shared;
using To_Do.Secrets;

namespace To_Do.Services;

internal interface IToDoApi
{
    #region ToDo
    [Headers("Authorization: Bearer")]
    [Post(SecretConstants.ADD_TODO_API)]
    Task<IApiResponse<ToDoTaskAddingDTO>> AddAsync([Body] ToDoTaskAddingDTO dto);

    [Headers("Authorization: Bearer")]
    [Get(SecretConstants.GET_TODO_API)]
    Task<IApiResponse<ToDoTaskGettingDTO>> GetAsync(int page);

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
