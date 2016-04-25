using System.Data.Entity;
using EmployeeTracker.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace EmployeeTracker.DAL
{
    public interface IEmployeeTrackerDb
    {
        DbSet<Department> Departments { get; set; }
        DbSet<EmployeeChangeHistory> EmployeeChangeHistories { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<JobTitle> JobTitles { get; set; }

        // this allows us to mock the methods for DbContext, which lacks an interface
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();
    }
}