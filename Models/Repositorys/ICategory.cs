namespace Mvc_Project.Models.Repositorys
{
    public interface ICategory : ICrudOperation<Category>
    {
        public Category GetByName(string name);
    }
}
