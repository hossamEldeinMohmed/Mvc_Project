namespace Mvc_Project.Models.Repositorys;
public class ProductRepository : IProductRepository
{
    private Context context;
    public ProductRepository(Context context)
    {
        this.context = context;
    }
    public void Add(Proudect newObjectModel)
    {
        context.Proudects.Add(newObjectModel);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        Proudect Pro = GetByID(id);
        if (Pro != null)
        {
            context.Remove(Pro);
            context.SaveChanges();
        }
    }

    public List<Proudect> GetAll()
    {
        return context.Proudects

           .ToList();
    }

    public Proudect GetByID(int id)
    {
        return context.Proudects.FirstOrDefault(i => i.Id == id);

    }

    public void Update(Proudect updatedObjectModel)
    {
        Proudect existingProudect = context.Proudects.FirstOrDefault(i => i.Id == updatedObjectModel.Id);

        if (existingProudect != null)
        {

            existingProudect.Name = updatedObjectModel.Name;
            existingProudect.Price = updatedObjectModel.Price;
            existingProudect.Image = updatedObjectModel.Image;
            existingProudect.Description = updatedObjectModel.Description;
            existingProudect.Category = updatedObjectModel.Category;
            existingProudect.DateAdded = updatedObjectModel.DateAdded;
            existingProudect.IsSold = updatedObjectModel.IsSold;

            context.SaveChanges();
        }
    }
}

