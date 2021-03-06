﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeTracker.Models
{
    public enum ChangeTypes
    {
        [Description("Permissions Level Change")]
        PermissionsLevelChange,
        [Description("Job Title Change")]
        JobTitleChange,
        [Description("Manager Change")]
        ManagerChange
    }

    public class EmployeeChangeHistory
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public ChangeTypes Type { get; set; }
        [Required]
        public string OldValue { get; set; }
        [Required]
        public string NewValue { get; set; }
        [Required]
        public DateTime DateChanged { get; set; }
    }
}