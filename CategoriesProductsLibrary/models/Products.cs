﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace CategoriesProductsLibrary.models;

public partial class Products
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int? CategoryId { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }

    public virtual Categories Category { get; set; }
}