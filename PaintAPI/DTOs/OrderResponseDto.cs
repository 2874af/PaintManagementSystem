using System;
using Paint.Models;

namespace PaintAPI.DTOs;

public class OrderResponseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public decimal TotalPrice { get; set; }
}
