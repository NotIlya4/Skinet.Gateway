namespace Infrastructure.ProductService;

public class ProductFilteringAndSorting
{
    public int Offset { get; }
    public int Limit { get; }
    public string? ProductType { get; }
    public string? Brand { get; }
    public string? Searching { get; }
    public List<string>? Sortings { get; }

    public ProductFilteringAndSorting(int offset, int limit, string? productType, string? brand, string? searching, List<string>? sortings)
    {
        Offset = offset;
        Limit = limit;
        ProductType = productType;
        Brand = brand;
        Searching = searching;
        Sortings = sortings;
    }
}