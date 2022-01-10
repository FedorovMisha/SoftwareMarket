namespace SoftwareMarket.Core.Entity;

public class Sale
{
    public int Id { get; set; }
    
    public int SoftwareId { get; set; }

    public Software Software { get; set; }
    
    public int? DiscountId { get; set; }

    public Discount Discount { get; set; }

    public int BuyerId { get; set; }
    
    public Buyer Buyer { get; set; }
    
    public DateTime OrderDate { get; set; }
}