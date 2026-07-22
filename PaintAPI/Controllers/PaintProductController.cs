using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paint;
using Paint.Models;
using PaintAPI.Database;
using PaintAPI.DTOs;

namespace PaintAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class paintproductsController : ControllerBase
    {
        private readonly PaintDbContext _context;

        public paintproductsController (PaintDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPaint(CreatePaintDto dto)
        {
            var nameExists = await _context.Paints.AnyAsync(p => p.Name == dto.Name);

            if (nameExists)
            {
                return BadRequest("This paint Product has existed.");
            }

            var paint = new PaintProduct
            {
                Name = dto.Name,
                Type = dto.Type,
                Specification = dto.Specification,
                Price = dto.Price,
                Brand = dto.Brand,
                Stock = dto.Stock,
            };

            _context.Paints.Add(paint);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = paint.Id}, new PaintResponseDto
            {
                TaxRate = paint.TaxRate,
                CreatedDate = paint.CreatedDate,
                DefaultDiscount = PaintProduct.DefaultDiscount,
                Id = paint.Id,
                Name = paint.Name,
                Type = paint.Type,
                Specification = paint.Specification,
                Price = paint.Price,
                Brand = paint.Brand,
                Stock = paint.Stock,

            });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paint = await _context.Paints.FirstOrDefaultAsync(p => p.Id == id);

            if (paint == null)
            {
                return NotFound();
            }

            var result = new PaintResponseDto
            {
                TaxRate = paint.TaxRate,
                CreatedDate = paint.CreatedDate,
                DefaultDiscount = PaintProduct.DefaultDiscount,
                Id = paint.Id,
                Name = paint.Name,
                Type = paint.Type,
                Specification = paint.Specification,
                Price = paint.Price,
                Brand = paint.Brand,
                Stock = paint.Stock,
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaints()
        {
            var result = await _context.Paints.Select(paint => new PaintResponseDto
            {
                TaxRate = paint.TaxRate,
                CreatedDate = paint.CreatedDate,
                DefaultDiscount = PaintProduct.DefaultDiscount,
                Id = paint.Id,
                Name = paint.Name,
                Type = paint.Type,
                Specification = paint.Specification,
                Price = paint.Price,
                Brand = paint.Brand,
                Stock = paint.Stock,
            }).ToListAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaint(CreatePaintDto dto, int id)
        {
            var paint = await _context.Paints.FirstOrDefaultAsync(p => p.Id == id);

            if (paint == null)
            {
                return NotFound();
            }

            var nameExists = await _context.Paints.AnyAsync(p => p.Name == dto.Name);

            if (nameExists)
            {
                return BadRequest("This paint Product has existed.");
            }

            paint.Name = dto.Name;
            paint.Type = dto.Type;
            paint.Specification = dto.Specification;
            paint.Price = dto.Price;
            paint.Brand = dto.Brand;
            paint.Stock = dto.Stock;

            await _context.SaveChangesAsync();

            return Ok(new PaintResponseDto
            {
                TaxRate = paint.TaxRate,
                CreatedDate = paint.CreatedDate,
                DefaultDiscount = PaintProduct.DefaultDiscount,
                Id = paint.Id,
                Name = paint.Name,
                Type = paint.Type,
                Specification = paint.Specification,
                Price = paint.Price,
                Brand = paint.Brand,
                Stock = paint.Stock,
            });
        }

    }
}