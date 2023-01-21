using DataHelperLibrary.Classes;
using EmployeesWithRelationsLibrary.Models;

namespace EmployeesWithRelationsLibrary.Classes;

public class BogusChildren
{
    /// <summary>
    /// We want specific contact types so no bogus
    /// </summary>
    /// <returns></returns>
    public static List<ContactType> ContactTypeList() =>
        new()
        {
            new() { ContactTitle = "Accounting Manager" },
            new() { ContactTitle = "Assistant Sales Agent" },
            new() { ContactTitle = "Assistant Sales Representative" },
            new() { ContactTitle = "Marketing Assistant" },
            new() { ContactTitle = "Marketing Manager" },
            new() { ContactTitle = "Order Administrator" },
            new() { ContactTitle = "Owner" },
            new() { ContactTitle = "Owner/Marketing Assistant" },
            new() { ContactTitle = "Sales Agent" },
            new() { ContactTitle = "Sales Associate" },
            new() { ContactTitle = "Sales Manager" },
            new() { ContactTitle = "Sales Representative" },
            new() { ContactTitle = "Vice President, Sales" }
        };

    public static List<Countries> CountriesList()
    {
        List<Countries> list = new();
        Mocked.CountryNames().ForEach(x => list.Add(new Countries() { Name = x }));
        return list;
    }
}