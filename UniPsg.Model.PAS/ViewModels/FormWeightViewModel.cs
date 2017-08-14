﻿using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(FormWeightViewModel))]
    public class FormWeightViewModel
    {
        [Key, Display(Name = "Id")]
        public int Id { get; set; }

        [Required, Display(Name = "考核表")]
        public int FormId { get; set; }

        [Display(Name = "考核表")]
        public string FormName { get; set; }

        [Required, Display(Name = "考核範圍")]
        public int ScopeId { get; set; }

        [Display(Name = "考核範圍")]
        public string ScopeName { get; set; }

        [Required, Display(Name ="比重(%)")]
        public decimal Weight { get; set; }

        [Display(Name = "適用對象")]
        public string Groups { get; set; }

        [Required, Display(Name = "狀態")]
        public int Status { get; set; }

        [Display(Name = "定義說明")]
        public string Definition { get; set; }

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
