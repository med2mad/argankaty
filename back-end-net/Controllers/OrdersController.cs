using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_end_net.Models;

namespace back_end_net.Controllers;

[Route("Orders")]
[ApiController]
public class OrdersController(DBContext _context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> Getorders() {
        return await _context.orders.Include(o => o.order_products).ThenInclude(op => op.Product).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id) {
        var order = await _context.orders.Include(o => o.order_products).ThenInclude(op => op.Product).FirstOrDefaultAsync(o => o.id == id);

        if (order == null) { return NotFound(); }

        return order;
    }

    // DTO for incoming order data
    public class OrderCreateDto
    {
        public string name { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string? email { get; set; }
        public string? address { get; set; }
        public string? coupon_code { get; set; }
        public decimal total { get; set; }
        public List<ProductDto> products { get; set; } = new();
    }
    public class ProductDto
    {
        public int id { get; set; }
        public int quantity { get; set; } = 1;
    }

    [HttpPost]
    public async Task<ActionResult<object>> PostOrder([FromBody] OrderCreateDto dto) {
        if (string.IsNullOrWhiteSpace(dto.name) || string.IsNullOrWhiteSpace(dto.phone) || dto.total <= 0) {
            return BadRequest(new { message = "Missing required fields or invalid total." });
        }

        var order = new Order {
            name = dto.name,
            phone = dto.phone,
            email = dto.email ?? string.Empty,
            address = dto.address ?? string.Empty,
            coupon_code = dto.coupon_code ?? string.Empty,
            total = dto.total,
            status = "pending",
            created_at = DateTime.UtcNow
        };
        _context.orders.Add(order);
        await _context.SaveChangesAsync();

        // Attach products if provided
        if (dto.products != null && dto.products.Count > 0) {
            foreach (var p in dto.products) {
                var product = await _context.products.FindAsync(p.id);
                if (product != null) {
                    var op = new order_product {
                        Order = order,
                        Product = product,
                        quantity = p.quantity > 0 ? p.quantity : 1
                    };
                    _context.order_product.Add(op);
                }
            }
            await _context.SaveChangesAsync();
        }

        // Reload order with products for response
        var createdOrder = await _context.orders
            .Include(o => o.order_products)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.id == order.id);

        return CreatedAtAction("GetOrder", new { id = order.id }, new { order = createdOrder });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order) {
        if (id != order.id) {
            return BadRequest();
        }

        _context.Entry(order).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!OrderExists(id)) {
                return NotFound();
            }
            else { throw; }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id) {
        var order = await _context.orders.FindAsync(id);
        if (order == null) {
            return NotFound();
        }

        _context.orders.Remove(order);
        await _context.SaveChangesAsync();

        return new JsonResult(new { message = "Order deleted successfully" });
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> PatchOrderStatus([FromRoute] int id, [FromBody] StatusDto dto) {
        if (dto == null || string.IsNullOrWhiteSpace(dto.status)) {
            return BadRequest(new { message = "missing status." });
        }
        var order = await _context.orders.FindAsync(id);
        if (order == null) {
            return NotFound();
        }
        order.status = dto.status;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    public class StatusDto
    {
        public string status { get; set; } = string.Empty;
    }

    private bool OrderExists(int id) {
        return _context.orders.Any(e => e.id == id);
    }
}
