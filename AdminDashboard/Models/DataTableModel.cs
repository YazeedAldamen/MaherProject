namespace AdminDashboard.Models
{
    public class DataTableModel
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderBy { get; set; }
        public bool isDescending { get; set; }
    }
}
