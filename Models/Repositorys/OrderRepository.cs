using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mvc_Project.Models.Repositorys
{
    public class OrderRepository : IOrderRepository
    {
        private  Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShoppingCart)
                .ThenInclude(sc => sc.CartItems)
                .ToList();
        }

        public Order GetByID(int id)
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShoppingCart)
                .ThenInclude(sc => sc.CartItems)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Add(Order newOrder)
        {
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }

        public void Update(Order updatedOrder)
        {
            _context.Orders.Update(updatedOrder);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public List<Order> GetOrdersByUser(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.ShoppingCart)
                .ThenInclude(sc => sc.CartItems)
                .ToList();
        }

        public List<Order> GetOrdersByStatus(string status)
        {
            return _context.Orders
                .Where(o => o.Status == status)
                .Include(o => o.ShoppingCart)
                .ThenInclude(sc => sc.CartItems)
                .ToList();
        }
    }
}
