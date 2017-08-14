using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(OrganizationViewModel))]
    public  class OrganizationViewModel
    {
        [Key, Display(Name = "專案代碼")]
        public int ProjectId { get; set; }

        [Display(Name = "專案名稱")]
        public string ProjectName { get; set; }

        [Key, Display(Name = "員工編號")]
        public string EmployeeNo { get; set; }

        [Required, Display(Name = "員工姓名")]
        public string EmployeeName { get; set; }

        [Display(Name = "員工帳號")]
        public string Account { get; set; }

        [Required, Display(Name = "公司別")]
        public string BranchCode { get; set; }

        [Required, Display(Name = "公司別")]
        public string BranchName { get; set; }

        [Required, Display(Name = "部門別")]
        public string DeptCode { get; set; }

        [Required, Display(Name = "部門別")]
        public string DeptName { get; set; }

        [ Display(Name = "科組別")]
        public string TeamCode { get; set; }

        [ Display(Name = "科組別")]
        public string TeamName { get; set; }

        [Required, Display(Name = "上層主管員工編號")]
        public string ManagerNo { get; set; }

        [Required, Display(Name = "上層主管姓名")]
        public string ManageName { get; set; }

        [Display(Name = "上層主管帳號")]
        public string ManageAccount { get; set; }

        [Required, Display(Name = "簽核流程")]
        public string FlowPath { get; set; }

        [Required, Display(Name = "簽核流程")]
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
