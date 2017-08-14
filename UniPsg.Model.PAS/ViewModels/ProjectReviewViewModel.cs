using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(ProjectReviewViewModel))]
    public class ProjectReviewViewModel
    {
        [Key, Display(Name = "Id")]
        public string Id { get; set; }

        [Required, Display(Name = "專案代碼")]
        public int ProjectId { get; set; }

        [Required, Display(Name = "員工編號")]
        public string EmployeeNo { get; set; }

        [Required, Display(Name = "評核員編")]
        public string Reviewer { get; set; }

        [Required, Display(Name = "考核範圍")]
        public int ScopeId { get; set; }

        [Required, Display(Name = "工作要項及目標")]
        public int ItemId { get; set; }

        [Required, Display(Name = "工作要項及目標"), StringLength(125, ErrorMessage = "最多輸入125個中文字")]
        public string ItemName { get; set; }

        [Display(Name = "說明")]
        public string Definition { get; set; }

        [Required, Display(Name = "KPI類別")]
        public int KPICategoryId { get; set; }

        [Display(Name = "KPI類別")]
        public string KPICategoryName { get; set; }

        [Required(ErrorMessage = "請輸入比重"), Display(Name = "比重"),Range(0, 100.00, ErrorMessage = "範圍值為0~100"), DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]       
        public decimal Weight { get; set; }

        [Display(Name = "執行狀況說明"), Range(0, 100.00, ErrorMessage = "範圍值為0~100"), DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Rate { get; set; }

        [Required, Display(Name = "員工考評分數"), Range(0, 100, ErrorMessage = "範圍值為0~100"), DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Score { get; set; }

        [Display(Name = "主管核訂分數"), Range(0, 100, ErrorMessage = "範圍值為0~100"), DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Score1 { get; set; }

        [Display(Name = "自我檢視"), StringLength(300, ErrorMessage = "最多輸入300個中文字")]
        public string Comment { get; set; }

        [Display(Name = "主管檢視"), StringLength(300, ErrorMessage = "最多輸入300個中文字")]
        public string Comment1 { get; set; }

        [Display(Name = "主管檢視"), StringLength(300, ErrorMessage = "最多輸入300個中文字")]
        public string Comment2 { get; set; }

        [Required, Display(Name = "狀態")]
        public int Status { get; set; }

        [Required, Display(Name = "建立者")]
        public string Creator { get; set; }

        [Required, Display(Name = "建立時間")]
        public string CreatedDate { get; set; }

        [Required, Display(Name = "修改者")]
        public string Modifier { get; set; }

        [Required, Display(Name = "修改時間")]
        public string ModifiedDate { get; set; }
    }
}
