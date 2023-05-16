using System.Collections.ObjectModel;

namespace To_Do.Models;

public static class FakeDataHelper
{
    public static void CreateTasks(ObservableCollection<TaskModel> tasks)
    {
        tasks.Add(new TaskModel()
        {
            Description = "Task 1...",
            IsFinished = false,
            IsStared = false,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.DAY_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 2...",
            IsFinished = false,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.DAY_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 3...",
            IsFinished = true,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.DAY_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 4...",
            IsFinished = true,
            IsStared = false,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.WEEK_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 5...",
            IsFinished = false,
            IsStared = false,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.WEEK_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 6...",
            IsFinished = true,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.WEEK_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 7...",
            IsFinished = false,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.MONTH_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 8...",
            IsFinished = false,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.MONTH_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 9...",
            IsFinished = false,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.MONTH_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 10...",
            IsFinished = false,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.LONGTERM_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 11...",
            IsFinished = false,
            IsStared = true,
            Steps = new System.Collections.Generic.List<string>()
            {
                "First, ...",
                "Then, ...",
                "Finally, ..."
            },
            Category = TaskCategory.LONGTERM_TASK
        });
        tasks.Add(new TaskModel()
        {
            Description = "Task 12...",
            IsFinished = false,
            IsStared = true,
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