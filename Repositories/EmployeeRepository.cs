using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementApi.Data;
using EmployeeManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Repositories
{
   public class EmployeeRepository : IEmployeeRepository
   {
      private readonly ApplicationDbContext _context;

      public EmployeeRepository(ApplicationDbContext context)
      {
         _context = context;
      }

      public async Task<IEnumerable<Employee>> GetAllAsync()
      {
         return await _context.Employees.ToListAsync();
      }

      public async Task<Employee> GetByIdAsync(int id)
      {
         return await _context.Employees.FindAsync(id);
      }

      public async Task AddAsync(Employee employee)
      {
         _context.Employees.Add(employee);
         await _context.SaveChangesAsync();
      }

      public async Task UpdateAsync(Employee employee)
      {
         _context.Employees.Update(employee);
         await _context.SaveChangesAsync();
      }

      public async Task DeleteAsync(int id)
      {
         var employee = await _context.Employees.FindAsync(id);
         if (employee != null)
         {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
         }
      }

      public async Task<IEnumerable<Employee>> SearchAsync(string? nama, string jabatan, string? departemen, DateTimeOffset? tanggalLahir)
      {
         var query = _context.Employees.AsQueryable();

         if (!string.IsNullOrEmpty(nama))
         {
            query = query.Where(e => e.Nama.Contains(nama));
         }

         if (!string.IsNullOrEmpty(departemen))
         {
            query = query.Where(e => e.Departemen.Contains(departemen));
         }

         if (!string.IsNullOrEmpty(jabatan))
         {
            query = query.Where(e => e.Jabatan.Contains(jabatan));
         }

         if (tanggalLahir.HasValue)
         {
            query = query.Where(e => e.TanggalLahir.Date == tanggalLahir.Value.Date);
         }

         return await query.ToListAsync();
      }
   }
}
