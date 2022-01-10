namespace SoftwareMarket.Core.Entity;

public class Buyer
{
    public int Id { get; set; }
    
    public int BuyerType { get; set; }
    
    public BuyerType Type { get; set; }
    
    public string Phone { get; set; }
}