using Assignment.Domain.Entities;
using Assignment.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Persistence.Seed;

public static class Seed
{
    // TO_DO seed could be better
    public static async Task SeedDeveloperData(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        var product1 = await context.Products.FirstOrDefaultAsync(x => x.Name == "Product1");
        if (product1 == null)
        {
            product1 = new Product() { Name = "Product1", PhotoUrl = "url1.", Description = "Mock description of product here", Value = 15, Size = 20 };
            await context.Products.AddAsync(product1);
        }

        var product2 = await context.Products.FirstOrDefaultAsync(x => x.Name == "Product1");
        if (product2 == null)
        {
            product2 = new Product() { Name = "Product two", PhotoUrl = "url1.wwww.gttps", Description = "Another mock description of product here", Value = 156, Size = 30 };
            await context.Products.AddAsync(product2);
        }

        await context.SaveChangesAsync();

    }
}