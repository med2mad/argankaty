using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_end_net.Models;

namespace back_end_net.Controllers;

[Route("order_product")]
[ApiController]
public class order_productController(DBContext _context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<order_product>>> Getorder_product() {
        return await _context.order_product.Include(op => op.Product).Include(op => op.Order).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<order_product>> Getorder_product(int id) {
        var order_product = await _context.order_product.FindAsync(id);

        if (order_product == null) { return NotFound(); }

        return order_product;
    }

    [HttpPost]
    public async Task<ActionResult<order_product>> Postorder_product(int orderId, int productId, int quantity) {
        var x = new order_product { Product = _context.products.Find(productId), Order = _context.orders.Find(orderId), quantity = quantity };
        _context.order_product.Add(x);
        await _context.SaveChangesAsync();

        return CreatedAtAction("Getorder_product", new { id = x.id }, x);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Putorder_product(int id, order_product order_product) {
        if (id != order_product.id) {
            return BadRequest();
        }

        _context.Entry(order_product).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!order_productExists(id)) {
                return NotFound();
            }
            else { throw; }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleteorder_product(int id) {
        var order_product = await _context.order_product.FindAsync(id);
        if (order_product == null) {
            return NotFound();
        }

        _context.order_product.Remove(order_product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool order_productExists(int id) {
        return _context.order_product.Any(e => e.id == id);
    }
}