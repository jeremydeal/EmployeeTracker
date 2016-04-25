using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeTracker;
using EmployeeTracker.Controllers;
using Moq;
using EmployeeTracker.DAL;
using EmployeeTracker.Models;
using System.Data.Entity;

namespace EmployeeTracker.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void EditActionPullsEmployeeModel()
        {
            // Arrange
            EmployeesController controller = SetUpEmployeesController();
            ViewResult result = controller.Index(null, null, null, null) as ViewResult;
            IEnumerable<Employee> model = result.Model as IEnumerable<Employee>;

            // Assert
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void EditActionStoresOriginalEmployeeInViewBag()
        {
            // Arrange
            EmployeesController controller = SetUpEmployeesController();
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.AreEqual(1, result.ViewBag.OriginalEmployee.ID);
        }



        private EmployeesController SetUpEmployeesController()
        {
            // mock DB
            var mockDb = new Mock<IEmployeeTrackerDb>();

            //// mock departments table
            //var departments = new List<Department>
            //{
            //    new Department { Name = "Call Center" },
            //    new Department { Name = "Accounting" },
            //    new Department { Name = "IT" },
            //    new Department { Name = "Maintenance" },
            //    new Department { Name = "Human Resources" },
            //    new Department { Name = "Payroll" },
            //    new Department { Name = "Submetering" },
            //    new Department { Name = "Marketing" },
            //    new Department { Name = "Sales" }
            //}.AsQueryable();

            //var departmentsSet = new Mock<DbSet<Department>>();
            //departmentsSet.As<IQueryable<Department>>().Setup(m => m.Provider).Returns(departments.Provider);
            //departmentsSet.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(departments.Expression);
            //departmentsSet.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(departments.ElementType);
            //departmentsSet.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(departments.GetEnumerator());

            //mockDb.Setup(m => m.Departments).Returns(departmentsSet.Object);

            //// mock job titles table
            //var jobTitles = new List<JobTitle>
            //{
            //    new JobTitle { Name = "Database Engineer" },
            //    new JobTitle { Name = "Junior Web Developer" },
            //    new JobTitle { Name = "Web Developer" },
            //    new JobTitle { Name = "Dev Ops Engineer" },
            //    new JobTitle { Name = "Project Manager" },
            //    new JobTitle { Name = "CIO" },
            //    new JobTitle { Name = "Sales Representative" },
            //    new JobTitle { Name = "Psychological Consultant" },
            //    new JobTitle { Name = "Customer Service Representative" },
            //    new JobTitle { Name = "Janitor" }
            //}.AsQueryable();

            //var jobTitlesSet = new Mock<DbSet<JobTitle>>();
            //jobTitlesSet.As<IQueryable<JobTitle>>().Setup(m => m.Provider).Returns(jobTitles.Provider);
            //jobTitlesSet.As<IQueryable<JobTitle>>().Setup(m => m.Expression).Returns(jobTitles.Expression);
            //jobTitlesSet.As<IQueryable<JobTitle>>().Setup(m => m.ElementType).Returns(jobTitles.ElementType);
            //jobTitlesSet.As<IQueryable<JobTitle>>().Setup(m => m.GetEnumerator()).Returns(jobTitles.GetEnumerator());

            //mockDb.Setup(m => m.JobTitles).Returns(jobTitlesSet.Object);

            // mock employees table
            var employees = new List<Employee>
            {
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
                    JobTitleID = 1,
                    DepartmentID = 1
                }
                //new Employee
                //{
                //    ID = 2,
                //    FirstName = "Jeremy",
                //    MiddleName = "Nigel",
                //    LastName = "Deal",
                //    Email = "jeremy.n.deal@gmail.com",
                //    PreferredPhoneNumber = 8282288032,
                //    AddressLine1 = "194 Stirling Pl.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("2016-05-23"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Total,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Web Developer").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "IT").ID
                //},
                //new Employee
                //{
                //    ID = 3,
                //    FirstName = "Anne",
                //    MiddleName = "Irene Bruce",
                //    LastName = "Galizio",
                //    Email = "annie.galizio@gmail.com",
                //    PreferredPhoneNumber = 9106173802,
                //    AddressLine1 = "194 Stirling Pl.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("2015-04-29"),
                //    Shift = Shifts.Third,
                //    Status = Statuses.PartTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Psychological Consultant").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "Marketing").ID
                //},
                //new Employee
                //{
                //    ID = 4,
                //    FirstName = "Richard",
                //    LastName = "Connoly",
                //    Email = "rc@conservice.com",
                //    PreferredPhoneNumber = 4352384111,
                //    AddressLine1 = "101 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("2012-01-01"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Sales Representative").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "Marketing").ID
                //},
                //new Employee
                //{
                //    ID = 5,
                //    FirstName = "Buck",
                //    LastName = "Rogers",
                //    Email = "br@conservice.com",
                //    PreferredPhoneNumber = 4352384112,
                //    AddressLine1 = "102 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("1988-01-02"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Customer Service Representative").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "Call Center").ID
                //},
                //new Employee
                //{
                //    ID = 6,
                //    FirstName = "Jack",
                //    LastName = "Garrison",
                //    Email = "jg@conservice.com",
                //    PreferredPhoneNumber = 4352384113,
                //    AddressLine1 = "103 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("2003-01-03"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Customer Service Representative").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "Call Center").ID
                //},
                //new Employee
                //{
                //    ID = 7,
                //    FirstName = "Jason",
                //    LastName = "Nickelback",
                //    Email = "jn@conservice.com",
                //    PreferredPhoneNumber = 4352384114,
                //    AddressLine1 = "104 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("1994-01-04"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Janitor").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "Maintenance").ID
                //},
                //new Employee
                //{
                //    ID = 8,
                //    FirstName = "Alycia",
                //    LastName = "Silverado",
                //    Email = "as@conservice.com",
                //    PreferredPhoneNumber = 4352384115,
                //    AddressLine1 = "105 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("2012-01-05"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Database Engineer").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "IT").ID
                //},
                //new Employee
                //{
                //    ID = 9,
                //    FirstName = "Allen",
                //    LastName = "Finch",
                //    Email = "af@conservice.com",
                //    PreferredPhoneNumber = 4352384116,
                //    AddressLine1 = "106 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("1997-01-06"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Database Engineer").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "IT").ID
                //},
                //new Employee
                //{
                //    ID = 10,
                //    FirstName = "Amy",
                //    LastName = "Pond",
                //    Email = "ap@conservice.com",
                //    PreferredPhoneNumber = 4352384116,
                //    AddressLine1 = "107 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("2011-04-22"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Basic,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Junior Web Developer").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "IT").ID
                //},
                //new Employee
                //{
                //    ID = 11,
                //    FirstName = "Jeremias",
                //    LastName = "Trato",
                //    Email = "jt@conservice.com",
                //    PreferredPhoneNumber = 4352384116,
                //    AddressLine1 = "108 Penny Ln.",
                //    City = "Logan",
                //    State = "UT",
                //    Zip = 84341,
                //    StartDate = DateTime.Parse("1988-12-25"),
                //    Shift = Shifts.First,
                //    Status = Statuses.FullTime,
                //    PermissionLevel = PermissionLevels.Total,
                //    JobTitleID = mockDb.Object.JobTitles.Single(t => t.Name == "Project Manager").ID,
                //    DepartmentID = mockDb.Object.Departments.Single(d => d.Name == "IT").ID
                //}
            }.AsQueryable();

            var employeesSet = new Mock<DbSet<Employee>>();
            employeesSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            employeesSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            employeesSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            employeesSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            mockDb.Setup(m => m.Employees).Returns(employeesSet.Object);

            // return controller with the mocked DB
            return new EmployeesController(mockDb.Object);
        }

    }
}
