using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do.API.Services;
using To_Do.Shared;

namespace To_Do.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/steps/[action]")]
public class TaskStepsController : ControllerBase
{
    private readonly TaskStepsService service;

    public TaskStepsController(TaskStepsService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IList<TaskStepDTO>> Get([FromBody] TaskStepPagingDTO paging)
     => await service.GetAsync(paging);

    [HttpPost]
    public async Task<TaskStepDTO> Add([FromBody] TaskStepDTO model)
        => await service.AddAsync(model);

    [HttpDelete("{id}")]
    public async Task<bool> Delete(long id)
        => await service.DeleteAsync(id);

    [HttpPost]
    public async Task<bool> Update([FromBody] TaskStepDTO model)
        => await service.UpdateAsync(model);
}
