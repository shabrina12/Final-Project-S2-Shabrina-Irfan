using WebAppDTS_API.Models;

namespace WebAppDTS_API.ViewModels
{
    public class AverageGpa : Employee
    {
        public string Major { get; set; } = null!;
        //public string Degree { get; set; } = null!;
        public decimal Gpa { get; set; }
        public string UniversityName { get; set; } = null!;
    }
}
