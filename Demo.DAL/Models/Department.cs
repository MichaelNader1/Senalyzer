using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Code is requred")]
        public int Code { get; set; }

        [Required(ErrorMessage = "name is requred")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
