using MinimalAPIWithAuthentication.Entities;
using MinimalAPIWithAuthentication.Enums;

namespace MinimalAPIWithAuthentication.Repository
{
    public class Repository : IRepository
    {
        private static List<User> Users = new List<User>()
        {
            new User {Name = "Tasbeeh", Password = "654321", Role = Role.Admin},
            new User {Name = "Israa", Password = "123456", Role = Role.User},
            new User {Name = "Danya", Password = "123456", Role = Role.User},
            new User {Name = "Ayah", Password = "123456", Role = Role.User}
        };

        public User? Find(string name, string password)
        {
            return Users.FirstOrDefault(u => u.Name == name && u.Password == password);
        }
    }
}
