using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeTracker.Models
{
    public class JobTitle
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}