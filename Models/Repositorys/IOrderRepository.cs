namespace Mvc_Project.Models.Repositorys
{
    public interface IOrderRepository : ICrudOperation<Order>
    {
        List<Order> GetOrdersByUser(int userId);
        List<Order> GetOrdersByStatus(string status);
    }
}
