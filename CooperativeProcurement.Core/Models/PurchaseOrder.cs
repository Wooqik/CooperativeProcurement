namespace CooperativeProcurement.Core.Models;

public class PurchaseOrder
{
    public Guid ID { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.New;

    public decimal CalculateTotal()
    {
        TotalAmount = Items.Sum(item => item.TotalPrice);
        return TotalAmount;
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Нельзя изменить статус отменённого заказа.");
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Нельзя изменить статус доставленного заказа.");
        Status = newStatus;
    }
}