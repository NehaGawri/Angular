using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DemoApp.API.Models.Data
{
    public static  class Seed
    {
        public static void SeedUser(DataContext context)
        {
            if(!context.Users.Any())
            {
            var seedData =System.IO.File.ReadAllText("Models/Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(seedData);
            foreach(var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash=passwordHash;
                user.PasswordSalt=passwordSalt;
                user.UserName= user.UserName.ToLower();
                context.Users.Add(user);
            }
            context.SaveChanges();
            }
        }

         private  static void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
          using(var hmac= new System.Security.Cryptography.HMACSHA512())
          {
              passwordsalt= hmac.Key;
              passwordhash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          }
        }
    }
}