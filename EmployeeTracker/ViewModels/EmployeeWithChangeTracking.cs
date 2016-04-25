using EmployeeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTracker.ViewModels
{
    public class EmployeeWithChangeTracking
    {
        public Employee Employee { get; set; }
        public ICollection<EmployeeChangeHistory> EmployeeChangeHistory { get; set; }
    }
}