using Inspiration.Repository.DataAccessObjects;
using Inspiration.Repository.Interfaces;
namespace Inspiration.Repository;
public class UserRepository : IUserRepository
{
    public void Create(UserDao user)
    {
        MockDB.Users.Add(user);
    }

    public UserDao? Get(Guid id)
    {
        return MockDB.Users.FirstOrDefault(x => x.Id == id);
    }

    public UserDao? GetUserByEmail(string email)
    {
        return MockDB.Users.SingleOrDefault(u => u.Email == email);
    }
}
