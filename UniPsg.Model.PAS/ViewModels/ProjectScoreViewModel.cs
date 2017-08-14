using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(ProjectScoreViewModel))]
    public class ProjectScoreViewModel
    {
        [Key, Display(Name = "Id")]
        public string Id { get; set; }

        [Required, Display(Name = "專案代碼")]
        public int ProjectId { get; set; }

        [Display(Name = "專案名稱")]
        public string ProjectName { get; set; }

        [Required, Display(Name = "員工編號")]
        public string EmployeeNo { get; set; }

        [Required, Display(Name = "評核主管編號")]
        public string Reviewer { get; set; }

        [Display(Name = "評核主管姓名")]
        public string ReviewName { get; set; }

        [Required, Display(Name = "KPI分數"), Range(0, 100, ErrorMessage = "範圍值為0~100")]
        public decimal KPIScore { get; set; }

        [Required, Display(Name = "核心職能分數"), Range(0, 100, ErrorMessage = "範圍值為0~100")]
        public decimal CoreScore { get; set; }

        [Required, Display(Name = "管理職能分數"), Range(0, 100, ErrorMessage = "範圍值為0~100")]
        public decimal ManageScore { get; set; }

        [Required, Display(Name = "加減分"), Range(0, 100, ErrorMessage = "範圍值為0~100")]
        public decimal BPScore { get; set; }

        [Required, Display(Name = "總計"), Range(0, 100, ErrorMessage = "範圍值為0~100")]
        public decimal TotalScore { get; set; }

        [Required, Display(Name = "等第")]
        public int RankId { get; set; }

        [Display(Name = "個人發展建議"), StringLength(300, ErrorMessage = "最多輸入300個中文字")]
        public string DevlopComment { get; set; }

        [Display(Name = "評語"), StringLength(300, ErrorMessage = "最多輸入300個中文字")]
        public string Comment { get; set; }

        [Required, Display(Name = "流程順序")]
        public int OderSerial { get; set; }

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
