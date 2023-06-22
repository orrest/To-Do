using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using To_Do.API.Entities;

namespace To_Do.API.Contexts;

public class ApplicationDbContext: IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // intend to be empty.
    }

    public virtual DbSet<ToDoTask> Tasks { get; set; }
    public virtual DbSet<ToDoTaskStep> TaskSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(b =>
        {
            b.HasMany<ToDoTask>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        });

        builder.Entity<ToDoTask>(b =>
        {
            b.HasKey(tsk => tsk.TaskId);
            b.ToTable("ToDoTasks");
            b.HasMany<ToDoTaskStep>().WithOne().HasForeignKey(tsk => tsk.TaskId).IsRequired();
        });

        builder.Entity<ToDoTaskStep>(b =>
        {
            b.HasKey(step => step.StepId);
            b.ToTable("ToDoTaskSteps");
        });
    }
}