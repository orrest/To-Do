using Refit;
using System.Threading.Tasks;
using To_Do.Shared;
using To_Do.Secrets;
using System.Collections.Generic;

namespace To_Do.Services;

public interface IToDoApi
{
    #region ToDo Steps
    [Headers("Authorization: Bearer")]
    [Post(SecretConstants.ADD_STEPS_API)]
    Task<IApiResponse<TaskStepDTO>> AddStepAsync([Body] TaskStepDTO dto);

    [Headers("Authorization: Bearer")]
    [Delete(SecretConstants.DEL_STEPS_API)]
    Task<IApiResponse<bool>> DeleteStepAsync(long id);

    [Headers("Authorization: Bearer")]
    [Post(SecretConstants.UPD_STEPS_API)]
    Task<IApiResponse<bool>> UpdateStepAsync([Body] TaskStepDTO dto);

    [Headers("Authorization: Bearer")]
    [Post(SecretConstants.GET_STEPS_API)]
    Task<IApiResponse<IList<TaskStepDTO>>> GetStepsAsync([Body] TaskStepPagingDTO paging);
    #endregion

    #region ToDo
    [Headers("Authorization: Bearer")]
    [Post(SecretConstants.ADD_TODO_API)]
    Task<IApiResponse<TaskDTO>> AddAsync([Body] TaskDTO dto);

    [Headers("Authorization: Bearer")]
    [Delete(SecretConstants.DEL_TODO_API)]
    Task<IApiResponse<bool>> DeleteAsync(long id);

    [Headers("Authorization: Bearer")]
    [Post(SecretConstants.UPD_TODO_API)]
    Task<IApiResponse<bool>> UpdateAsync([Body] TaskDTO dto);

    [Headers("Authorization: Bearer")]
    [Post(SecretConstants.GET_TODO_API)]
    Task<IApiResponse<IList<TaskDTO>>> GetAsync([Body] TaskPagingDTO paging);
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
