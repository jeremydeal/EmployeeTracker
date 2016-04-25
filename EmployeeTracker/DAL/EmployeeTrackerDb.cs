using EmployeeTracker.Models;
using System.Data.Entity;

namespace EmployeeTracker.DAL
{
    public class EmployeeTrackerDb : DbContext, IEmployeeTrackerDb
    {
        public EmployeeTrackerDb() : base("EmployeeTrackerDb")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<EmployeeChangeHistory> EmployeeChangeHistories { get; set; }

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