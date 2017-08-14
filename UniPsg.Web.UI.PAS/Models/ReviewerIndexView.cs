using System.Collections.Generic;
using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class ReviewerIndexView
    {
        // Properties        
        public IPagedList<ReviewerViewModel> Reviews { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public ReviewerIndexView()
        {
            Page = 0;
        }
    }
}