using System;

namespace Paint.Models;

public class Order
{
    public readonly DateTime CreatedAt;

    public List<PaintProduct> Products { get; set; }
    public List<int> Quantities { get; set; }
    public decimal TotalPrice { get; set; }

    public Order(List<PaintProduct> products, List<int> quantities)
    {
        CreatedAt = DateTime.Now;
        Products = products;
        Quantities = quantities;
        TotalPrice = GetTotalPrice();
    }

    public void DisplayOrder()
    {
        Console.WriteLine($"Create Time: {CreatedAt}");
        Console.WriteLine($"Total Price: {TotalPrice}");

        for (int i = 0; i < Products.Count; i++)
        {
            Console.WriteLine($"{Products[i].Name} with quantity {Quantities[i]}");
        }
    }

    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        for (int i = 0; i < Products.Count; i++)
        {
            totalPrice += Products[i].GetFinalPrice() * Quantities[i];
        }

        return totalPrice;
    }

    public List<PaintProduct> GetMostExpensivePaintProducts()
    {
        decimal biggestPrice = Products.Max(product => product.GetFinalPrice());

        return Products
            .Where(product => product.GetFinalPrice() == biggestPrice)
            .ToList();
    }

    public void RemoveProduct(int productId)
    {
        for (int i = 0; i < Products.Count; i++)
        {
            if (Products[i].Id == productId)
            {
                Products.Remove(Products[i]);
                Quantities.Remove(Quantities[i]);
                break;
            }
        }
    }

    public List<PaintProduct> FindSpecificPaints(decimal x, decimal y)
    {
        return Products
            .Where(product => product.GetFinalPrice() > x)
            .Where(product => product.Price < y)
            .ToList();
    }

    public Dictionary<int, decimal> GetTotalPriceForEachPaint()
    {
        return Products
            .Select((product, index) => new
            {
                Id = product.Id,
                TotalPrice = product.GetFinalPrice() * Quantities[index]
            } )
            .ToDictionary(x => x.Id, x => x.TotalPrice);
    }
}
