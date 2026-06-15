using System;
using Paint.Enums;

namespace Paint.Models;

public class Payment
{
    public static int nextId = 1;
    public readonly int Id;
    public readonly DateTime CreatedAt;
    public PaymentStatus Status { get; set; }
    public decimal PaymentAmount { get; set; }
    public Order PaymentOrder { get; set; }
    public User PaymentUser { get; set; }

    public Payment(Order order, decimal amount, User user)
    {
        Id = nextId;
        nextId ++;

        CreatedAt = DateTime.Now;
        Status = PaymentStatus.Pending;
        PaymentAmount = amount;
        PaymentOrder = order;
        PaymentUser = user;
    }
}
