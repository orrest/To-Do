using AutoMapper;
using To_Do.API.Entities;
using To_Do.Shared;

public class AutoMapperProfile : MapperConfigurationExpression
{
    public AutoMapperProfile()
    {
        CreateMap<ToDoTask, ToDoTaskAddingDTO>().ReverseMap();
        CreateMap<ToDoTask, ToDoTaskGettingDTO>().ReverseMap();
    }
}