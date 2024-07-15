using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatories
{
    public class EmployeeReposatory : GenaricReposatory<Employee> ,IEmployeeReposatory
    {
        private readonly AppDbContext _context;
        public EmployeeReposatory(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
