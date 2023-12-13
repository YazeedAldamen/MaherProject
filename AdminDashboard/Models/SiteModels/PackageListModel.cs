using ServiceLayer.DTO;

namespace AdminDashboard.Models.SiteModels
{
    public class PackageListModel
    {
        public List<PackageDTO>? Packages { get; set; }
        public int TotalRecords { get; set; }
    }
}
