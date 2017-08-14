using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(AssessPersonViewModel))]
    public  class AssessPersonViewModel
    {
        [Key, Display(Name = "專案代碼")]
        public int ProjectId { get; set; }

        [Display(Name = "專案名稱")]
        public string ProjectName { get; set; }

        [Required, Display(Name = "考核表")]
        public int FormId { get; set; }

        [Display(Name = "考核表")]
        public string FormName { get; set; }

        [Required, Display(Name = "適用群組")]
        public int GroupId { get; set; }

        [Display(Name = "適用群組")]
        public string GroupName { get; set; }

        [Key, Display(Name = "員工編號")]
        public string EmployeeNo { get; set; }

        [Required, Display(Name = "員工姓名")]
        public string EmployeeName { get; set; }

        [Required, Display(Name = "目前評核人員")]
        public string Reviewer { get; set; }

        [Display(Name = "目前評核人員")]
        public string ReName { get; set; }

        [Required, Display(Name = "到職日")]
        public int StartDate { get; set; }

        [Required, Display(Name = "試用期到期日")]
        public int TryDate { get; set; }

        [Required, Display(Name = "公司別")]
        public string BranchCode { get; set; }

        [Required, Display(Name = "公司別")]
        public string BranchName { get; set; }

        [Required, Display(Name = "部門別")]
        public string DeptCode { get; set; }

        [Required, Display(Name = "部門別")]
        public string DeptName { get; set; }

        [Display(Name = "科組別")]
        public string TeamCode { get; set; }

        [Display(Name = "科組別")]
        public string TeamName { get; set; }

        [Required, Display(Name = "職務/稱代碼")]
        public string JobCap { get; set; }

        [Required, Display(Name = "職務/稱名稱")]
        public string JobCapName { get; set; }

        [Required, Display(Name = "職務/稱代碼")]
        public string JobPos { get; set; }

        [Required, Display(Name = "職務/稱名稱")]
        public string JobPosName { get; set; }

        [Display(Name = "最高學歷")]
        public string Education { get; set; }

        [Required, Display(Name = "員工屬性")]
        public string EmployeeType { get; set; }

        [Display(Name = "簽核流程")]
        public string FlowPath { get; set; }

        [Display(Name = "簽核流程")]
        public string NamePath { get; set; }

        [Required, Display(Name = "狀態")]
        public int Status { get; set; }

        [Required, Display(Name = "建立者")]
        public string Creator { get; set; }

        [Display(Name = "建立時間")]
        public string CreatedDate { get; set; }

        [Required, Display(Name = "修改者")]
        public string Modifier { get; set; }

        [Display(Name = "修改時間")]
        public string ModifiedDate { get; set; }
    }
}
