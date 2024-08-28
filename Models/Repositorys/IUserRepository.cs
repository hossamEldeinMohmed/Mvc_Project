namespace Mvc_Project.Models.Repositorys
{
    public interface IUserRepository : ICrudOperation<User>
    {
        User GetByEmail(string email);
        User GetByName(string name);

        List<User> GetUsersByRole(int roleId);
    }
}
