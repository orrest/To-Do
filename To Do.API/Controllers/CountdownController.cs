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
[Route("/api/countdowns/[action]")]
public class CountdownController : ControllerBase
{
    private readonly Guid userId;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private IRepository<CountdownEntity> repo;

    public CountdownController(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserProvider userProvider)
    {
        userId = userProvider.GetUserId();
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.repo = unitOfWork.GetRepository<CountdownEntity>();
    }

    [HttpPost]
    public async Task<IActionResult> Get(
        [FromBody] CountdownPagingDTO paging)
    {
        try
        {
            var res = await repo.GetPagedListAsync(
                predicate: cd => cd.UserId.Equals(userId),
                pageIndex: paging.PageIndex,
                pageSize: paging.PageSize,
                orderBy: items => items.OrderByDescending(ent => ent.UpdateTime));

            var mappedRes = PagedList.From(
                res,
                items => mapper.Map<IEnumerable<CountdownEntity>, IEnumerable<CountdownDTO>>(items));
            return Ok(mappedRes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CountdownDTO model)
    {
        try
        {
            var entity = mapper.Map<CountdownEntity>(model);
            entity.UserId = userId;

            var add = await repo.InsertAsync(entity);
            var res = await unitOfWork.SaveChangesAsync();
            var ent = add.Entity;

            return res > 0
                ? Ok(mapper.Map<CountdownDTO>(ent))
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
            var repo = unitOfWork.GetRepository<CountdownEntity>();
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
    public async Task<IActionResult> Update([FromBody] CountdownDTO model)
    {
        try
        {
            var entity = mapper.Map<CountdownEntity>(model);
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