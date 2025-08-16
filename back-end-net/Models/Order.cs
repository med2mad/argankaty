using Microsoft.EntityFrameworkCore;

namespace back_end_net.Models;

public class Order
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string phone { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string address { get; set; } = string.Empty;
    public string? coupon_code { get; set; }
    public decimal coupn_value { get; set; }
    public string status { get; set; } = "pending";
    public DateTime created_at { get; set; } = DateTime.Now;
    public DateTime updated_at { get; set; } = DateTime.Now;
    public decimal total { get; set; }

    public ICollection<order_product> order_products { get; set; } = null!;
}
