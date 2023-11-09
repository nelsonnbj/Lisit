using System.Linq;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.SeederData
{
    public static class UserSeeder
    {
        public static void SeeadData(AplicationDataContext dbContext)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            dbContext.Users.AddRange(
                new Users
                {
                    Email = "Admin@Admin.com",
                    EmailConfirmed = true,
                    UserName = "Admin@Admin.com",
                    RolesId = Guid.Parse("d93926e6-25c0-4344-acbb-c9f36c44cfb1"),
                    PasswordHash = "@dmIn"
                }
             );

            dbContext.SaveChanges();
        }
    }
}
