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

    public virtual DbSet<TaskEntity> Tasks { get; set; }
    public virtual DbSet<TaskStepEntity> TaskSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(b =>
        {
            b.HasMany<TaskEntity>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        });

        builder.Entity<TaskEntity>(b =>
        {
            b.HasKey(tsk => tsk.TaskId);
            b.ToTable("ToDoTasks");
            b.HasMany<TaskStepEntity>().WithOne().HasForeignKey(tsk => tsk.TaskId).IsRequired();
        });

        builder.Entity<TaskStepEntity>(b =>
        {
            b.HasKey(step => step.StepId);
            b.ToTable("ToDoTaskSteps");
        });
    }
}