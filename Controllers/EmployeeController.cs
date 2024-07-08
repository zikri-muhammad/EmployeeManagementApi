using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementApi.Services;
using EmployeeManagementApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [Authorize]
   public class EmployeeController : ControllerBase
   {
      private readonly IEmployeeService _employeeService;

      public EmployeeController(IEmployeeService employeeService)
      {
         _employeeService = employeeService;
      }

      // [HttpGet]
      // public async Task<ActionResult<PagedResult<EmployeeDto>>> Search([FromQuery] EmployeeSearchQuery query)
      // {
      //    var result = await _employeeService.SearchAsync(query);
      //    return Ok(result);
      // }
      [HttpGet]
      public async Task<ActionResult<PagedResult<IEnumerable<EmployeeDto>>>> GetAll([FromQuery] EmployeeSearchQuery query)
      {


         if (query == null || (string.IsNullOrWhiteSpace(query.Nama) && string.IsNullOrWhiteSpace(query.Departemen) && string.IsNullOrWhiteSpace(query.Jabatan) && query.TanggalLahir == null))
         {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
         }
         else
         {
            var result = await _employeeService.SearchAsync(query);
            return Ok(result);
         }
      }


      [HttpGet("{id}")]
      public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetById(int id)
      {
         var employee = await _employeeService.GetByIdAsync(id);
         if (employee == null)
         {
            return NotFound(new ApiResponse<EmployeeDto> { Message = "Employee not found", Success = false });
         }
         return Ok(new ApiResponse<EmployeeDto> { Data = employee, Message = "Employee retrieved successfully", Success = true });
      }

      [HttpPost]
      public async Task<ActionResult<ApiResponse<EmployeeDto>>> Create([FromBody] EmployeeDto employeeDto)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(new ApiResponse<EmployeeDto> { Message = "Invalid data", Success = false });
         }
         await _employeeService.AddAsync(employeeDto);
         return Ok(new ApiResponse<EmployeeDto> { Data = employeeDto, Message = "Employee created successfully", Success = true });
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<ApiResponse<EmployeeDto>>> Update(int id, [FromBody] EmployeeDto employeeDto)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(new ApiResponse<EmployeeDto> { Message = "Invalid data", Success = false });
         }
         employeeDto.Id = id;
         await _employeeService.UpdateAsync(employeeDto);
         return Ok(new ApiResponse<EmployeeDto> { Data = employeeDto, Message = "Employee updated successfully", Success = true });
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult<ApiResponse<EmployeeDto>>> Delete(int id)
      {
         await _employeeService.DeleteAsync(id);
         return Ok(new ApiResponse<EmployeeDto> { Message = "Employee deleted successfully", Success = true });
      }
   }
}
