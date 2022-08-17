using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CategoryProductConsoleApp.Classes
{
    public static class EntityFrameworkExtensions
    {
        public static async Task<(bool success, Exception exception)> CanConnectAsync(this DbContext context)
        {
            try
            {
                var result = await Task.Run(async () => await context.Database.CanConnectAsync());
                return (result, null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }
    }
}
