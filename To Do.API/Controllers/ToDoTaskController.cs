using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do.API.Services;
using To_Do.Shared;

namespace To_Do.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/todos/[action]")]
public class ToDoTaskController : ControllerBase
{
    private readonly IToDoTaskService service;
    private readonly Guid userId;

    public ToDoTaskController(IToDoTaskService service, IUserProvider userProvider)
    {
        this.service = service;
        userId = userProvider.GetUserId();
    }

    [HttpGet("type/{type}/page_index/{pageIndex}")]
    public async Task<IList<ToDoTaskAddingDTO>> Get(int type, int pageIndex, int pageSize = 20)
        => await service.GetAsync(type, pageIndex, pageSize);

    [HttpGet("{id}")]
    public async Task<ToDoTaskAddingDTO> Get(long id)
    {
        return await service.GetAsync(id);
    }

    [HttpGet("page_index/{pageIndex}")]
    public async Task<IList<ToDoTaskAddingDTO>> GetPage(int pageIndex, int pageSize = 20)
        => await service.GetPagedListAsync(pageIndex, pageSize);

    [HttpPost]
    public async Task<ToDoTaskAddingDTO> Add([FromBody] ToDoTaskAddingDTO model)
        => await service.AddAsync(model, userId);

    [HttpDelete("{id}")]
    public async Task<bool> Delete(long id)
        => await service.DeleteAsync(id);

    [HttpPost]
    public async Task<bool> Update([FromBody] ToDoTaskAddingDTO model)
        => await service.UpdateAsync(model);
}
