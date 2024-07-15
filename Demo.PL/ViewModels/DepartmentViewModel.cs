using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is requred")]
        public int Code { get; set; }

        [Required(ErrorMessage = "name is requred")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

    }
}
