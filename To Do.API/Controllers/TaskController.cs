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
    private readonly ITaskService service;
    private readonly Guid userId;

    public TaskController(ITaskService service, IUserProvider userProvider)
    {
        this.service = service;
        userId = userProvider.GetUserId();
    }

    [HttpPost]
    public async Task<IList<TaskGettingDTO>> Get(
        [FromBody] TaskPagingDTO paging)
        => await service.GetAsync(userId, paging);

    [HttpGet("page_index/{pageIndex}")]
    public async Task<IList<TaskAddingDTO>> GetPage(int pageIndex, int pageSize = 20)
        => await service.GetPagedListAsync(pageIndex, pageSize);

    [HttpPost]
    public async Task<TaskAddingDTO> Add([FromBody] TaskAddingDTO model)
        => await service.AddAsync(model, userId);

    [HttpDelete("{id}")]
    public async Task<bool> Delete(long id)
        => await service.DeleteAsync(id);

    [HttpPost]
    public async Task<bool> Update([FromBody] TaskAddingDTO model)
        => await service.UpdateAsync(model);
}
