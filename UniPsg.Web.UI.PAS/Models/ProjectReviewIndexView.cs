using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class ProjectReviewIndexView
    {
        // Properties        
        public IPagedList<ProjectReviewViewModel> Reviews { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public ProjectReviewIndexView()
        {
            Page = 0;
        }
    }
}