using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoriesProductsLibrary.Data;
using CategoriesProductsLibrary.models;
using Microsoft.EntityFrameworkCore;

namespace CategoriesProductsLibrary.Classes
{
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
}
