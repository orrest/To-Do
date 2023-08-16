using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do.API.Services;
using To_Do.Shared;
using To_Do.Shared.Paging;

namespace To_Do.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/countdowns/[action]")]
public class CountdownController : ControllerBase
{
    private readonly CountdownService service;
    private readonly Guid userId;

    public CountdownController(
        CountdownService service,
        IUserProvider userProvider)
    {
        this.service = service;
        userId = userProvider.GetUserId();
    }

    [HttpPost]
    public async Task<IList<CountdownDTO>> Get(
        [FromBody] CountdownPagingDTO paging)
    {
        paging.UserId = userId;
        return await service.GetAsync(paging);
    }

    [HttpPost]
    public async Task<CountdownDTO> Add([FromBody] CountdownDTO model)
        => await service.AddAsync(model, userId);

    [HttpDelete("{id}")]
    public async Task<bool> Delete(long id)
        => await service.DeleteAsync(id);

    [HttpPost]
    public async Task<bool> Update([FromBody] CountdownDTO model)
        => await service.UpdateAsync(model, userId);
}