namespace AdminDashboard.Models
{
    public class TopTenModel
    {
        public List<int> oldTopTen { get; set; }
        public List<int> newTopTen { get; set; }
        public List<PackageModel> Packages { get; set; }
    }
}
