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
        decimal mostExpensivePrice = Products[0].GetFinalPrice();

        foreach (PaintProduct product in Products)
        {
            if (product.GetFinalPrice() > mostExpensivePrice)
            {
                mostExpensivePrice = product.GetFinalPrice();
            }
        }

        List<PaintProduct> MostExpensivePaints = [];

        foreach(PaintProduct product in Products)
        {
            if (product.GetFinalPrice() == mostExpensivePrice)
            {
                MostExpensivePaints.Add(product);
            }
        }

        return MostExpensivePaints;
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
        List<PaintProduct> specificPaints = [];
        foreach(PaintProduct product in Products)
        {
            if (x < product.GetFinalPrice() && product.GetFinalPrice() < y)
            {
                specificPaints.Add(product);
            }
        }

        return specificPaints;
    }

    public Dictionary<int, decimal> GetTotalPriceForEachPaint()
    {
        Dictionary<int, decimal> totalPriceForEachPaint = new Dictionary<int, decimal>();

        for (int i = 0; i < Products.Count; i++)
        {
            totalPriceForEachPaint[Products[i].Id] = Products[i].GetFinalPrice() * Quantities[i];
        }

        return totalPriceForEachPaint;
    }
}
