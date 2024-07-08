using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementApi.Models;


namespace EmployeeManagementApi.Repositories
{

   public interface IEmployeeRepository
   {
      Task<IEnumerable<Employee>> GetAllAsync();
      Task<Employee> GetByIdAsync(int id);
      Task AddAsync(Employee employee);
      Task UpdateAsync(Employee employee);
      Task DeleteAsync(int id);
      Task<IEnumerable<Employee>> SearchAsync(string nama, string departemen, string jabatan, DateTimeOffset? tanggalLahir);
   }

}