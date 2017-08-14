using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(ReviewerViewModel))]
    public class ReviewerViewModel
    {
        [Display(Name = "專案代碼")]
        public int ProjectId { get; set; }

        [Display(Name = "專案名稱")]
        public string ProjectName { get; set; }

        [Display(Name = "受考核年度")]
        public int AssessYear { get; set; }

        [Display(Name = "受考核對象")]
        public int GroupId { get; set; }

        [Display(Name = "受考核對象")]
        public string GroupName { get; set; }

        [Display(Name = "適用考格表")]
        public int FormId { get; set; }

        [Display(Name = "適用考格表")]
        public string FormName { get; set; }

        [Display(Name = "員工編號")]
        public string EmployeeNo { get; set; }

        [Display(Name = "員工姓名")]
        public string EmployeeName { get; set; }

        [Display(Name = "考核類別")]
        public int CategoryId { get; set; }

        [Display(Name = "目前檢視")]
        public string Reviewer { get; set; }

    }
}
