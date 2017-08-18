using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jQueryDataTables.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [MaxLength(1)]
        public string MiddleName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime? StartDate { get; set; }

        [MaxLength(1)]
        public string Sex { get; set; }
    }
}