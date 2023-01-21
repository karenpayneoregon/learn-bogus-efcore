# Using EF Core and Bogus

In this article with basic to intermediate code sample, learn how to generate data which can be used for working through data schemas along with Entity Framework Core ([EF Core](https://learn.microsoft.com/en-us/ef/core/)) using [Bogus](https://github.com/bchavez/Bogus).

What is Bogus? 

*A simple fake data generator for C#, F#, and VB.NET. Based on and ported from the famed faker.js.*

> **Note**
> Code presented was originally written in VS2019, .NET Core 5 and has been updated to .NET Core 7, EF Core 7 using VS2022.

The first step when working with databases is to plan out the schema which comes from business requirements. Once the schema is in place write SQL statements to validate all operations can be performed or some will want this done bypassing [SQL statement](https://learn.microsoft.com/en-us/sql/t-sql/statements/statements?view=sql-server-ver16) and use their EF Core code. The other path is to test with SQL and EF Core.

Some will want to do this with a mocking framework like [Progress Telerik JustMock](https://www.telerik.com/products/mocking.aspx) and EF Core [in-memory](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli) testing which doesn’t fully replicate working with databases.

This is where Bogus NuGet package [![](assets/Link_16x.png)](https://www.nuget.org/packages/Bogus) comes into play. Bogus provides everything needed to create mocked data for your database. Check out their documentation and code samples to get an idea how to work with Bogus. There can be a problem with novice developers/coders in how to put everything together which is where this article will assist novice developers/coders.


## Step 1

Once the database has been created, [scaffold/reverse engineer](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli) to create a DbContext and models

## Step 2

Write code to create the database and tables, the following code is used in each project.

Each place will have a different `DbContext`, in this case `NorthWindContext`.

```csharp
// create or recreate database
await using var context = new NorthWindContext();
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();
```

Once done start writing code to create fake data, here there are two models, `Categories` and `Products`.

```csharp
public partial class Categories
{
    public Categories()
    {
        Products = new HashSet<Products>();
    }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public virtual ICollection<Products> Products { get; set; }
}
```

</br>

```csharp
    public partial class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }

        public virtual Categories Category { get; set; }
    }
```

</br>

**Important notes**

- Going forward, the secret to knowing were to find data is to read [Bogus](https://github.com/bchavez/Bogus) information in the readme file which goes into depth were to find paths to generate data. This is very important as you can clash with their classes e.g. you create a Person class and guess what, Bogus has a Person class. This is were knowing how to work with `using` statements and `using` statements with `aliasing`.
- We don't create primary keys, EF Core does this for us.


## Step 3

Code to generate a single category using [Bogus](https://github.com/bchavez/Bogus) where `f.Commerce.Categories(1)` selects one category name from an array, `[0]` indicates we want the first category name.

Note there is a parameter which specifies how many records to create which defaults to 4.

The last line `return fake.Generate(count);` performs generating and returns the generated list to the caller which is next up.

```csharp
public static List<Categories> CategoriesList(int count = 4)
{
    var fake = new Faker<Categories>()
        .RuleFor(c => c.CategoryName, f => f.Commerce.Categories(1)[0]);

    return fake.Generate(count);
}
```

</br>

The following code uses `CategoriesList` to generate the Product list


```csharp
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
```

## Calling above code

We call the code to generate data in the following method.

1. Ask permission to continue, this is important if you ran the process and don't want to regenerate.
2. Method is called to generate the data


```csharp
private static async Task Initialize(int count)
{
    AnsiConsole.MarkupLine("[skyblue1]Creating and populating database[/]");
            
    var (success, exception) = await BogusOperations.CreateDatabaseAndPopulate(count);

    if (!success)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[red]Failed to create and populated[/]");
        Console.WriteLine(exception.Message);
    }
}
```

## Reading data

After the code to generate executes and returns without any errors the following code shows the generated data.

```csharp
private static async Task ReadProducts()
{
    AnsiConsole.Clear();

    List<Products> list = await DataOperations.GetProductsList();

    var table = new Table()
        .RoundedBorder()
        .AddColumn("[b]Id[/]")
        .AddColumn("[b]Name[/]")
        .AddColumn("[b]Unit price[/]")
        .AddColumn("[b]Category[/]")
        .Alignment(Justify.Center)
        .BorderColor(Color.LightSlateGrey)
        .Title("[LightGreen]Products[/]");


    foreach (var product in list)
    {
        // ReSharper disable once PossibleInvalidOperationException
        table.AddRow(product.ProductId.ToString(), product.ProductName, product.UnitPrice.Value.ToString("C2"), product.Category.CategoryName);
    }

    AnsiConsole.Write(table);
            
    Console.WriteLine();
    AnsiConsole.MarkupLine("Press [b]any[/] key to leave");

    Console.ReadLine();

}
```

## Protecting generated data

Once data has been generated suppose you don't want to overwrite it? The following statement can tell us all tables are populated.

```sql
SELECT   T.name AS TableName, SI.rows AS NumberOfRows
FROM     sys.tables AS T INNER JOIN sys.sysindexes AS SI ON T.object_id = SI.id
WHERE    (SI.indid IN (0, 1)) AND T.name <> 'sysdiagrams'
ORDER BY NumberOfRows DESC, TableName
```

**Basics to run in code**

Place the query into a method as shown below

```csharp
public class SqlStatements
{
    public static string AllTablesHaveRecords => "SELECT T.name TableName,i.Rows NumberOfRows FROM sys.tables T JOIN sys.sysindexes I ON T.OBJECT_ID = I.ID WHERE indid IN (0,1) ORDER BY i.Rows DESC,T.name";
}
```

:pushpin:  Create a method to check if all tables are populated

```csharp
public static bool TablesArePopulated()
{
    using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
    using var cmd = new SqlCommand(SqlStatements.AllTablesHaveRecords, cn);

    DataTable table = new();
    cn.Open();

    table.Load(cmd.ExecuteReader());
    return table.AsEnumerable()
        .All(row => row.Field<int>("NumberOfRows") > 0);

}
```

To check if the datbase exists (code is in several projects), create a new instance of a DbContext and call it `await someContext.CanConnectAsync();`

```csharp
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
```

## See also

- [Learn DateOnly & TimeOnly](https://dev.to/karenpayneoregon/learn-dateonly-timeonly-23j0) which works with Bogu


## Summary

This article provides code samples to fake/generate data with one example HasDataConsoleApp which generates data directly in the DbContext for a single table while the remaining projects show how to work with one to many models.

Take time to run each console project, study the code than read the readme file at the Bogus GitHub repository [![](assets/Link_16x.png)](https://github.com/bchavez/Bogus) and note at the end of the readme page there are other free packages to enhance Bogus.

:small_blue_diamond: I can not stress enough to read the documentation on [Bogus](https://github.com/bchavez/Bogus) rather than just work with provided code. See [featured in](https://github.com/bchavez/Bogus#featured-in) also.

## See also

:pushpin:  [FluentValidation tips C#](https://dev.to/karenpayneoregon/fluentvalidation-tips-c-3olf)


## Requires

- Microsoft Visual Studio 2022 or higher
- .NET Core 7 or higher
- SQL-Server Express installed

