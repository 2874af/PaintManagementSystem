using Paint.Enums;
using Paint.Interfaces;

namespace Paint.Models;

public class PaintProduct: IBuyable
{
    public readonly decimal TaxRate;
    public const decimal DefaultDiscount = 0.05m;

    public static int nextId = 1;
    public readonly int Id;
    public string Name { get; set; }
    public PaintType Type { get; set; }
    public PaintSpecification Specification { get; set; }
    public decimal Price { get; set; }
    public PaintBrand Brand { get; set; }

    public PaintProduct()
    {
        Name = "paint";
        Type = PaintType.Matte;
        Specification = new PaintSpecification();
        Price = 80;  
        TaxRate = 0.1m;
        Brand = PaintBrand.Dulux;

        Id = nextId;
        nextId ++;

    }

    public decimal GetFinalPrice()
    {   
        decimal finalPrice = Price * (1 + TaxRate - DefaultDiscount);
        return finalPrice;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Type: {Type}");
        Console.WriteLine($"Color: {Specification.Color}");
        Console.WriteLine($"Size in Liters: {Specification.SizeInLiters}");
        Console.WriteLine($"Price: {Price}");
    }

    public decimal GetMaxDiscount(int rate, bool isOverridable)
    {
        if (isOverridable)
        {
            return rate;
        } else
        {
            return DefaultDiscount;
        }
    }
}
