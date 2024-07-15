using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatories
{
    public class GenaricReposatory<T> : IGenaricReposatory<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenaricReposatory(AppDbContext context)
        {
            _context = context;
        }


            public async Task<int> Add(T entity)
            {
                _context.Add(entity);
                return await _context.SaveChangesAsync();
            }

            public async Task<int> Delete(T entity)
            {
                _context.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            public async Task<T> Get(int id)
            {
                return await _context.Set<T>().FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAll()
            {
                if (typeof(T) == typeof(Employee))
                {
                    return (IEnumerable<T>)await _context.Employees.Include(e => e.Department).ToListAsync();

                }
                return await _context.Set<T>().ToListAsync();
            }

            public async Task<int> Update(T entity)
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync();
            }
        }
    }

