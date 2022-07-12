using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database: SqlServer"]);


var app = builder.Build();
var configuration = app.Configuration;
ProductRepository.Init(configuration);

app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) => {
    // var category = context.Categories.Where(c => c.Id == productRequest.CategoryId).First();
    // var product = new Product
    // {
    //     Code = productRequest.Code,
    //     Name = productRequest.Name,
    //     Description = productRequest.Description,
    //     Category = category
    // };
    // context.Products.Add(product);
    // return Results.Created($"/products/{product.Id}", product.Id);
});

app.MapGet("/products/{code}", ([FromRoute] string Code) => {
    var product = ProductRepository.GetBy(Code);
    if (product != null)
        return Results.Ok(product);
     return Results.NotFound();
});

app.MapGet("/products", (HttpRequest request) => {
    return request.Headers["product-code"].ToString();
});

app.MapPut("/products", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok();
});

app.MapDelete("/products/{code}", ([FromRoute] string Code) => {
    var productSaved = ProductRepository.GetBy(Code);
    ProductRepository.Remove(productSaved);
    return Results.Ok();
});

if(app.Environment.IsStaging())
    app.MapGet("/configuration/database", (IConfiguration configuration) => {
        return Results.Ok(configuration["database:connection"]);
    });
app.Run();
