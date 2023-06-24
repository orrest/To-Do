using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using To_Do.Shared;

namespace To_Do.Services;

internal interface IToDoTaskService
{
    Task<IApiResponse<TaskDTO>> AddAsync([Body] TaskDTO dto);
    Task<IApiResponse<bool>> DeleteAsync(long id);
    Task<IApiResponse<bool>> UpdateAsync([Body] TaskDTO dto);
    Task<IApiResponse<IList<TaskDTO>>> GetAsync(TaskPagingDTO paging);
}

internal class ToDoTaskService : IToDoTaskService
{
    private readonly IToDoApi api;

    public ToDoTaskService(IToDoApi api)
    {
        this.api = api;
    }

    public Task<IApiResponse<TaskDTO>> AddAsync([Body] TaskDTO dto)
        => api.AddAsync(dto);

    public Task<IApiResponse<bool>> DeleteAsync(long id)
        => api.DeleteAsync(id);

    public Task<IApiResponse<bool>> UpdateAsync([Body] TaskDTO dto)
        => api.UpdateAsync(dto);

    public Task<IApiResponse<IList<TaskDTO>>> GetAsync(TaskPagingDTO paging)
        => api.GetAsync(paging);
}
