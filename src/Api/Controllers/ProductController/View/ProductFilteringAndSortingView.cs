namespace Api.Controllers.ProductController.View;

public class ProductFilteringAndSortingView
{
    public int Offset { get; set; }
    public int Limit { get; set;  }
    public string? ProductType { get; set; }
    public string? Brand { get; set; }
    public string? Searching { get; set; }
    public List<string>? Sortings { get; set; }

    public ProductFilteringAndSortingView(int offset, int limit, string? productType = null, string? brand = null, string? searching = null, List<string>? sortings = null)
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