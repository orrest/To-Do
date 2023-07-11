using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do.API.Services;
using To_Do.Shared;

namespace To_Do.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/todos/[action]")]
public class TaskController : ControllerBase
{
    private readonly TasksService service;
    private readonly Guid userId;

    public TaskController(
        TasksService service,
        IUserProvider userProvider)
    {
        this.service = service;
        userId = userProvider.GetUserId();
    }

    [HttpPost]
    public async Task<IList<TaskDTO>> Get(
        [FromBody] TaskPagingDTO paging)
    {
        paging.UserId = userId;
        return await service.GetAsync(paging);
    }

    [HttpPost]
    public async Task<TaskDTO> Add([FromBody] TaskDTO model)
        => await service.AddAsync(model, userId);

    [HttpDelete("{id}")]
    public async Task<bool> Delete(long id)
        => await service.DeleteAsync(id);

    [HttpPost]
    public async Task<bool> Update([FromBody] TaskDTO model)
        => await service.UpdateAsync(model, userId);
}
