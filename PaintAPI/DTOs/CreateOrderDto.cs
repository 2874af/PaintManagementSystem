using System;
using System.ComponentModel.DataAnnotations;
using Paint.Models;

namespace PaintAPI.DTOs;

public class CreateOrderDto
{
    [Required(ErrorMessage = "Product can not be null.")]
    [MinLength(1, ErrorMessage = "At least including one product.")]
    public List<OrderProduct> OrderProducts { get; set; }

    [Required(ErrorMessage = "UserId can not be null.")]
    public int UserId { get; set; }

}
