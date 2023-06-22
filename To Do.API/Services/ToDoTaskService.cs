using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_Do.API.Entities;
using To_Do.Shared;

namespace To_Do.API.Services;

public abstract class IToDoTaskService : Service<ToDoTaskAddingDTO, ToDoTask>
{
    protected IToDoTaskService(IUnitOfWork unitOfWork, ILogger<Service<ToDoTaskAddingDTO, ToDoTask>> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
    {
    }

    public abstract Task<IList<ToDoTaskAddingDTO>> GetAsync(int type, int pageIndex, int pageSize);

    public abstract Task<ToDoTaskAddingDTO> AddAsync(ToDoTaskAddingDTO mode, Guid userId);
}

public class ToDoTaskService : IToDoTaskService
{
    public ToDoTaskService(IUnitOfWork unitOfWork, ILogger<Service<ToDoTaskAddingDTO, ToDoTask>> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
    {
    }

    public override async Task<IList<ToDoTaskAddingDTO>> GetAsync(int type, int pageIndex, int pageSize)
    { 
        var repo = unitOfWork.GetRepository<ToDoTask>();
        
        var res = await repo.GetPagedListAsync(
            predicate: tsk => tsk.TaskType.Equals(type),
            pageIndex: pageIndex,
            pageSize: pageSize);

        return mapper.Map<IList<ToDoTask>, IList<ToDoTaskAddingDTO>>(res.Items);
    }

    public override Task<ToDoTaskAddingDTO?> AddAsync(ToDoTaskAddingDTO model)
    {
        throw new AccessViolationException("Do not use this.");
    }

    public override async Task<ToDoTaskAddingDTO> AddAsync(ToDoTaskAddingDTO model, Guid userId)
    {
        var entity = mapper.Map<ToDoTask>(model);
        entity.UserId = userId;
        entity.CreateTime = DateTime.Now;
        entity.UpdateTime = DateTime.Now;

        var add = await unitOfWork.GetRepository<ToDoTask>().InsertAsync(entity);
        var res = await unitOfWork.SaveChangesAsync();
        var ent = add.Entity;

        return res > 0 
            ? mapper.Map<ToDoTaskAddingDTO>(ent)
            : throw new DbUpdateException("Insertion failed.");
    }
}
