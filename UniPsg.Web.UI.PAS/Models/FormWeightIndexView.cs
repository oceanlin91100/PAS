using System;
using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class FormWeightIndexView
    {
        // Properties        
        public IPagedList<FormWeightViewModel> Forms { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public FormWeightIndexView()
        {
            Page = 0;
        }
    }
}