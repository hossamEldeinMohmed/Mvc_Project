using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;

namespace Mvc_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Add(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public IActionResult Edit(int id)
        {
            var order = _orderRepository.GetByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _orderRepository.Update(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
