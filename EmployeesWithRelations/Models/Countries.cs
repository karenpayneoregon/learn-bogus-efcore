﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EmployeesWithRelationsLibrary.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Employees = new HashSet<Employees>();
        }

        public int CountryIdentifier { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}