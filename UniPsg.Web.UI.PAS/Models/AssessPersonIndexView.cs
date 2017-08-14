using UniPsg.Model.PAS.ViewModels;
using PagedList;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessPersonIndexView
    {
        // Properties  
        public int? ProjectId { get; set; }      
        public string BranchCode { get; set; }
        public string DeptCode { get; set; }
        public string EmployeeNo { get; set; }
        public IPagedList<AssessPersonViewModel> People{ get; set; } // 符合條件資料

        public int Page { get; set; }  // 頁碼

        // Constructors
        public AssessPersonIndexView()
        {   
            BranchCode = string.Empty;
            DeptCode = string.Empty;
            Page = 0;
        }
    }
}