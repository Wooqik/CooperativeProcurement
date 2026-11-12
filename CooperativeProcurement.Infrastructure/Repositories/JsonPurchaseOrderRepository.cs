using System.Text.Json;
using CooperativeProcurement.Core.Interfaces;
using CooperativeProcurement.Core.Models;

namespace CooperativeProcurement.Infrastructure.Repositories;

public class JsonPurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly string _filePath;

    public JsonPurchaseOrderRepository(string filePath = "orders.json")
    {
        _filePath = filePath;
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public PurchaseOrder? GetById(Guid id)
    {
        var orders = GetAll();
        return orders.FirstOrDefault(o => o.ID == id);
    }

    public void Save(PurchaseOrder order)
    {
        var orders = GetAll();
        var existingIndex = orders.FindIndex(o => o.ID == order.ID);

        if (existingIndex >= 0)
            orders[existingIndex] = order;
        else
            orders.Add(order);

        var json = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<PurchaseOrder> GetAll()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<PurchaseOrder>>(json) ?? new List<PurchaseOrder>();
    }
}