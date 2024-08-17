namespace Mvc_Project.Models.Repositorys
{
    public interface IUserRepository : ICrudOperation<User>
    {
        User GetByEmail(string email);
        List<User> GetUsersByRole(int roleId);
    }
}
