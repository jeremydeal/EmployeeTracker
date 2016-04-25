using EmployeeTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using EmployeeTracker.DAL;

namespace EmployeeTracker.Sandbox {
    public class EmployeeTrackerDbExperimental : DbContext, IEmployeeTrackerDb
    {
        public EmployeeTrackerDbExperimental() : base("EmployeeTrackerDbExperimental")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<EmployeeChangeHistory> EmployeeChangeHistories { get; set; }


        // Audit change tracking by overriding DbContext's SaveChanges method here.
        // Note: based on code samples from https://jmdority.wordpress.com/2011/07/20/using-entity-framework-4-1-dbcontext-change-tracking-for-audit-logging/
        public override int SaveChanges()
        {
            // Get all Added/Modified entities
            IEnumerable<DbEntityEntry> entries = this.ChangeTracker.Entries()
                                                    .Where(p => p.State == EntityState.Added
                                                            || p.State == EntityState.Modified)
                                                    // Get only entries related to the Employee table
                                                    .Where(e => e.Entity.GetType() == typeof(Employee));

            foreach (var entry in entries)
            {
                // For each changed record, get the audit record entries and add them
                foreach (EmployeeChangeHistory x in GetAuditRecordsForEmployee(entry))
                {
                    this.EmployeeChangeHistories.Add(x);
                }
            }

            // Save both the original changes made and the audit records
            return base.SaveChanges();
        }

        private List<EmployeeChangeHistory> GetAuditRecordsForEmployee(DbEntityEntry employeeEntry)
        {
            var result = new List<EmployeeChangeHistory>();
            DateTime changeTime = DateTime.UtcNow;
            var newVals = employeeEntry.CurrentValues;
            var oldVals = employeeEntry.OriginalValues;


            if (employeeEntry.State == EntityState.Added)
            {
                // For Inserts, add an entry for each tracked column
                result.AddRange( new List<EmployeeChangeHistory>() {
                    new EmployeeChangeHistory()
                    {
                        EmployeeID = oldVals.GetValue<int>("ID"),
                        DateChanged = changeTime,
                        Type = ChangeTypes.JobTitleChange,
                        OldValue = "",
                        NewValue = newVals.GetValue<JobTitle>("JobTitle").Name
                    },
                    new EmployeeChangeHistory()
                    {
                        EmployeeID = oldVals.GetValue<int>("ID"),
                        DateChanged = changeTime,
                        Type = ChangeTypes.PermissionsLevelChange,
                        OldValue = "",
                        NewValue = newVals.GetValue<PermissionLevels>("PermissionLevel").ToString()
                    },
                    new EmployeeChangeHistory()
                    {
                        EmployeeID = oldVals.GetValue<int>("ID"),
                        DateChanged = changeTime,
                        Type = ChangeTypes.ManagerChange,
                        OldValue = "",
                        NewValue = newVals.GetValue<Employee>("Manager").FullName
                    }
                });

            }
            else if (employeeEntry.State == EntityState.Modified)
            {
                // For Inserts, add an entry for each tracked column

                // If job title has been changed, add the change
                if (oldVals.GetValue<int>("JobTitleID") != newVals.GetValue<int>("JobTitleID"))
                {
                    result.Add(new EmployeeChangeHistory()
                    {
                        EmployeeID = oldVals.GetValue<int>("ID"),
                        DateChanged = changeTime,
                        Type = ChangeTypes.JobTitleChange,
                        OldValue = oldVals.GetValue<JobTitle>("JobTitleID").Name,
                        NewValue = newVals.GetValue<JobTitle>("JobTitleID").Name
                    });
                }

                // If permission level has been changed, add the change
                if (oldVals.GetValue<PermissionLevels>("PermissionLevel") != newVals.GetValue<PermissionLevels>("PermissionLevel"))
                {
                    result.Add(new EmployeeChangeHistory()
                    {
                        EmployeeID = oldVals.GetValue<int>("ID"),
                        DateChanged = changeTime,
                        Type = ChangeTypes.JobTitleChange,
                        OldValue = oldVals.GetValue<PermissionLevels>("PermissionLevel").ToString(),
                        NewValue = newVals.GetValue<PermissionLevels>("PermissionLevel").ToString()
                    });
                }

                // If manager has been changed, add the change
                if (oldVals.GetValue<int>("ManagerID") != newVals.GetValue<int>("ManagerID"))
                {
                    result.Add(new EmployeeChangeHistory()
                    {
                        EmployeeID = oldVals.GetValue<int>("ID"),
                        DateChanged = changeTime,
                        Type = ChangeTypes.JobTitleChange,
                        OldValue = oldVals.GetValue<Employee>("Manager").FullName,
                        NewValue = newVals.GetValue<Employee>("Manager").FullName
                    });
                }

            }
            // Otherwise, don't do anything, we don't care about Unchanged or Detached entities

            return result;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.Manager)
                .WithMany(m => m.DirectReports)
                .HasForeignKey(e => e.ManagerID)
                .WillCascadeOnDelete(false);
        }
    }
}