using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatories
{
    public class DepartmentReposatory : GenaricReposatory<Department>, IDepartmentReposatory
    {
        private readonly AppDbContext _context;
        public DepartmentReposatory(AppDbContext context):base(context) 
        {
            _context = context;
        }


    }
}
