namespace Infrastructure.ProductService;

public class ProductFilteringAndSortingView
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public string? ProductType { get; set; }
    public string? Brand { get; set; }
    public string? Searching { get; set; }
    public List<string>? Sortings { get; set; }

    public ProductFilteringAndSortingView(int offset, int limit, string? productType, string? brand, string? searching, List<string>? sortings)
    {
        Offset = offset;
        Limit = limit;
        ProductType = productType;
        Brand = brand;
        Searching = searching;
        Sortings = sortings;
    }
    
    public ProductFilteringAndSortingView()
    {
        
    }
}