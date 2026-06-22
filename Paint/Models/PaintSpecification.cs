using System;

namespace Paint.Models;

public class PaintSpecification
{
    public string Color { get; set; }
    public int SizeInLiters { get; set; }
    public int Id { get; set; }

    public PaintSpecification()
    {
        Color = "white";
        SizeInLiters = 3;
    }

    public void DisplaySpecification()
    {
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Size in Liters: {SizeInLiters}");
    }
}
