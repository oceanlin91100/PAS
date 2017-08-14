using System;
using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(AssessProjectViewModel))]
    public class AssessProjectViewModel
    {
        [Key, Display(Name = "Id")]
        public int Id { get; set; }

        [Required, Display(Name = "名稱")]
        public string Name { get; set; }

        [Required, Display(Name = "受考核起始日")]
        public int ViewStarDate { get; set; }

        [Required, Display(Name = "受考核結束日")]
        public int ViewEndDate { get; set; }

        [Required, Display(Name = "考核作業起始日")]
        public int FromDate { get; set; }

        [Required, Display(Name = "考核作業截止日")]
        public int Deadline { get; set; }

        [Required, Display(Name = "試用期(迄)日")]
        public int TryDatae { get; set; }

        [Required, Display(Name = "受考核年度")]
        public int AssessYear { get; set; }

        [Required, Display(Name = "受考核對象")]
        public string Groups { get; set; }

        [Required, Display(Name = "適用考格表")]
        public string Forms { get; set; }

        [Required, Display(Name = "考核類別")]
        public int CategoryId { get; set; }

        [Display(Name = "考核類別")]
        public string CategoryName { get; set; }

        [Required, Display(Name = "狀態")]
        public int Status { get; set; }

        [Display(Name = "定義說明")]
        public string Definition { get; set; }

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
