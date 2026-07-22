using System;
using Paint.Enums;
using Paint.Models;

namespace PaintAPI.DTOs;

public class PaintResponseDto
{
    public decimal TaxRate { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal DefaultDiscount { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public PaintType Type { get; set; }
    public PaintSpecification Specification { get; set; }
    public decimal Price { get; set; }
    public PaintBrand Brand { get; set; }
    public int Stock { get; set; }
}
