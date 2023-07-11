using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_Do.API.Entities;
using To_Do.Shared;

namespace To_Do.API.Services;

public abstract class ITasksService<ENT, DTO> : IService<ENT, DTO>
    where ENT : class
    where DTO : class
{
    public ITasksService(
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public abstract Task<IList<TaskDTO>> GetAsync(TaskPagingDTO paging);

    public abstract Task<TaskDTO> AddAsync(
        TaskDTO model,
        Guid userId);

    public abstract Task<bool> UpdateAsync(
        TaskDTO model,
        Guid userId);

    public virtual async Task<bool> DeleteAsync(long id)
    {
        var repo = unitOfWork.GetRepository<TaskEntity>();
        var entity = await repo.FindAsync(id);
        repo.Delete(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }
}

public class TasksService : ITasksService<TaskEntity, TaskDTO>
{
    public TasksService(
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public override async Task<IList<TaskDTO>> GetAsync(TaskPagingDTO paging)
    {
        var res = await repo.GetPagedListAsync(
            predicate: tsk => tsk.TaskType.Equals(paging.TaskType) && tsk.UserId.Equals(paging.UserId),
            pageIndex: paging.PageIndex,
            pageSize: paging.PageSize);

        return mapper.Map<IList<TaskEntity>, IList<TaskDTO>>(res.Items);
    }

    public override async Task<TaskDTO> AddAsync(
        TaskDTO model,
        Guid userId)
    {
        var entity = mapper.Map<TaskEntity>(model);
        entity.UserId = userId;
        entity.CreateTime = DateTime.Now;
        entity.UpdateTime = DateTime.Now;

        var add = await repo.InsertAsync(entity);
        var res = await unitOfWork.SaveChangesAsync();
        var ent = add.Entity;

        return res > 0 
            ? mapper.Map<TaskDTO>(ent)
            : throw new DbUpdateException("Insertion failed.");
    }

    public override async Task<bool> UpdateAsync(
        TaskDTO model,
        Guid userId)
    {
        var entity = mapper.Map<TaskEntity>(model);
        entity.UserId = userId;
        entity.UpdateTime = DateTime.Now;

        repo.Update(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }
}
