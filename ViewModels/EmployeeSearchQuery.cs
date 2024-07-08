namespace EmployeeManagementApi.ViewModels
{
   public class EmployeeSearchQuery
   {
      public string? Nama { get; set; }
      public string? Departemen { get; set; }
      public string? Jabatan { get; set; }
      public string? JenisKelamin { get; set; }
      public DateTimeOffset? TanggalLahir { get; set; }
      public int PageNumber { get; set; } = 1;
      public int PageSize { get; set; } = 10;
   }
}
