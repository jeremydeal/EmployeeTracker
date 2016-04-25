using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeTracker.DAL;
using EmployeeTracker.Models;
using PagedList;
using EmployeeTracker.ViewModels;

namespace EmployeeTracker.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeTrackerDb db;


        public EmployeesController(IEmployeeTrackerDb dbParam)
        {
            db = dbParam;
        }

        // GET: Employees
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // initialize sortOrder values for use in the clickable column headers of the grid view
            ViewBag.LastNameSortParam = String.IsNullOrEmpty(sortOrder) ? "LastName_Desc" : "";      /* defaults to LastName ascending */
            ViewBag.FirstNameSortParam = sortOrder == "FirstName" ? "FirstName_Desc" : "FirstName";
            ViewBag.DepartmentSortParam = sortOrder == "Department" ? "Department_Desc" : "Department";
            ViewBag.JobTitleSortParam = sortOrder == "JobTitle" ? "JobTitle_Desc" : "JobTitle";
            ViewBag.ManagerSortParam = sortOrder == "Manager" ? "Manager_Desc" : "Manager";

            // blah
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            // pull employees from the database
            var employees = db.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .Include(e => e.Manager);

            // filter employees
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.LastName.Contains(searchString)
                                                || e.FirstName.Contains(searchString));
            }

            // sort employees on selected column
            switch (sortOrder)
            {
                case "LastName_Desc":
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
                case "FirstName":
                    employees = employees.OrderBy(e => e.FirstName);
                    break;
                case "FirstName_Desc":
                    employees = employees.OrderByDescending(e => e.FirstName);
                    break;
                case "Department":
                    employees = employees.OrderBy(e => e.Department.Name);
                    break;
                case "Department_Desc":
                    employees = employees.OrderByDescending(e => e.Department.Name);
                    break;
                case "JobTitle":
                    employees = employees.OrderBy(e => e.JobTitle.Name);
                    break;
                case "JobTitle_Desc":
                    employees = employees.OrderByDescending(e => e.JobTitle.Name);
                    break;
                case "Manager":
                    employees = employees.OrderBy(e => e.Manager != null        /* bring null/empty to the bottom */
                                                        ? false
                                                        : true)
                                .ThenBy(e => e.Manager.LastName);
                    break;
                case "Manager_Desc":
                    employees = employees.OrderByDescending(s => s.Manager.LastName)
                        .ThenBy(e => e.Manager != null                  /* bring null/empty to the bottom */
                                         ? e.Manager.LastName
                                         : null);
                    break;
                default:
                    employees = employees.OrderBy(s => s.LastName);
                    break;
            }

            // configure pagination
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees
                .Where(e => e.ID == id)
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .Include(e => e.Manager)
                .SingleOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }

            List<EmployeeChangeHistory> changeHistory = db.EmployeeChangeHistories
                .Where(ch => ch.EmployeeID == employee.ID)
                .Select(ch => ch).ToList();

            var viewModel = new EmployeeWithChangeTracking
            {
                Employee = employee,
                EmployeeChangeHistory = changeHistory
            };

            return View(viewModel);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name");
            ViewBag.JobTitleID = new SelectList(db.JobTitles, "ID", "Name");
            ViewBag.ManagerID = new SelectList(db.Employees, "ID", "FirstName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,MiddleName,LastName,Email,PreferredPhoneNumber,AddressLine1,AddressLine2,AddressLine3,City,State,Zip,StartDate,EndDate,Shift,Status,PermissionLevel,JobTitleID,Image,DepartmentID,ManagerID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            ViewBag.JobTitleID = new SelectList(db.JobTitles, "ID", "Name", employee.JobTitleID);
            ViewBag.ManagerID = new SelectList(db.Employees, "ID", "FirstName", employee.ManagerID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            // populate SelectLists for dropdowns in the view
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            ViewBag.JobTitleID = new SelectList(db.JobTitles, "ID", "Name", employee.JobTitleID);
            ViewBag.ManagerID = new SelectList(db.Employees, "ID", "FirstName", employee.ManagerID);

            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, FirstName,MiddleName,LastName,Email,PreferredPhoneNumber,AddressLine1,AddressLine2,AddressLine3,City,State,Zip,StartDate,EndDate,Shift,Status,PermissionLevel,JobTitleID,Image,DepartmentID,ManagerID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            ViewBag.JobTitleID = new SelectList(db.JobTitles, "ID", "Name", employee.JobTitleID);
            ViewBag.ManagerID = new SelectList(db.Employees, "ID", "FirstName", employee.ManagerID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
