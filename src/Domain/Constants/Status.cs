namespace Domain.Constants;

public enum OrderStatus
{
    Pending,    // Order is created but payment is not yet completed
    Paid,       // Payment is completed
    Failed,     // Payment failed
    Shipped,    // Order has been shipped
    Delivered,  // Order has been delivered to the customer
    Cancelled   // Order has been cancelled
}

public enum PaymentMethod
{
    CashOnDelivery,
    Card,
}



public enum PaymentStatus
{
    Pending,
    Paid,
    Failed
}
