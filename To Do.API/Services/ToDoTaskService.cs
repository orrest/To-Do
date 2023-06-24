using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_Do.API.Entities;
using To_Do.Shared;

namespace To_Do.API.Services;

public abstract class ITaskService : Service<TaskDTO, ToDoTask>
{
    protected ITaskService(
        IUnitOfWork unitOfWork, 
        ILogger<Service<TaskDTO, ToDoTask>> logger, 
        IMapper mapper
    ) : base(unitOfWork, logger, mapper) {  }

    public abstract Task<IList<TaskDTO>> GetAsync(
        Guid userId, TaskPagingDTO paging, int pageSize = 20);

    public abstract Task<TaskDTO> AddAsync(
        TaskDTO mode, Guid userId);
}

public class ToDoTaskService : ITaskService
{
    public ToDoTaskService(IUnitOfWork unitOfWork, ILogger<Service<TaskDTO, ToDoTask>> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
    {
    }

    public override async Task<IList<TaskDTO>> GetAsync(
        Guid userId, TaskPagingDTO paging, int pageSize = 20)
    { 
        var repo = unitOfWork.GetRepository<ToDoTask>();
        
        var res = await repo.GetPagedListAsync(
            predicate: tsk => tsk.TaskType.Equals(paging.TaskType) && tsk.UserId.Equals(userId),
            pageIndex: paging.PageIndex,
            pageSize: pageSize);

        return mapper.Map<IList<ToDoTask>, IList<TaskDTO>>(res.Items);
    }

    public override Task<TaskDTO?> AddAsync(TaskDTO model)
    {
        throw new AccessViolationException("Do not use this.");
    }

    public override async Task<TaskDTO> AddAsync(TaskDTO model, Guid userId)
    {
        var entity = mapper.Map<ToDoTask>(model);
        entity.UserId = userId;
        entity.CreateTime = DateTime.Now;
        entity.UpdateTime = DateTime.Now;

        var add = await unitOfWork.GetRepository<ToDoTask>().InsertAsync(entity);
        var res = await unitOfWork.SaveChangesAsync();
        var ent = add.Entity;

        return res > 0 
            ? mapper.Map<TaskDTO>(ent)
            : throw new DbUpdateException("Insertion failed.");
    }
}
