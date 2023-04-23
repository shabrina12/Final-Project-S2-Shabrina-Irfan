using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebAppDTS_API.Models;

namespace WebAppDTS_API.ViewModels
{
    public class EmployeeMasterVM : Employee
    {
        public string Major { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public decimal GPA { get; set; }
        public string UniversityName { get; set; } = null!;
    }
}

