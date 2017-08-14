using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessCategoryIndexView
    {
        // Properties        
        public IPagedList<AssessCategoryViewModel> Categories { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public AssessCategoryIndexView()
        {   
            Page = 0;
        }
    }
}