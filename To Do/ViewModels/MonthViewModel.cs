using AutoMapper;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class MonthViewModel : ToDoBaseViewModel
{
    public MonthViewModel(IToDoApi service, IMapper mapper) 
        : base("тбхннЯ", service, TaskType.MONTH, mapper)
    {

    }

}