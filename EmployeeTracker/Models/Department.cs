using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeTracker.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}