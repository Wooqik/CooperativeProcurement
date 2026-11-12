using CooperativeProcurement.Core.Models;

namespace CooperativeProcurement.WebAPI.Request;

public class UpdateStatusRequest
{
    public OrderStatus Status { get; set; }
}