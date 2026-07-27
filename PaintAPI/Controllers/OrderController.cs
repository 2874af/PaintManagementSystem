using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Paint.Models;
using PaintAPI.Database;
using PaintAPI.DTOs;

namespace PaintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ordersController : ControllerBase
    {
        // public static List<Order> _orders = new List<Order>();

        // [HttpGet]
        // public IActionResult GetAllOrders([FromQuery] int pageNumber, [FromQuery] int pageSize)
        // {
        //     int totalCount = _orders.Count;

        //     var result = _orders
        //         .OrderByDescending(order => order.CreatedAt)
        //         .Skip((pageNumber-1) * pageSize)
        //         .Take(pageSize)
        //         .ToList();

        //     return Ok(result);
        // }

        // [HttpGet]
        // public IActionResult GetOrdersByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        // {
        //     var result = _orders
        //         .Where(order => order.TotalPrice > min )
        //         .Where(order => order.TotalPrice < max )
        //         .OrderByDescending(order => order.CreatedAt)
        //         .ToList;

        //     return Ok(result);
        // }

        // [HttpGet("{id}")]
        // public IActionResult GetOrderById (int id)
        // {
        //     var result = _orders
        //         .FirstOrDefault(order => order.Id == id);
            
        //     return Ok(result);
        // }

        // [HttpGet]
        // public IActionResult GetOrdersByUserId ([FromQuery] int userId)
        // {
        //     var result = _orders
        //         .Where(order => order.UserId == userId)
        //         .OrderByDescending(order => order.CreatedAt)
        //         .ToList();

        //     return Ok(result);
        // }

        // [HttpGet]
        // public IActionResult GetLastMonthOrders()
        // {
        //     var now = DateTime.Now;

        //     DateTime firstTimeLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
        //     DateTime lastTimeLastMonth = new DateTime(now.Year, now.Month, 1).AddSeconds(-1);

        //     var result = _orders
        //         .Where(order => order.CreatedAt >= firstTimeLastMonth)
        //         .Where(order => order.CreatedAt <= lastTimeLastMonth)
        //         .OrderByDescending(order => order.CreatedAt)
        //         .ToList();

        //     return Ok(result);
        // }

        // [HttpGet]
        // public IActionResult GetOrdersByDate([FromQuery] DateTime date)
        // {
        //     var result = _orders
        //         .Where(order => order.CreatedAt.Date == date)
        //         .OrderByDescending(order => order.CreatedAt)
        //         .ToList();
            
        //     return Ok(result);
        // }

        private readonly OrderDbContext _context;

        public ordersController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(CreateOrderDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!userExists)
            {
                return NotFound();
            }

            var allPaintId = await _context.Paints.Select(p => p.Id).ToListAsync();
            var paintId = dto.OrderProducts.Select(p => p.PaintId).ToList();

            var allExist = paintId.All(i => allPaintId.Contains(i));
            
            if (!allExist)
            {
                return BadRequest("There is invalid paint product.");
            }

            var allValidQuantity = dto.OrderProducts.All(p => p.Quantity > 0);

            if (!allValidQuantity)
            {
                return BadRequest("There is invalid quantity.");
            }

            var allInStock = dto.OrderProducts.All(p => p.Quantity <= p.Paint.Stock);
            if (!allInStock)
            {
                return BadRequest("Some products are out of stock.");
            }

            var order = new Order
            {
                UserId =  dto.UserId,
                OrderProducts =  dto.OrderProducts

            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = order.Id}, new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                UserId = order.UserId,
                OrderProducts = order.OrderProducts,
                TotalPrice = order.TotalPrice,
            });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                UserId = order.UserId,
                OrderProducts = order.OrderProducts,
                TotalPrice = order.TotalPrice,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _context.Orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                UserId = o.UserId,
                OrderProducts = o.OrderProducts,
                TotalPrice = o.TotalPrice,
            }).ToListAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(OrderResponseDto dto, int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!userExists)
            {
                return NotFound();
            }

            var allPaintId = await _context.Paints.Select(p => p.Id).ToListAsync();
            var paintId = dto.OrderProducts.Select(p => p.PaintId).ToList();

            var allExist = paintId.All(i => allPaintId.Contains(i));
            
            if (!allExist)
            {
                return BadRequest("There is invalid paint product.");
            }

            var allValidQuantity = dto.OrderProducts.All(p => p.Quantity > 0);

            if (!allValidQuantity)
            {
                return BadRequest("There is invalid quantity.");
            }

            var allInStock = dto.OrderProducts.All(p => p.Quantity <= p.Paint.Stock);
            if (!allInStock)
            {
                return BadRequest("Some products are out of stock.");
            }

            order.OrderProducts = dto.OrderProducts;
            order.UserId = dto.UserId;


            await _context.SaveChangesAsync();

            return Ok(new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                UserId = order.UserId,
                OrderProducts = order.OrderProducts,
                TotalPrice = order.TotalPrice,
            });
        }

    }
}
