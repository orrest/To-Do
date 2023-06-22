using Refit;
using System.Threading.Tasks;
using To_Do.Shared;

namespace To_Do.Services;

public interface IToDoTaskService
{
    Task<IApiResponse<ToDoTaskAddingDTO>> AddAsync([Body] ToDoTaskAddingDTO dto);
}

internal class ToDoTaskService : IToDoTaskService
{
    private readonly IToDoApi api;

    public ToDoTaskService(IToDoApi api)
    {
        this.api = api;
    }

    public Task<IApiResponse<ToDoTaskAddingDTO>> AddAsync([Body] ToDoTaskAddingDTO dto)
        => api.AddAsync(dto);
}
