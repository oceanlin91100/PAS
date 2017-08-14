using System;
using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessRankingIndexView
    {
        // Properties        
        public IPagedList<AssessRankingViewModel> Rankings { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public AssessRankingIndexView()
        {
            Page = 0;
        }
    }
}