using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.DAL.Models
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HireDate { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string ImageName { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
