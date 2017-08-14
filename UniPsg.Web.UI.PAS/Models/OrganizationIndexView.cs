using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class OrganizationIndexView
    {
        // Properties                
        public int? ProjectId { get; set; }      
        public string BranchCode { get; set; }
        public string DeptCode { get; set; }
        public string EmployeeNo { get; set; }

        public IPagedList<OrganizationViewModel> Organizations { get; set; } // 符合條件資料
        
        public int Page { get; set; }  // 頁碼

        // Constructors
        public OrganizationIndexView()
        {
            Page = 0;
        }
    }
}