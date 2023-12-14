using ServiceLayer.DTO;

namespace AdminDashboard.Models.SiteModels
{
    public class BlogsListModel
    {
        public List<BlogModel>? Blogs { get; set; }
        public int TotalRecords { get; set; }
    }
}
