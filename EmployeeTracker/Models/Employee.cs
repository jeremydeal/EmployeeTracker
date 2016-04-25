using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text;

namespace EmployeeTracker.Models
{
    #region Enums

    public enum Shifts
    {
        First,
        Second,
        Third
    }

    public enum Statuses
    {
        [Description("Part-Time")]
        PartTime,
        [Description("Full-Time")]
        FullTime,
        Temporary,
        Inactive
    }

    public enum PermissionLevels
    {
        None,
        Basic,
        Administrative,
        Total
    }

    #endregion

    #region Child Classes

    public class EmployeeImage
    {
        public int EmployeeID { get; private set; }
        public byte[] Image { get; set; }
    }

    #endregion

    public class Employee
    {
        [Key]
        public int ID { get; set; }

        // NAME
        [Required]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        // CONTACT
        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Phone")]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}")]
        public long PreferredPhoneNumber { get; set; }

        // ADDRESS
        [Required]
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }
        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }
        [DisplayName("Address Line 3")]
        public string AddressLine3 { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(2)]
        private string state;
        public string State { get { return state; } set { state = value.ToUpper(); } }
        [Required]
        [Range(10000,99999)]
        public int Zip { get; set; }

        // POSITION INFO
        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public Shifts Shift { get; set; }
        [Required]
        public Statuses Status { get; set; }
        [Required]
        [Display(Name = "Permission Level")]
        public PermissionLevels PermissionLevel { get; set; }

        // REFERENCE: JOB TITLE
        [ForeignKey("JobTitle")]
        public int JobTitleID { get; set; }
        public virtual JobTitle JobTitle { get; set; }

        // HEADSHOT
        public byte[] Image { get; set; }
        [StringLength(50)]
        public string ImageMimeType { get; set; }

        // REFERENCE: DEPARTMENT
        [DisplayName("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        // REFERENCE: MANAGER
        [DisplayName("Manager")]
        public int? ManagerID { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> DirectReports { get; set; }


        // HELPER PROPERTIES
        [DisplayName("Name")]
        public string FullNameReversed
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}, {1}", LastName, FirstName);
                if (!string.IsNullOrWhiteSpace(MiddleName))
                {
                    sb.AppendFormat(" {0}.", MiddleName.TrimStart()[0]);
                }
                return sb.ToString();
            }
        }

        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

    }
}