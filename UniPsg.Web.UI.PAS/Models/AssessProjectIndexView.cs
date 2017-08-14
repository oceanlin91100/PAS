using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessProjectIndexView
    {
        // Properties        
        public IPagedList<AssessProjectViewModel> Projects { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public AssessProjectIndexView()
        {
            Page = 0;
        }
    }
}