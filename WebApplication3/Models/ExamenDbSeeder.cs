using System.Linq;

namespace WebApplication3.Models
{
    public class ExamenDbSeeder
    {
        public static void Initialize(ExamenDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any movies.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.AddRange(
                          new User
                          {
                              FirstName = "Corina",
                              LastName = "Sofron",
                              Username = "corina",
                              Email = "corina@yahoo.com",
                              Password = "123456",
                              UserRole = UserRole.Admin,
                              DataRegistered = System.DateTime.Now
                          }
                      );
            context.SaveChanges();
        }
    }
}
