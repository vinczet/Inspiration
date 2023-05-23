namespace Inspiration.Repository.DataAccessObjects;

public class UserDao
{
    public Guid Id { get; set; }    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

    public UserDao(Guid id, string name, string email, string password, bool isAdmin)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        IsAdmin = isAdmin;
    }
}
