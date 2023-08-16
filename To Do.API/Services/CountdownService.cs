using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_Do.API.Entities;
using To_Do.Shared;
using To_Do.Shared.Paging;

namespace To_Do.API.Services;

public abstract class ICountdownService<ENT, DTO> : IService<ENT, DTO>
    where ENT : class
    where DTO : class
{
    public ICountdownService(
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public abstract Task<IList<CountdownDTO>> GetAsync(CountdownPagingDTO paging);

    public abstract Task<CountdownDTO> AddAsync(
        CountdownDTO model,
        Guid userId);

    public abstract Task<bool> UpdateAsync(
        CountdownDTO model,
        Guid userId);

    public virtual async Task<bool> DeleteAsync(long id)
    {
        var repo = unitOfWork.GetRepository<CountdownEntity>();
        var entity = await repo.FindAsync(id);
        repo.Delete(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }
}

public class CountdownService : ICountdownService<CountdownEntity, CountdownDTO>
{
    public CountdownService(
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public override async Task<IList<CountdownDTO>> GetAsync(CountdownPagingDTO paging)
    {
        var res = await repo.GetPagedListAsync(
            predicate: cd => cd.UserId.Equals(paging.UserId),
            pageIndex: paging.PageIndex,
            pageSize: paging.PageSize);

        return mapper.Map<IList<CountdownEntity>, IList<CountdownDTO>>(res.Items);
    }

    public override async Task<CountdownDTO> AddAsync(
        CountdownDTO model,
        Guid userId)
    {
        var entity = mapper.Map<CountdownEntity>(model);
        entity.UserId = userId;

        var add = await repo.InsertAsync(entity);
        var res = await unitOfWork.SaveChangesAsync();
        var ent = add.Entity;

        return res > 0
            ? mapper.Map<CountdownDTO>(ent)
            : throw new DbUpdateException("Insertion failed.");
    }

    public override async Task<bool> UpdateAsync(
        CountdownDTO model,
        Guid userId)
    {
        var entity = mapper.Map<CountdownEntity>(model);
        entity.UserId = userId;
        entity.UpdateTime = DateTime.Now;

        repo.Update(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }
}
