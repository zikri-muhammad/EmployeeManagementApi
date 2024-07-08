using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using EmployeeManagementApi.ViewModels;

namespace EmployeeManagementApi.Services
{
   public class EmployeeService : IEmployeeService
   {
      private readonly IEmployeeRepository _employeeRepository;

      public EmployeeService(IEmployeeRepository employeeRepository)
      {
         _employeeRepository = employeeRepository;
      }

      public async Task<PagedResult<EmployeeDto>> GetAllAsync()
      {
         var employees = await _employeeRepository.GetAllAsync();
         var employeeList = employees.ToList();

         var employeeDtos = employeeList.Select(e => new EmployeeDto
         {
            Id = e.Id,
            Nik = e.Nik,
            Nama = e.Nama,
            Alamat = e.Alamat,
            TanggalLahir = e.TanggalLahir,
            JenisKelamin = e.JenisKelamin,
            Departemen = e.Departemen,
            Jabatan = e.Jabatan
         }).ToList();

         return new PagedResult<EmployeeDto>
         {
            Items = employeeDtos,
            TotalCount = employeeDtos.Count,
            PageNumber = 1,
            PageSize = employeeDtos.Count
         };
      }

      public async Task<PagedResult<EmployeeDto>> SearchAsync(EmployeeSearchQuery query)
      {
         var result = await _employeeRepository.GetAllAsync();

         // Applying filters
         if (!string.IsNullOrWhiteSpace(query.Nama))
            result = result.Where(e => e.Nama.Contains(query.Nama, StringComparison.OrdinalIgnoreCase));
         if (!string.IsNullOrWhiteSpace(query.Departemen))
            result = result.Where(e => e.Departemen.Contains(query.Departemen, StringComparison.OrdinalIgnoreCase));
         if (!string.IsNullOrWhiteSpace(query.Jabatan))
            result = result.Where(e => e.Jabatan.Contains(query.Jabatan, StringComparison.OrdinalIgnoreCase));
         if (query.TanggalLahir.HasValue)
            result = result.Where(e => e.TanggalLahir.Date == query.TanggalLahir.Value.Date);

         var totalCount = result.Count();

         var paginatedResult = result
             .Skip((query.PageNumber - 1) * query.PageSize)
             .Take(query.PageSize)
             .ToList();

         var employeeDtos = paginatedResult.Select(e => new EmployeeDto
         {
            Id = e.Id,
            Nik = e.Nik,
            Nama = e.Nama,
            Alamat = e.Alamat,
            TanggalLahir = e.TanggalLahir,
            JenisKelamin = e.JenisKelamin,
            Departemen = e.Departemen,
            Jabatan = e.Jabatan
         }).ToList();

         return new PagedResult<EmployeeDto>
         {
            Items = employeeDtos,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
         };
      }

      public async Task<EmployeeDto> GetByIdAsync(int id)
      {
         var employee = await _employeeRepository.GetByIdAsync(id);
         if (employee == null)
         {
            return null;
         }
         return new EmployeeDto
         {
            Id = employee.Id,
            Nik = employee.Nik,
            Nama = employee.Nama,
            Alamat = employee.Alamat,
            TanggalLahir = employee.TanggalLahir,
            JenisKelamin = employee.JenisKelamin,
            Departemen = employee.Departemen,
            Jabatan = employee.Jabatan
         };
      }

      public async Task AddAsync(EmployeeDto employeeDto)
      {
         var employee = new Employee
         {
            Nik = employeeDto.Nik,
            Nama = employeeDto.Nama,
            Alamat = employeeDto.Alamat,
            TanggalLahir = employeeDto.TanggalLahir,
            JenisKelamin = employeeDto.JenisKelamin,
            Departemen = employeeDto.Departemen,
            Jabatan = employeeDto.Jabatan
         };
         await _employeeRepository.AddAsync(employee);
      }

      public async Task UpdateAsync(EmployeeDto employeeDto)
      {
         var employee = new Employee
         {
            Id = employeeDto.Id,
            Nik = employeeDto.Nik,
            Nama = employeeDto.Nama,
            Alamat = employeeDto.Alamat,
            TanggalLahir = employeeDto.TanggalLahir,
            JenisKelamin = employeeDto.JenisKelamin,
            Departemen = employeeDto.Departemen,
            Jabatan = employeeDto.Jabatan
         };
         await _employeeRepository.UpdateAsync(employee);
      }

      public async Task DeleteAsync(int id)
      {
         await _employeeRepository.DeleteAsync(id);
      }


   }
}
