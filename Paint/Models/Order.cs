using System;

namespace Paint.Models;

public class Order
{
    public readonly DateTime CreatedAt;

    public PaintProduct Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public Order(PaintProduct paintProduct, int quantity)
    {
        CreatedAt = DateTime.Now;
        Product = paintProduct;
        TotalPrice = GetTotalPrice();
    }

    public void DisplayOrder()
    {
        Console.WriteLine($"Create Time: {CreatedAt}");
        Console.WriteLine($"Product Name: {Product.Name}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Total Price: {TotalPrice}");
    }

    public decimal GetTotalPrice()
    {
        
    }
}
