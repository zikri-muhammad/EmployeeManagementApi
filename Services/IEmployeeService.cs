using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementApi.ViewModels;

namespace EmployeeManagementApi.Services
{
   public interface IEmployeeService
   {
      Task<PagedResult<EmployeeDto>> GetAllAsync();
      Task<EmployeeDto> GetByIdAsync(int id);
      Task AddAsync(EmployeeDto employeeDto);
      Task UpdateAsync(EmployeeDto employeeDto);
      Task DeleteAsync(int id);
      Task<PagedResult<EmployeeDto>> SearchAsync(EmployeeSearchQuery query);
   }
}
