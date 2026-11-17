using LinqPractice.Data;
using LinqPractice.Models;

using var db = new AppDbContext();

// Create database and table if they don't exist
db.Database.EnsureCreated();

// Insert sample data only once
if (!db.Products.Any())
{
    db.Products.AddRange(
        new Product { Name = "Laptop", Category = "Electronics", Price = 65000, Stock = 10 },
        new Product { Name = "Mouse", Category = "Electronics", Price = 800, Stock = 50 },
        new Product { Name = "Keyboard", Category = "Electronics", Price = 1500, Stock = 25 },
        new Product { Name = "Chair", Category = "Furniture", Price = 5000, Stock = 8 },
        new Product { Name = "Desk", Category = "Furniture", Price = 9000, Stock = 5 },
        new Product { Name = "Notebook", Category = "Stationery", Price = 100, Stock = 100 }
    );

    db.SaveChanges();
}

// Fetch products
var products = db.Products.ToList();

Console.WriteLine("All Products:");

foreach (var product in products)
{
    Console.WriteLine(
        $"{product.Name} - {product.Category} - ₹{product.Price}"
    );
}