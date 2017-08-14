using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessGroupIndexView
    {
        // Properties        
        public IPagedList<AssessGroupViewModel> Groups { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public AssessGroupIndexView()
        {
            Page = 0;
        }
    }
}