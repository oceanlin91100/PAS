using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class KPICategoryIndexView
    {
        // Properties        
        public IPagedList<KPICategoryViewModel> Categories { get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public KPICategoryIndexView()
        {
            Page = 0;
        }
    }
}