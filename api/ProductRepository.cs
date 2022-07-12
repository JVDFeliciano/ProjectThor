public static class ProductRepository 
{

    public static void Init(IConfiguration configuration) {
        var  products = configuration.GetSection("Products").Get<List<Product>>();
        Products = products;
    }


    public static List<Product> Products { get; set; }

    public static void Add(Product product) 
    {
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(String code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product)
    {
        Products.Remove(product);
    }
}