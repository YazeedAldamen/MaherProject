using ServiceLayer.DTO;

namespace AdminDashboard.Models.SiteModels
{
    public class ReviewsListModel
    {
        public List<ReviewDTO> Reviews { get; set; }

        public int TotalRecords { get; set; }
    }
}
