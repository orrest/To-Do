using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do.API.Entities;
using To_Do.API.Services;
using To_Do.Shared;
using To_Do.Shared.Paging;

namespace To_Do.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/todos/[action]")]
public class TaskController : ControllerBase
{
    private IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly Guid userId;
    protected readonly IRepository<TaskEntity> repo;

    public TaskController(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserProvider userProvider)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.repo = unitOfWork.GetRepository<TaskEntity>();
        this.userId = userProvider.GetUserId();
    }

    [HttpPost]
    public async Task<IActionResult> Get(
        [FromBody] TaskPagingDTO paging)
    {
        try
        {
            var res = await repo.GetPagedListAsync(
                predicate: tsk => tsk.TaskType.Equals(paging.TaskType)
                    && tsk.UserId.Equals(userId),
                pageIndex: paging.PageIndex,
                pageSize: paging.PageSize, 
                orderBy: items => items.OrderByDescending(ent => ent.UpdateTime));

            var mappedRes = PagedList.From(
                res,
                items => mapper.Map<IEnumerable<TaskEntity>, IEnumerable<TaskDTO>>(items));
            return Ok(mappedRes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TaskDTO model)
    {
        try
        {
            var entity = mapper.Map<TaskEntity>(model);
            entity.UserId = userId;
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            var add = await repo.InsertAsync(entity);
            var res = await unitOfWork.SaveChangesAsync();
            var ent = add.Entity;

            return res > 0
                ? Ok(mapper.Map<TaskDTO>(ent))
                : BadRequest("添加失败");
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
    public async Task<IActionResult> Update([FromBody] TaskDTO model)
    {
        try
        {
            var entity = mapper.Map<TaskEntity>(model);
            entity.UserId = userId;
            entity.UpdateTime = DateTime.Now;

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
