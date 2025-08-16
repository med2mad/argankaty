using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_end_net.Models;

namespace back_end_net.Controllers;

[Route("/")]
[ApiController]
public class ProductsController(DBContext _context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<object>> Getproducts() {
        return new { rows = await _context.products.OrderBy(p => p.order).ToListAsync() };
    }
    [HttpGet("cart/z")]
    public async Task<ActionResult<object>> Getcart([FromQuery] string ids) {
        return new {
            rows = await _context.products
            .FromSqlRaw("SELECT * FROM products WHERE id IN (" + ids + ")")
            .ToListAsync()
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id) {
        var product = await _context.products.FindAsync(id);

        if (product == null) { return NotFound(); }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product) {
        _context.products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product) {
        if (id != product.id) {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!ProductExists(id)) {
                return NotFound();
            }
            else { throw; }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id) {
        var product = await _context.products.FindAsync(id);
        if (product == null) {
            return NotFound();
        }

        _context.products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id) {
        return _context.products.Any(e => e.id == id);
    }
}
