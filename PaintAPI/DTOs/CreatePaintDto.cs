using System;
using System.ComponentModel.DataAnnotations;
using Paint.Enums;
using Paint.Models;

namespace PaintAPI.DTOs;

public class CreatePaintDto
{
    [Required(ErrorMessage = "Name can not be empty")]
    public string Name { get; set; }

    [EnumDataType(typeof(PaintType), ErrorMessage = "Invalid paint type.")]
    public PaintType Type { get; set; }

    [Required(ErrorMessage = "Specification can not be empty.")]
    public PaintSpecification Specification { get; set; }
    
    [Range(0.001, double.MaxValue, ErrorMessage = "Price must be more than 0.")]
    public decimal Price { get; set; }

    [EnumDataType(typeof(PaintBrand), ErrorMessage = "Invalid brand name.")]
    public PaintBrand Brand { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Invilide stock.")]
    public int Stock { get; set; }
}
