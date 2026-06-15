using System;
using System.Dynamic;
using System.Linq.Expressions;

namespace Paint.Models;

public class User
{
    public List<Order> OrderHistory { get; set; }
    public List<Payment> PaymentHistory { get; set; }

    public User(List<Order> orderHistory, List<Payment> paymentHistory)
    {
        OrderHistory = orderHistory;
        PaymentHistory = paymentHistory;
    }

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
