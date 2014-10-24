using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class UserRepository : BaseRepository
    {
        public IQueryable<User> GetAll()
        {
            return Entities.Users.AsQueryable();
        }

        public User GetById(int Id)
        {
            return Entities.Users.SingleOrDefault(u => u.Id.Equals(Id));
        }

        public void Add(User newUser)
        {
            if (newUser != null)
            {
                Entities.Users.Add(newUser);
                Entities.SaveChanges();
            }
        }

        public void Add(string UserName, string HASH_PASSWORD_CHECKSUM)
        {
            User newUser = new User();
            newUser.Name = UserName;
            newUser.Password = HASH_PASSWORD_CHECKSUM;

            if (newUser != null)
            {
                Entities.Users.Add(newUser);
                Entities.SaveChanges();
            }
        }

        public User GetUserByName(string userName)
        {
            return Entities.Users.Single(u => u.Name.Equals(userName));
        }

        public bool Login(string userName, string password)
        {
            return Entities.Users.Any(u => u.Name.Equals(userName) && u.Password.Equals(password));
        }

        public bool Exists(string userName)
        {
            var user = Entities.Users.SingleOrDefault(u => u.Name.Equals(userName));

            return user != null;
        }
    }
}
