﻿using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(FunctionBehavioralIndicatorViewModel))]
    public class FunctionBehavioralIndicatorViewModel
    {
        [Key,Display(Name = "Id")]
        public int Id { get; set; }

        [Required, Display(Name = "考核項目Id")]
        public string ItemId { get; set; }

        [Required, Display(Name = "定義說明")]
        public string Definition { get; set; }

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
