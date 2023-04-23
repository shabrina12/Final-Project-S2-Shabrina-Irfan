namespace WebAppDTS_API.ViewModels
{
    public class TotalEmployeeByMajor
    {
        public string Major { get; set; } = null!;
        public string UniversityName { get; set; } = null!;
        public int TotalEmployees { get; set; }
    }
}
