using System;
using Todo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Todo.Data.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)  
        {  }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            user.ToTable("user");
            user.HasMany(x => x.Todos)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            var todoItem = modelBuilder.Entity<TodoItem>();
            todoItem.ToTable("todo");
            todoItem.HasOne(x => x.User)
                .WithMany(x => x.Todos)
                .HasForeignKey(x => x.UserId);


            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasData(
                   new User
                   {
                       UserId = 1,
                       Email = "admin@admin.com",
                       Password = "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92", // 123456
                       FullName = "Walterli Valadares Junior",
                       Perfil = Domain.Enum.PerfilEnum.ADMIN,
                   },
                   new User
                   {
                       UserId = 2,
                       Email = "teste_um@teste_um.com",
                       Password = "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92", // 123456
                       FullName = "Walterli Valadares Junior",
                       Perfil = Domain.Enum.PerfilEnum.USER,
                   },
                   new User
                   {
                       UserId = 3,
                       Email = "teste_dois@teste_dois.com",
                       Password = "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92", // 123456
                       FullName = "Walterli Valadares Junior",
                       Perfil = Domain.Enum.PerfilEnum.USER,
                   }
            );

            modelBuilder.Entity<TodoItem>()
            .HasData(
                   // TESTE_UM
                   new TodoItem
                   {
                       TodoId = 1,
                       DateInsertion = DateTime.Now.AddDays(-4),
                       Deadline = DateTime.Now.AddDays(+5),
                       Description = "Primeira tarefa do usuario 2",
                       Status = Domain.Enum.StatusTodoEnum.BACKLOG,
                       UserId = 2
                   },
                   new TodoItem
                   {
                       TodoId = 2,
                       DateInsertion = DateTime.Now.AddDays(-2),
                       Deadline = DateTime.Now.AddDays(+3),
                       Description = "Segunda tarefa do usuario 2",
                       Status = Domain.Enum.StatusTodoEnum.TODO,
                       UserId = 2
                   },
                   new TodoItem
                   {
                       TodoId = 3,
                       DateInsertion = DateTime.Now.AddDays(-1),
                       Deadline = DateTime.Now.AddDays(+1),
                       Description = "Terceira tarefa do usuario 2",
                       Status = Domain.Enum.StatusTodoEnum.DOING,
                       UserId = 2
                   },
                   new TodoItem
                   {
                       TodoId = 4,
                       DateInsertion = DateTime.Now.AddDays(-10),
                       Deadline = DateTime.Now.AddDays(-1),
                       Description = "Quarta tarefa do usuario 2",
                       Status = Domain.Enum.StatusTodoEnum.DOING,
                       UserId = 2
                   },
                   new TodoItem
                   {
                       TodoId = 5,
                       DateInsertion = DateTime.Now.AddDays(-9),
                       Deadline = DateTime.Now.AddDays(-2),
                       Description = "Quinta tarefa do usuario 2",
                       Status = Domain.Enum.StatusTodoEnum.TODO,
                       UserId = 2
                   },

                   // TESTE_DOIS
                   new TodoItem
                   {
                       TodoId = 6,
                       DateInsertion = DateTime.Now.AddDays(-10),
                       Deadline = DateTime.Now.AddDays(-1),
                       Description = "Primeira tarefa do usuario 3",
                       Status = Domain.Enum.StatusTodoEnum.DOING,
                       UserId = 3
                   },
                   new TodoItem
                   {
                       TodoId = 7,
                       DateInsertion = DateTime.Now.AddDays(-9),
                       Deadline = DateTime.Now.AddDays(-2),
                       Description = "Segunda tarefa do usuario 3",
                       Status = Domain.Enum.StatusTodoEnum.TODO,
                       UserId = 3
                   },
                   new TodoItem
                   {
                       TodoId = 8,
                       DateInsertion = DateTime.Now.AddDays(-4),
                       Deadline = DateTime.Now.AddDays(+5),
                       Description = "Terceira tarefa do usuario 3",
                       Status = Domain.Enum.StatusTodoEnum.BACKLOG,
                       UserId = 3
                   },

                   // ADM
                   new TodoItem
                   {
                       TodoId = 9,
                       DateInsertion = DateTime.Now,
                       Deadline = DateTime.Now.AddDays(+14),
                       Description = "Gerenciar a equipe do usuario 1",
                       Status = Domain.Enum.StatusTodoEnum.TODO,
                       UserId = 1
                   }
            );
        }

        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}