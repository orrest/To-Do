using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class WeekViewModel : ToDoBaseViewModel
{
    public WeekViewModel(IToDoTaskService service, IMapper mapper) 
        : base("÷‹»ŒŒÒ", service, TaskType.WEEK, mapper)
    {
        Initialize();
    }

    public async void Initialize()
    {
        var response = await service.GetAsync(new TaskPagingDTO()
        {
            TaskType = taskType,
            PageIndex = 0
        });

        if (response.IsSuccessStatusCode)
        {
            var tasks = response.Content;
            var vms = mapper.Map<IList<TaskDTO>, IList<TaskViewModel>>(tasks!);
            Tasks.AddRange(vms);
        }
        else
        {

        }
    }
}