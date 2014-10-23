using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class UserRepository
    {
        PlanerDatabaseEntities entities = new PlanerDatabaseEntities();

        public IQueryable<User> GetAll()
        {
            return entities.Users.AsQueryable();
        }

        public void Add(User newUser)
        {
            if (newUser != null)
            {
                entities.Users.Add(newUser);
                entities.SaveChanges();
            }
        }

        public void Add(string UserName, string HASH_PASSWORD_CHECKSUM)
        {
            User newUser = new User();
            newUser.Name = UserName;
            newUser.Password = HASH_PASSWORD_CHECKSUM;

            if (newUser != null)
            {
                entities.Users.Add(newUser);
                entities.SaveChanges();
            }
        }

        public User GetUserByName(string userName)
        {
            return entities.Users.Single(u => u.Name.Equals(userName));
        }

        public bool Login(string userName, string password)
        {
            return entities.Users.Any(u => u.Name.Equals(userName) && u.Password.Equals(password));
        }

        public bool Exists(string userName)
        {
            var user = entities.Users.SingleOrDefault(u => u.Name.Equals(userName));

            return user != null;
        }
    }
}
