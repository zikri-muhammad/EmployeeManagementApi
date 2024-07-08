using Microsoft.AspNetCore.Mvc;

public class EmployeeDto
{
   public int Id { get; set; }
   public string Nik { get; set; }
   public string Nama { get; set; }
   public string Alamat { get; set; }
   public DateTimeOffset TanggalLahir { get; set; }
   public string JenisKelamin { get; set; }
   public string Departemen { get; set; }
   public string Jabatan { get; set; }
}
