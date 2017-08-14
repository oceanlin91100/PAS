using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessFormIndexView
    {
        // Properties        
        public IPagedList<AssessFormViewModel> Forms { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public AssessFormIndexView()
        {
            Page = 0;
        }
    }
}