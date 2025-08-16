using Microsoft.EntityFrameworkCore;

namespace back_end_net.Models;

public class order_product
{
    public int id { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Order? Order { get; set; }
    public Product? Product { get; set; }

    public int quantity { get; set; }
    public DateTime created_at { get; set; } = DateTime.Now;
    public DateTime updated_at { get; set; } = DateTime.Now;
}
