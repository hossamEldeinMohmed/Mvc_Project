namespace Mvc_Project.Models.Repositorys
{
    public interface ICrudOperation<T>
    {
        public List<T> GetAll();
        public T GetByID(int id);
        public void Add(T newObjectModel);

        public void Update(T updatedObjectModel);
        public void Delete(int id);
    }
}
