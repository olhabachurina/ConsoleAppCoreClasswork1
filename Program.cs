namespace ConsoleAppCoreClasswork1
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using System.Collections.Generic;
    using System.Diagnostics;

    class Program
    {


        static void Main()
        {
            using (var context = new ApplicationContext())
            {

                var newUser = new User { Name = "Anna Demidenko", Age = 25 };
                context.Users.Add(newUser);
                context.SaveChanges();

                var userById = context.Users.Find(newUser.Id);

                if (userById != null)
                {
                    Console.WriteLine($"User found by Id: {userById.Id}, Name: {userById.Name}, Age: {userById.Age}");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
        public class ApplicationContext : DbContext
        {

            public DbSet<User> Users { get; set; }
            public ApplicationContext()
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=testDB;Integrated Security=True;TrustServerCertificate=True;");
                optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
            }
        }
    }
}

