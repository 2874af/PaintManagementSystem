using System;

namespace Paint.Models;

public class OrderProduct
{
    public int Id { get; set; }
    public readonly int OrderId;
    public Order Order { get; set; }
    public int PaintId { get; set; }
    public PaintProduct Paint { get; set; }
    public int Quantity { get; set; }



}
