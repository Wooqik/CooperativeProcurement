using Microsoft.AspNetCore.Mvc;
using CooperativeProcurement.Core.Services;
using CooperativeProcurement.WebAPI.Request;

namespace CooperativeProcurement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseOrderController : ControllerBase
{
    private readonly PurchaseOrderService _service;

    public PurchaseOrderController(PurchaseOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult PostOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            var items = request.Items.Select(i => new CreateOrderItem
            {
                Name = i.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            var id = _service.CreateOrder(request.SupplierName, items);
            return Ok(new { Id = id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPatch("{id:guid}/status")]
    public IActionResult PatchStatus(Guid id, [FromBody] UpdateStatusRequest request)
    {
        try
        {
            _service.UpdateStatus(id, request.Status);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}