namespace back_end_net.Models;

public class Product
{
    public int id { get; set; }
    public string nameEN { get; set; } = string.Empty;
    public string nameFR { get; set; } = string.Empty;
    public string nameCH { get; set; } = string.Empty;
    public string descriptionEN { get; set; } = string.Empty;
    public string descriptionFR { get; set; } = string.Empty;
    public string descriptionCH { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public decimal price { get; set; }
    public int promo { get; set; }
    public string photo { get; set; } = string.Empty;
    public int order { get; set; }

    public ICollection<order_product>? order_products { get; set; }
}
