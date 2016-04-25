namespace EmployeeTracker.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EmployeeTracker.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeTrackerDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EmployeeTracker.DAL.EmployeeTrackerDb";
        }

        protected override void Seed(EmployeeTrackerDb context)
        {
            // seed departments
            context.Departments.AddOrUpdate(d => d.Name,
                new Department { Name = "Call Center" },
                new Department { Name = "Accounting" },
                new Department { Name = "IT" },
                new Department { Name = "Maintenance" },
                new Department { Name = "Human Resources" },
                new Department { Name = "Payroll" },
                new Department { Name = "Submetering" },
                new Department { Name = "Marketing" },
                new Department { Name = "Sales" });
            context.SaveChanges();

            // seed job titles
            context.JobTitles.AddOrUpdate(jt => jt.Name,
                new JobTitle { Name = "Database Engineer" },
                new JobTitle { Name = "Junior Web Developer" },
                new JobTitle { Name = "Web Developer" },
                new JobTitle { Name = "Dev Ops Engineer" },
                new JobTitle { Name = "Project Manager" },
                new JobTitle { Name = "CIO" },
                new JobTitle { Name = "Sales Representative" },
                new JobTitle { Name = "Psychological Consultant" },
                new JobTitle { Name = "Customer Service Representative" },
                new JobTitle { Name = "Janitor" });
            context.SaveChanges();

            // seed employees
            context.Employees.AddOrUpdate(
                new Employee
                {
                    ID = 1,
                    FirstName = "Bruce",
                    LastName = "Springsteen",
                    Email = "bruce.springsteen@conservice.com",
                    PreferredPhoneNumber = 4352106362,
                    AddressLine1 = "342 N 500 E",
                    City = "Logan",
                    State = "UT",
                    Zip = 84321,
                    StartDate = DateTime.Parse("1999-03-01"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Total,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "CIO").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "IT").ID
                },
                new Employee
                {
                    ID = 2,
                    FirstName = "Jeremy",
                    MiddleName = "Nigel",
                    LastName = "Deal",
                    Email = "jeremy.n.deal@gmail.com",
                    PreferredPhoneNumber = 8282288032,
                    AddressLine1 = "194 Stirling Pl.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("2016-05-23"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Total,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Web Developer").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "IT").ID
                },
                new Employee
                {
                    ID = 3,
                    FirstName = "Anne",
                    MiddleName = "Irene Bruce",
                    LastName = "Galizio",
                    Email = "annie.galizio@gmail.com",
                    PreferredPhoneNumber = 9106173802,
                    AddressLine1 = "194 Stirling Pl.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("2015-04-29"),
                    Shift = Shifts.Third,
                    Status = Statuses.PartTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Psychological Consultant").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "Marketing").ID
                },
                new Employee
                {
                    ID = 4,
                    FirstName = "Richard",
                    LastName = "Connoly",
                    Email = "rc@conservice.com",
                    PreferredPhoneNumber = 4352384111,
                    AddressLine1 = "101 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("2012-01-01"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Sales Representative").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "Marketing").ID
                },
                new Employee
                {
                    ID = 5,
                    FirstName = "Buck",
                    LastName = "Rogers",
                    Email = "br@conservice.com",
                    PreferredPhoneNumber = 4352384112,
                    AddressLine1 = "102 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("1988-01-02"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Customer Service Representative").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "Call Center").ID
                },
                new Employee
                {
                    ID = 6,
                    FirstName = "Jack",
                    LastName = "Garrison",
                    Email = "jg@conservice.com",
                    PreferredPhoneNumber = 4352384113,
                    AddressLine1 = "103 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("2003-01-03"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Customer Service Representative").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "Call Center").ID
                },
                new Employee
                {
                    ID = 7,
                    FirstName = "Jason",
                    LastName = "Nickelback",
                    Email = "jn@conservice.com",
                    PreferredPhoneNumber = 4352384114,
                    AddressLine1 = "104 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("1994-01-04"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Janitor").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "Maintenance").ID
                },
                new Employee
                {
                    ID = 8,
                    FirstName = "Alycia",
                    LastName = "Silverado",
                    Email = "as@conservice.com",
                    PreferredPhoneNumber = 4352384115,
                    AddressLine1 = "105 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("2012-01-05"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Database Engineer").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "IT").ID
                },
                new Employee
                {
                    ID = 9,
                    FirstName = "Allen",
                    LastName = "Finch",
                    Email = "af@conservice.com",
                    PreferredPhoneNumber = 4352384116,
                    AddressLine1 = "106 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("1997-01-06"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Database Engineer").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "IT").ID
                },
                new Employee
                {
                    ID = 10,
                    FirstName = "Amy",
                    LastName = "Pond",
                    Email = "ap@conservice.com",
                    PreferredPhoneNumber = 4352384116,
                    AddressLine1 = "107 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("2011-04-22"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Basic,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Junior Web Developer").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "IT").ID
                },
                new Employee
                {
                    ID = 11,
                    FirstName = "Jeremias",
                    LastName = "Trato",
                    Email = "jt@conservice.com",
                    PreferredPhoneNumber = 4352384116,
                    AddressLine1 = "108 Penny Ln.",
                    City = "Logan",
                    State = "UT",
                    Zip = 84341,
                    StartDate = DateTime.Parse("1988-12-25"),
                    Shift = Shifts.First,
                    Status = Statuses.FullTime,
                    PermissionLevel = PermissionLevels.Total,
                    JobTitleID = context.JobTitles.Single(t => t.Name == "Project Manager").ID,
                    DepartmentID = context.Departments.Single(d => d.Name == "IT").ID
                });
            context.SaveChanges();

            //context.Employees.Single(e => e.FirstName == "Jeremy").Manager = context.Employees.Single(e => e.FirstName == "Bruce");
            //context.SaveChanges();

            var itManager = context.Employees.Single(e => e.FirstName == "Bruce");
            var itEmployees = context.Employees.Where(e => e.Department.Name == "IT" && e.FirstName != "Bruce").Select(e => e);
            foreach (var employee in itEmployees)
            {
                employee.Manager = itManager;
            }
            context.SaveChanges();
        }
    }
}
