using System;

namespace Paint.Models;

public class PaintStore
{
    public PaintProduct[] BuyablePaints { get; set; }
    public int[] AvailableQuantities { get; set; }

    public PaintStore(PaintProduct[] buyablePaints, int[] availableQuantities)
    {
        BuyablePaints = buyablePaints;
        AvailableQuantities = availableQuantities;
    }

    public void DisplayStoreInfo()
    {
        for (int i = 0; i < BuyablePaints.Length; i++)
        {
            Console.WriteLine($"{BuyablePaints[i].Name} has {AvailableQuantities[i]} left.");
        }
    }

}
