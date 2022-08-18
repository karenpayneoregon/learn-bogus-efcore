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

        public static async Task<(bool success, Exception exception)> CreateDatabaseAndPopulate(int employeeCount)
        {

            try
            {
                await using var context = new NorthWindContext();
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                var countryCount = BogusChildren.CountriesList().Count();
                var contactTypeCount = BogusChildren.ContactTypeList().Count();

                await context.AddRangeAsync(BogusChildren.ContactTypeList());
                await context.AddRangeAsync(BogusChildren.CountriesList());
                
                Faker<Employees> faker = new Faker<Employees>()
                    .RuleFor(e => e.FirstName, f => f.Person.FirstName)
                    .RuleFor(e => e.LastName, f => f.Person.LastName)
                    .RuleFor(e => e.TitleOfCourtesy, f => f.Random.ArrayElement(Mocked.Titles()))
                    .RuleFor(e => e.BirthDate, f => f.Person.DateOfBirth)
                    .RuleFor(e => e.ContactTypeIdentifier, f => f.Random.Int(1, contactTypeCount))
                    .RuleFor(e => e.CountryIdentifier, f => f.Random.Int(1, countryCount));

                var list = faker.Generate(employeeCount);
                await context.AddRangeAsync(list);

                var employeesList = context.Employees.Where(x => x.EmployeeID > 1).ToList();

                for (int index = 0; index < employeesList.Count; index++)
                {
                    employeesList[index].ReportsToNavigationEmployee = context.Employees.FirstOrDefault(x => x.EmployeeID == 1);
                }

                await context.SaveChangesAsync();

                // TODO move to program main
                var empList = await context
                    .Employees
                    .Include(x => x.ContactTypeIdentifierNavigation)
                    .Include(x => x.InverseReportsToNavigationEmployee)
                    .Include(x => x.ReportsToNavigationEmployee)
                    .Include(x => x.CountryIdentifierNavigation)
                    .ToListAsync();

                return (true, null);
            }
            catch (Exception localException)
            {
                return (false, localException);
            }
            
        }
    }
}
