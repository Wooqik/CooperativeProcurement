using CooperativeProcurement.Core.Models;

namespace CooperativeProcurement.Core.Interfaces;

public interface IPurchaseOrderRepository
{
    PurchaseOrder GetById(Guid id);
    void Save(PurchaseOrder order);
    List<PurchaseOrder> GetAll();
}