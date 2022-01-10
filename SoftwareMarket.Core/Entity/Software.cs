namespace SoftwareMarket.Core.Entity;

public class Software
{
    public int Id { get; set; }
    
    public int TypeId { get; set; }
    
    public SoftwareType Type { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
}