using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using To_Do.Shared;

namespace To_Do.Services;

public interface IToDoTaskService
{
    Task<IApiResponse<TaskAddingDTO>> AddAsync([Body] TaskAddingDTO dto);
    Task<IApiResponse<IList<TaskGettingDTO>>> GetAsync(TaskPagingDTO paging);
}

internal class ToDoTaskService : IToDoTaskService
{
    private readonly IToDoApi api;

    public ToDoTaskService(IToDoApi api)
    {
        this.api = api;
    }

    public Task<IApiResponse<TaskAddingDTO>> AddAsync([Body] TaskAddingDTO dto)
        => api.AddAsync(dto);

    public Task<IApiResponse<IList<TaskGettingDTO>>> GetAsync(TaskPagingDTO paging)
        => api.GetAsync(paging);
}
