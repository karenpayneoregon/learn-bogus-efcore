﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

using EmployeesWithRelationsLibrary.Models;
using Microsoft.EntityFrameworkCore;
using ConfigurationLibrary.Classes;

#nullable disable

#nullable disable

namespace EmployeesWithRelationsLibrary.Data;

public partial class NorthWindContext : DbContext
{
    public NorthWindContext()
    {
    }

    public NorthWindContext(DbContextOptions<NorthWindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContactType> ContactType { get; set; }
    public virtual DbSet<Countries> Countries { get; set; }
    public virtual DbSet<Employees> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConfigurationHelper.ConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.ApplyConfiguration(new Configurations.ContactTypeConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.CountriesConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.EmployeesConfiguration());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}