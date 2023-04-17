namespace Api.Controllers.ProductController.View;

public class GetProductsRequestView
{
    public required int Offset { get; init; }
    public required int Limit { get; init; }
    public List<string>? Sortings { get; init; }
    public string? ProductType { get; init; }
    public string? Brand { get; init; }
    public string? Searching { get; init; }
}