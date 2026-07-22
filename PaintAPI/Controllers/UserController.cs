using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Paint.Models;
using PaintAPI.Database;
using PaintAPI.DTOs;

namespace PaintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public usersController(UserDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser(CreateUserDto dto)
        {
            var emailExists =await _context.Users.AnyAsync(user => user.Email == dto.Email);
            if (emailExists)
            {
                return BadRequest("This email has Existed.");
            }

            User user = new User(
                orderHistory: new List<Order>(),
                paymentHistory: new List<Payment>(),
                name: dto.Name,
                email: dto.Email,
                phone: dto.Phone
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = new UserResponseDto
            {
                Id = user.Id,
                CreatedDate = user.CreatedDate,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
            };

            return CreatedAtAction(nameof(GetById), new {id = user.Id}, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
        {

            var result = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(new UserResponseDto
            {
                CreatedDate = result.CreatedDate,
                Id = result.Id,
                Name = result.Name,
                Email = result.Email,
                Phone = result.Phone,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _context.Users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                CreatedDate = u.CreatedDate,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
            }).ToListAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfor(CreateUserDto dto, int id)
        {
           var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
           if (user == null)
            {
                return NotFound();
            };

            var emailExists = await _context.Users.AnyAsync(u => u.Id != id && u.Email == dto.Email);
            if (emailExists)
            {
                return BadRequest("This email has existed.");
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Phone = dto.Phone;

            await _context.SaveChangesAsync();

            return Ok(new UserResponseDto
            {
                CreatedDate = user.CreatedDate,
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
