using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using To_Do.API.Entities;
using To_Do.Shared;

namespace To_Do.API.Services;

public abstract class ITaskStepsService<ENT, DTO> : IService<ENT, DTO>
    where ENT : class
    where DTO : class
{
    protected ITaskStepsService(
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public abstract Task<IList<DTO>> GetAsync(TaskStepPagingDTO paging);

    public abstract Task<DTO> AddAsync(DTO model);

    public abstract Task<bool> UpdateAsync(DTO model);

    public virtual async Task<bool> DeleteAsync(long id)
    {
        var repo = unitOfWork.GetRepository<ENT>();
        var entity = await repo.FindAsync(id);
        repo.Delete(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }
}

public class TaskStepsService 
    : ITaskStepsService<TaskStepEntity, TaskStepDTO>
{
    public TaskStepsService(
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public override async Task<TaskStepDTO> AddAsync(TaskStepDTO model)
    {
        var entity = mapper.Map<TaskStepEntity>(model);
        entity.CreateTime = DateTime.Now;
        entity.UpdateTime = DateTime.Now;

        var add = await repo.InsertAsync(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return mapper.Map<TaskStepDTO>(add.Entity);
    }

    public override async Task<IList<TaskStepDTO>> GetAsync(TaskStepPagingDTO paging)
    {
        var res = await repo.GetPagedListAsync(
            predicate: step => step.TaskId.Equals(paging.TaskId),
            pageIndex: paging.PageIndex,
            pageSize: paging.PageSize);

        return mapper.Map<IList<TaskStepEntity>, IList<TaskStepDTO>>(res.Items);
    }

    public override async Task<bool> UpdateAsync(TaskStepDTO model)
    {
        var entity = mapper.Map<TaskStepEntity>(model);
        entity.UpdateTime = DateTime.Now;

        repo.Update(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }
}
