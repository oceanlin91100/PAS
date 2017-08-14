using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class ProjectScoreIndexView
    {
        // Properties        
        public IPagedList<ProjectScoreViewModel> Scores { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public ProjectScoreIndexView()
        {
            Page = 0;
        }
    }
}