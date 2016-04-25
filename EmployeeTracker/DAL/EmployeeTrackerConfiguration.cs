using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace EmployeeTracker.DAL
{
    public class EmployeeTrackerConfiguration : DbConfiguration
    {
        public EmployeeTrackerConfiguration()
        {
            //SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}