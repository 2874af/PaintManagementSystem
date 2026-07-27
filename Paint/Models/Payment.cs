using System;
using Paint.Enums;

namespace Paint.Models;

public class Payment
{
    public int Id { get; set; }
    public readonly DateTime CreatedAt;
    public PaymentStatus Status { get; set; }
    public decimal PaymentAmount { get; set; }
    public Order PaymentOrder { get; set; }
    public User PaymentUser { get; set; }

}
