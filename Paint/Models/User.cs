using System;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Paint.Models;

public class User
{
    public readonly DateTime CreatedDate;
    public List<Order> OrderHistory { get; set; } = new List<Order>();
    public List<Payment> PaymentHistory { get; set; } = new List<Payment>();
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }


    public List<Order> GetMostExpensiveOrders()
    {
        decimal mostExpensiveOrderPrice = OrderHistory.Max(order => order.TotalPrice);

        return OrderHistory
            .Where(order => order.TotalPrice == mostExpensiveOrderPrice)
            .ToList();
    }

    public Order GetLatestOrder()
    {
        return OrderHistory
            .OrderByDescending(order => order.CreatedAt)
            .First();
    }

    public List<Payment> GetLowestPayment()
    {
        decimal lowestPrice = PaymentHistory.Min(payment => payment.PaymentAmount);
        
        return PaymentHistory
            .Where(payment => payment.PaymentAmount == lowestPrice)
            .ToList();
    }

    public Payment LatestPayment()
    {
        return PaymentHistory
            .OrderByDescending(payment => payment.CreatedAt)
            .First();
    }

    public List<Payment> GetMoreThanTenPayment()
    {

        return PaymentHistory
            .Where(payment => payment.PaymentAmount > 10)
            .ToList();
    }
}
