using Microsoft.EntityFrameworkCore;

namespace BSA_WebAPI.Models;

public class BsaContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public BsaContext(DbContextOptions<BsaContext> options)
        :base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Team>()
            .HasIndex(x => x.Name)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(x=>x.Email)
            .IsUnique();
        var teams = new List<Team>()
        {
            new Team() with { Id = 1, Name = "Team1", CreatedAt = DateTime.Now },
            new Team() with { Id = 2, Name = "Team2", CreatedAt = DateTime.Now },
        };
        var users = new List<User>() 
        {
            new User()with
            {
                Id = 1,
                BirthDay = DateTime.Now,
                FirstName = "Nikita",
                LastName = "Mikhalchenko",
                Email = "nikitamikhalchenko@gmail.com",
                RegisteredAt = DateTime.Now
            },new User()with
            {
                Id = 2,
                BirthDay = DateTime.Now,
                FirstName = "Name1",
                LastName = "LastName1",
                Email = "email1@gmail.com",
                RegisteredAt = DateTime.Now
            },new User()with
            {
                Id = 3,
                TeamId = 1,
                BirthDay = DateTime.Now,
                FirstName = "Name2",
                LastName = "LastName2",
                Email = "email2@gmail.com",
                RegisteredAt = DateTime.Now
            },new User()with
            {
                Id = 4,
                TeamId = 2,
                BirthDay = DateTime.Now,
                FirstName = "Name3",
                LastName = "LastName3",
                Email = "email3@gmail.com",
                RegisteredAt = DateTime.Now
            }
        };
        var projects = new List<Project>
        {
            new Project() with
            {
                Id = 1,
                AuthorId = 1,
                Description = "Desc1",
                Name = "Name1",
                TeamId = 1,
                CreateAt = DateTime.Now,
                Deadline = DateTime.Now + TimeSpan.FromDays(30)
            },
            new Project() with
            {
                Id = 2,
                AuthorId = 2,
                Description = "Desc1",
                Name = "Name1",
                TeamId = 2,
                CreateAt = DateTime.Now,
                Deadline = DateTime.Now + TimeSpan.FromDays(10)
            }
        };
        var tasks = new List<Task>()
        {
            new Task() with
            {
                Id = 1,
                PerformerId = 3,
                Name = "Name1",
                Description = "Desc1",
                CreatedAt = DateTime.Now,
                State = TaskState.ToDo,
                ProjectId = 1
            },
            new Task() with
            {
                Id = 2,
                PerformerId = 4,
                Name = "Name2",
                Description = "Desc2",
                CreatedAt = DateTime.Now,
                State = TaskState.ToDo,
                ProjectId = 2
            }
        };
        modelBuilder.Entity<Team>().HasData(teams);
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Project>().HasData(projects);
        modelBuilder.Entity<Task>().HasData(tasks);
        base.OnModelCreating(modelBuilder);
    }
}