using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do.API.Entities;
using To_Do.Shared;

namespace To_Do.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/steps/[action]")]
public class TaskStepController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private IRepository<TaskStepEntity> repo;

    public TaskStepController(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.repo = unitOfWork.GetRepository<TaskStepEntity>();
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] TaskStepPagingDTO paging)
    {
        try
        {
            var res = await repo.GetPagedListAsync(
                predicate: step => step.TaskId.Equals(paging.TaskId),
                pageIndex: paging.PageIndex,
                pageSize: paging.PageSize,
                orderBy: items => items.OrderByDescending(ent => ent.UpdateTime));

            var mappedRes = PagedList.From(res, items =>
                mapper.Map<IEnumerable<TaskStepEntity>, IEnumerable<TaskStepDTO>>(items));

            return Ok(mappedRes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TaskStepDTO model)
    {
        try
        {
            var entity = mapper.Map<TaskStepEntity>(model);

            var add = await repo.InsertAsync(entity);
            var res = await unitOfWork.SaveChangesAsync();

            return Ok(mapper.Map<TaskStepDTO>(add.Entity));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var entity = await repo.FindAsync(id);
            repo.Delete(entity);
            var res = await unitOfWork.SaveChangesAsync();

            return res > 0 ? Ok(true) : BadRequest("删除失败");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] TaskStepDTO model)
    {
        try
        {
            var entity = mapper.Map<TaskStepEntity>(model);
            repo.Update(entity);
            var res = await unitOfWork.SaveChangesAsync();

            return res > 0 ? Ok(true) : BadRequest("更新失败");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
