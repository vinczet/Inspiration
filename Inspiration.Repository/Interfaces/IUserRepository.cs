using Inspiration.Repository.DataAccessObjects;

namespace Inspiration.Repository.Interfaces;

public interface IUserRepository
{
    public UserDao? Get(Guid id);
    public void Create(UserDao user);
    public UserDao? GetUserByEmail(string email);
}
