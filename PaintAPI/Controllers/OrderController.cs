using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Paint.Models;

namespace PaintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public static List<Order> _orders = new List<Order>();

        [HttpGet]
        public IActionResult GetAllOrders([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            int totalCount = _orders.Count;

            var result = _orders
                .OrderByDescending(order => order.CreatedAt)
                .Skip((pageNumber-1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetOrdersByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var result = _orders
                .Where(order => order.TotalPrice > min )
                .Where(order => order.TotalPrice < max )
                .OrderByDescending(order => order.CreatedAt)
                .ToList;

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById (int id)
        {
            var result = _orders
                .FirstOrDefault(order => order.Id == id);
            
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetOrdersByUserId ([FromQuery] int userId)
        {
            var result = _orders
                .Where(order => order.UserId == userId)
                .OrderByDescending(order => order.CreatedAt)
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetLastMonthOrders()
        {
            var now = DateTime.Now;

            DateTime firstTimeLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            DateTime lastTimeLastMonth = new DateTime(now.Year, now.Month, 1).AddSeconds(-1);

            var result = _orders
                .Where(order => order.CreatedAt >= firstTimeLastMonth)
                .Where(order => order.CreatedAt <= lastTimeLastMonth)
                .OrderByDescending(order => order.CreatedAt)
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetOrdersByDate([FromQuery] DateTime date)
        {
            var result = _orders
                .Where(order => order.CreatedAt.Date == date)
                .OrderByDescending(order => order.CreatedAt)
                .ToList();
            
            return Ok(result);
        }
    }
}
