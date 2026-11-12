using CooperativeProcurement.Core.Interfaces;
using CooperativeProcurement.Core.Models;

namespace CooperativeProcurement.Core.Services;

public class PurchaseOrderService
{
    private readonly IPurchaseOrderRepository _repository;

    public PurchaseOrderService(IPurchaseOrderRepository repository)
    {
        _repository = repository;
    }

    public Guid CreateOrder(string supplierName, List<CreateOrderItem> items)
    {
        var order = new PurchaseOrder
        {
            ID = Guid.NewGuid(),
            SupplierName = supplierName,
            Items = items.Select(i => new OrderItem
            {
                ID = Guid.NewGuid(),
                Name = i.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        order.CalculateTotal();
        _repository.Save(order);
        return order.ID;
    }

    public void UpdateStatus(Guid id, OrderStatus status)
    {
        var order = _repository.GetById(id);
        if (order == null)
            throw new KeyNotFoundException($"Заказ с ID {id} не найден.");

        order.ChangeStatus(status);
        _repository.Save(order);
    }
}

public class CreateOrderItem
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}