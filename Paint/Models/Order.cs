using System;
using System.Net.Http.Headers;

namespace Paint.Models;

public class Order
{
    public readonly DateTime CreatedAt;
    public int UserId { get; set; }

    public List<OrderProduct> OrderProducts { get; set; } = new();
    
    public int Id { get; private set; }

    public decimal TotalPrice { get; private set; }

    public Order()
    {
        CreatedAt = DateTime.Now;
        TotalPrice = GetTotalPrice();
    }

    public void DisplayOrder()
    {
        Console.WriteLine($"Create Time: {CreatedAt}");
        Console.WriteLine($"Total Price: {TotalPrice}");

        foreach (var product in OrderProducts)
        {
            Console.WriteLine($"{product.PaintId} with quantity {product.Quantity}");
        }
    }

    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        
        foreach (var product in OrderProducts)
        {
            totalPrice += product.Paint.GetFinalPrice() * product.Quantity;
        }

        return totalPrice;
    }

    public List<PaintProduct> GetMostExpensivePaintProducts()
    {
        decimal biggestPrice = OrderProducts.Max(product => product.Paint.GetFinalPrice());

        return OrderProducts
            .Where(product => product.Paint.GetFinalPrice() == biggestPrice)
            .Select(product => product.Paint)
            .ToList();
    }

    public void RemoveProduct(int productId)
    {
        foreach (var product in OrderProducts)
        {
            if (product.PaintId == productId)
            {
                OrderProducts.Remove(product);
            }
        }
    }

    public List<PaintProduct> FindSpecificPaints(decimal x, decimal y)
    {
        return OrderProducts
            .Where(product => product.Paint.GetFinalPrice() > x)
            .Where(product => product.Paint.GetFinalPrice() < y)
            .Select(product => product.Paint)
            .ToList();
    }

    public Dictionary<PaintProduct, decimal> GetTotalPriceForEachPaint()
    {
        return OrderProducts
            .ToDictionary(
                product => product.Paint,
                product => product.Paint.GetFinalPrice() * product.Quantity
            );
    }
}
