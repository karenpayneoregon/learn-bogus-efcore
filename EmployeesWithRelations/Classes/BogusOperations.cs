using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DataHelperLibrary.Classes;
using EmployeesWithRelationsLibrary.Data;
using EmployeesWithRelationsLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeesWithRelationsLibrary.Classes
{
    public class BogusOperations
    {
        /// <summary>
        /// We want specific contact types so no bogus
        /// </summary>
        /// <returns></returns>
        public static List<ContactType> ContactTypeList() =>
            new()
            {
                new () {ContactTitle = "Accounting Manager" },
                new () {ContactTitle = "Assistant Sales Agent" },
                new () {ContactTitle = "Assistant Sales Representative" },
                new () {ContactTitle = "Marketing Assistant" },
                new () {ContactTitle = "Marketing Manager" },
                new () {ContactTitle = "Order Administrator" },
                new () {ContactTitle = "Owner" },
                new () {ContactTitle = "Owner/Marketing Assistant" },
                new () {ContactTitle = "Sales Agent" },
                new () {ContactTitle = "Sales Associate" },
                new () {ContactTitle = "Sales Manager" },
                new () {ContactTitle = "Sales Representative" },
                new () {ContactTitle = "Vice President, Sales" }
            };

        public static List<Countries> CountriesList()
        {
            List<Countries> list = new();
            Mocked.CountryNames().ForEach(x => list.Add(new Countries() {Name = x}));
            return list;
        }


        public static async Task<(bool success, Exception exception)> CreateDatabaseAndPopulate(int employeeCount)
        {

            try
            {
                await using var context = new NorthWindContext();
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                await context.AddRangeAsync(ContactTypeList());
                await context.AddRangeAsync(CountriesList());

                //await context.SaveChangesAsync();

                var countryCount = context.Countries.Count();
                var contactTypeCount = context.ContactType.Count();
                
                Faker<Employees> faker = new Faker<Employees>()
                    .RuleFor(e => e.FirstName, f => f.Person.FirstName)
                    .RuleFor(e => e.LastName, f => f.Person.LastName)
                    .RuleFor(e => e.TitleOfCourtesy, f => f.Random.ArrayElement(Mocked.Titles()))
                    .RuleFor(e => e.BirthDate, f => f.Person.DateOfBirth)
                    .RuleFor(e => e.ContactTypeIdentifier, f => f.Random.Int(1, contactTypeCount))
                    .RuleFor(e => e.CountryIdentifier, f => f.Random.Int(1, countryCount));

                var list = faker.Generate(employeeCount);
                await context.AddRangeAsync(list);
                await context.SaveChangesAsync();

                // TODO move to program main
                var empList = await context
                    .Employees
                    .Include(x => x.ContactTypeIdentifierNavigation)
                    .Include(x => x.CountryIdentifierNavigation).ToListAsync();

                return (true, null);
            }
            catch (Exception localException)
            {
                return (false, localException);
            }
            
        }
    }
}
