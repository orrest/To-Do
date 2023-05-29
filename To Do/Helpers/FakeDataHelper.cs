using System;
using System.Collections.ObjectModel;
using To_Do.ViewModels;

namespace To_Do.Models;

public static class FakeDataHelper
{
    public static void CreateTasks(ObservableCollection<TaskViewModel> tasks)
    {
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 1...",
            IsFinished = false,
            IsStared = false,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.DAY_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 2...",
            IsFinished = false,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.DAY_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 3...",
            IsFinished = true,
            CreateTime = DateTime.Now,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.DAY_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 4...",
            IsFinished = true,
            IsStared = false,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.WEEK_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 5...",
            IsFinished = false,
            IsStared = false,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.WEEK_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 6...",
            IsFinished = true,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.WEEK_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 7...",
            IsFinished = false,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.MONTH_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 8...",
            IsFinished = false,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.MONTH_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 9...",
            IsFinished = false,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.MONTH_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 10...",
            IsFinished = false,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.LONGTERM_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 11...",
            IsFinished = false,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.LONGTERM_TASK
        });
        tasks.Add(new TaskViewModel()
        {
            Description = "Task 12...",
            IsFinished = false,
            IsStared = true,
            CreateTime = DateTime.Now,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.LONGTERM_TASK
        });
    }
}