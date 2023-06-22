using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;

namespace To_Do.API.Services;

/// <summary>
/// This class contains the common CRUD operations.
/// </summary>
/// <typeparam name="DTO">The DTO are the data classes in the Shared lib.</typeparam>
/// <typeparam name="ENT">The ENT are the data classes in the ./API/Entities/ folder.</typeparam>
public abstract class Service<DTO, ENT> 
    where DTO : class 
    where ENT : class
{
    protected readonly IUnitOfWork unitOfWork;
    protected readonly ILogger logger;
    protected readonly IMapper mapper;

    public Service(
        IUnitOfWork unitOfWork,
        ILogger<Service<DTO, ENT>> logger,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
        this.mapper = mapper;
    }

    public virtual async Task<IList<DTO>> GetPagedListAsync(int pageIndex, int pageSize = 20)
    {
        var res = await unitOfWork.GetRepository<ENT>()
            .GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);

        var afterMap = mapper.Map<IList<ENT>, IList<DTO>>(res.Items);

        return afterMap;
    }

    public virtual async Task<DTO> GetAsync(long id)
    {
        var res = await unitOfWork.GetRepository<ENT>().FindAsync(id);
        return mapper.Map<DTO>(res);
    }

    public virtual async Task<DTO?> AddAsync(DTO model)
    {
        var entity = mapper.Map<ENT>(model);

        var add = await unitOfWork.GetRepository<ENT>().InsertAsync(entity);
        var res = await unitOfWork.SaveChangesAsync();
        var ent = add.Entity;

        return res > 0 ? mapper.Map<DTO>(ent) : null;
    }

    public virtual async Task<bool> UpdateAsync(DTO model)
    {
        var entity = mapper.Map<ENT>(model);

        unitOfWork.GetRepository<ENT>().Update(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }

    public virtual async Task<bool> DeleteAsync(long id)
    {
        var repo = unitOfWork.GetRepository<ENT>();
        var entity = await repo.FindAsync(id);
        repo.Delete(entity);
        var res = await unitOfWork.SaveChangesAsync();

        return res > 0 ? true : false;
    }
}
