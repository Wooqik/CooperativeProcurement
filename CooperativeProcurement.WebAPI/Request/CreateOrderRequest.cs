namespace CooperativeProcurement.WebAPI.Request;

public class CreateOrderRequest
{
    public string SupplierName { get; set; } = string.Empty;
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}

public class CreateOrderItemRequest
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}