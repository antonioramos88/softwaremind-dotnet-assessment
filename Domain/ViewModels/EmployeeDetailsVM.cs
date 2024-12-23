namespace Domain.ViewModels
{
    public class EmployeeDetailsVM : EmployeeListView
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
    }
}
