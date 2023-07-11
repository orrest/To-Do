using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;

namespace To_Do.API.Services;

public abstract class IService<ENT, DTO>
    where ENT : class
    where DTO : class
{
    protected readonly IUnitOfWork unitOfWork;
    protected readonly IMapper mapper;
    protected readonly IRepository<ENT> repo;

    public IService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        repo = unitOfWork.GetRepository<ENT>();
    }
}