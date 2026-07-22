using System;

namespace Paint.Models;

public class OrderProduct
{
    public readonly int OrderId;
    public Order Order { get; set; }
    public int PaintId { get; set; }
    public PaintProduct Paint { get; set; }
    public int Quantity { get; set; }


    public OrderProduct(PaintProduct paint, int paintId, int quantity, int orderId, Order order)
    {
        PaintId = paintId;
        Paint = paint;
        Quantity = quantity;
        OrderId = orderId;
        Order = order;
    }
}
