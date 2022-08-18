using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using CategoriesProductsLibrary.Data;
using CategoriesProductsLibrary.models;

namespace CategoriesProductsLibrary.Classes
{
    public class BogusOperations
    {
        public static List<Categories> CategoriesList(int count = 4)
        {
            var fake = new Faker<Categories>()
                .RuleFor(c => c.CategoryName, f => f.Commerce.Categories(1)[0]);

            return fake.Generate(count);
        }

        public static List<Products> ProductsList(int productCount, int categoryCount)
        {
            Faker<Products> fake = new Faker<Products>()
                .RuleFor(p => p.ProductName, f => f.Address.County())
                .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(10,45))
                .RuleFor(p => p.UnitsInStock, f => f.Random.Short(1,5))
                .RuleFor(p => p.CategoryId, f => f.Random.Int(1,categoryCount));

            return fake.Generate(productCount);
        }

        public static async Task<(bool success, Exception exception)> CreateDatabaseAndPopulate(int productCount) 
        {
            try
            {
                await using var context = new NorthWindContext();
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                List<Categories> categories = CategoriesList();
                await context.AddRangeAsync(categories);

                List<Products> products = ProductsList(productCount, categories.Count);
                await context.AddRangeAsync(products);
                await context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception localException)
            {
                return (false, localException);
            }
        }
    }
}
