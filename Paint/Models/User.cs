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
        decimal mostExpensiveOrderPrice = OrderHistory[0].TotalPrice;

        foreach (Order order in OrderHistory)
        {
            if (order.TotalPrice > mostExpensiveOrderPrice)
            {
                mostExpensiveOrderPrice = order.TotalPrice;
            }
        }

        List<Order> mostExpensiveOrders = [];

        foreach (Order order in OrderHistory)
        {
            if (order.TotalPrice == mostExpensiveOrderPrice)
            {
                mostExpensiveOrders.Add(order);
            }
        }

        return mostExpensiveOrders;
    }

    public Order GetLatestOrder()
    {
        Order latestOrder = OrderHistory[0];

        foreach(Order order in OrderHistory)
        {
            if (order.CreatedAt > latestOrder.CreatedAt)
            {
                latestOrder = order;
            }
        }

        return latestOrder;
    }

    public List<Payment> GetLowestPayment()
    {
        decimal lowestPaymentPrice = PaymentHistory[0].PaymentAmount;

        foreach (Payment payment in PaymentHistory)
        {
            if (payment.PaymentAmount < lowestPaymentPrice)
            {
                lowestPaymentPrice = payment.PaymentAmount;
            }
        }

        List<Payment> lowestPayments = [];

        foreach (Payment payment in PaymentHistory)
        {
            if (payment.PaymentAmount == lowestPaymentPrice)
            {
                lowestPayments.Add(payment);
            }
        }

        return lowestPayments;
    }

    public Payment LatestPayment()
    {
        Payment latestPayment = PaymentHistory[0];

        foreach(Payment payment in PaymentHistory)
        {
            if (payment.CreatedAt > latestPayment.CreatedAt)
            {
                latestPayment = payment;
            }
        }

        return latestPayment;
    }

    public List<Payment> GetMoreThanTenPayment()
    {

        List<Payment> MoreThanTenPayments = [];

        foreach (Payment payment in PaymentHistory)
        {
            if (payment.PaymentAmount > 10 )
            {
                MoreThanTenPayments.Add(payment);
            }
        }

        return MoreThanTenPayments;
    }
}
