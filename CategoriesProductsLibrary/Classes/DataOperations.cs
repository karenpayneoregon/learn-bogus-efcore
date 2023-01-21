using CategoriesProductsLibrary.Data;
using CategoriesProductsLibrary.models;
using Microsoft.EntityFrameworkCore;

namespace CategoriesProductsLibrary.Classes;

public class DataOperations
{
    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>list of products</returns>
    public static async Task<List<Products>> GetProductsList()
    {
        await using var context = new NorthWindContext();

        return await context.Products.Include(p => p.Category).ToListAsync();
    }
}