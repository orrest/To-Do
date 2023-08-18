using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;
using To_Do.Shared.Paging;

namespace To_Do.ViewModels;

internal abstract class ToDoBaseViewModel : BaseViewModel
{
    /// <summary>
    /// ���ѡ�е�Task
    /// </summary>
    private TaskViewModel? selectedTask;
    public TaskViewModel? SelectedTask
    {
        get { return selectedTask; }
        set { selectedTask = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// ����Taskʱ��Task������
    /// </summary>
    private string inputTaskDescriptionText = "";
    public string InputTaskDescriptionText
    {
        get { return inputTaskDescriptionText; }
        set { inputTaskDescriptionText = value; RaisePropertyChanged(); }
    }
    
    /// <summary>
    /// �Ƿ�򿪳���
    /// </summary>
    private bool isDrawerOpen;
    public bool IsDrawerOpen
    {
        get { return isDrawerOpen; }
        set { isDrawerOpen = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// ��ǰ�򿪵ĳ�������
    /// </summary>
    private TaskDrawerViewModel? currentDrawer;
    public TaskDrawerViewModel? CurrentDrawer
    {
        get { return currentDrawer; }
        set { currentDrawer = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// Drawer��ViewModel, �򿪳���ʱ��̬��ʼ��
    /// </summary>
    private Dictionary<TaskViewModel, TaskDrawerViewModel> drawerVms;
    public ObservableCollection<TaskViewModel> Tasks { get; private set; }
    public PagingButtonsViewModel PagingVm { get; private set; }
    public DelegateCommand DrawerOpenCommand { get; private set; }
    public DelegateCommand AddTaskCommand { get; private set; }


    protected TaskType taskType;
    private readonly IEventAggregator aggregator;
    protected readonly IToDoApi service;

    public ToDoBaseViewModel() {  }

    public ToDoBaseViewModel(
        string viewTitle, 
        IToDoApi service, 
        TaskType taskType,
        IEventAggregator aggregator
    ) : base(aggregator) 
    {
        drawerVms = new Dictionary<TaskViewModel, TaskDrawerViewModel>();

        Tasks = new ObservableCollection<TaskViewModel>();
        DrawerOpenCommand = new DelegateCommand(OpenDrawer);
        AddTaskCommand = new DelegateCommand(AddTask);

        this.viewTitle = viewTitle;
        this.service = service;
        this.taskType = taskType;
        this.aggregator = aggregator;

        IsLoading = true;
        IsEmptyList = false;

        LoadingItems();
    }

    public override async void LoadingItems()
    {
        OpenLoading();

        var response = await service.GetAsync(new TaskPagingDTO()
        {
            TaskType = taskType,
            PageIndex = 0
        });

        if (response.IsSuccessStatusCode)
        {
            var page = response.Content;
            var tasks = page.Items;
            PagingVm = new PagingButtonsViewModel()
            {
                CurrentPage = page.IndexFrom,
                TotalPages = page.TotalPages,
                IsBackwardEnable = page.HasPreviousPage,
                IsForwardEnable = page.HasNextPage
            };
            foreach (var dto in tasks)
            {
                Tasks.Insert(0, new TaskViewModel(service, aggregator)
                {
                    TaskId = dto.TaskId,
                    TaskDescription = dto.TaskDescription,
                    TaskMemo = dto.TaskMemo,
                    IsStared = dto.IsStared,
                    IsFinished = dto.IsFinished,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    TaskType = dto.TaskType
                });
            }

            CloseLoading(tasks.Count > 0);
        }
        else
        {
            aggregator.PublishMessage(viewTitle, $"��������ʧ�� {response.StatusCode}");
            CloseLoading(false);
        }
    }

    private void OpenDrawer()
    {
        IsDrawerOpen = !IsDrawerOpen;

        if (SelectedTask != null)
        {
            var isContained = drawerVms.ContainsKey(SelectedTask);

            if (!isContained)
            {
                drawerVms[SelectedTask] = new TaskDrawerViewModel(
                    service,
                    aggregator,
                    SelectedTask,
                    () => { IsDrawerOpen = false; }, 
                    (task) => { Tasks.Remove(task); });
            }

            CurrentDrawer = drawerVms[SelectedTask];
        }
        else
        {
            throw new ApplicationException("Task not selected, this OpenDrawer method shouldn't be triggered");
        }
    }

    /// <summary>
    /// ���һ��������, ��VM�и���
    /// </summary>
    private async void AddTask()
    {
        var task = new TaskDTO()
        {
            TaskType = taskType,
            TaskDescription = InputTaskDescriptionText,
            TaskMemo = "",
            IsFinished = false,
            IsStared = false
        };

        var response = await service.AddAsync(task);

        if (response.IsSuccessStatusCode)
        {
            var dto = response.Content;
            Tasks.Insert(0, new TaskViewModel(service, aggregator)
            {
                TaskId = dto.TaskId,
                TaskDescription = dto.TaskDescription,
                TaskMemo = dto.TaskMemo,
                IsStared = dto.IsStared,
                IsFinished = dto.IsFinished,
                CreateTime = dto.CreateTime,
                UpdateTime = dto.UpdateTime,
                TaskType = dto.TaskType
            });
            InputTaskDescriptionText = "";
            IsEmptyList = false;
        }
        else
        {
            // TODO snackbar mq
        }
    }
}