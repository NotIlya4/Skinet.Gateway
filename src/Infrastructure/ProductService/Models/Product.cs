namespace Infrastructure.ProductService.Models;

public record Product
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public Uri PictureUrl { get; }
    public decimal Price { get; }
    public string Brand { get; }
    public string ProductType { get; }

    public Product(Guid id, string name, string description, Uri pictureUrl, decimal price, string brand, string productType)
    {
        Id = id;
        Name = name;
        Description = description;
        PictureUrl = pictureUrl;
        Price = price;
        Brand = brand;
        ProductType = productType;
    }
}