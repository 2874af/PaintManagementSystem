using System;
using System.Net.Http.Headers;

namespace Paint.Models;

public class Order
{
    public readonly DateTime CreatedAt;
    public readonly int UserId;

    public Dictionary<PaintProduct, int> Products { get; set; }
    
    public int Id { get; private set; }

    public decimal TotalPrice { get; private set; }

    public Order(Dictionary<PaintProduct, int> products, int userId)
    {
        CreatedAt = DateTime.Now;
        Products = products;
        TotalPrice = GetTotalPrice();
        UserId = userId;
    }

    public void DisplayOrder()
    {
        Console.WriteLine($"Create Time: {CreatedAt}");
        Console.WriteLine($"Total Price: {TotalPrice}");

        foreach (var product in Products)
        {
            Console.WriteLine($"{product.Key} with quantity {product.Value}");
        }
    }

    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        
        foreach (var product in Products)
        {
            totalPrice += product.Key.GetFinalPrice() * product.Value;
        }

        return totalPrice;
    }

    public List<PaintProduct> GetMostExpensivePaintProducts()
    {
        decimal biggestPrice = Products.Max(product => product.Key.GetFinalPrice());

        return Products
            .Where(product => product.Key.GetFinalPrice() == biggestPrice)
            .Select(product => product.Key)
            .ToList();
    }

    public void RemoveProduct(int productId)
    {
        foreach (var product in Products)
        {
            if (product.Key.Id == productId)
            {
                Products.Remove(product.Key);
            }
        }
    }

    public List<PaintProduct> FindSpecificPaints(decimal x, decimal y)
    {
        return Products
            .Where(product => product.Key.GetFinalPrice() > x)
            .Where(product => product.Key.Price < y)
            .Select(product => product.Key)
            .ToList();
    }

    public Dictionary<PaintProduct, decimal> GetTotalPriceForEachPaint()
    {
        return Products
            .ToDictionary(
                product => product.Key,
                product => product.Key.GetFinalPrice() * product.Value
            );
    }
}
