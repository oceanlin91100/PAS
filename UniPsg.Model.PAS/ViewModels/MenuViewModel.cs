using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(MenuViewModel))]
    public class MenuViewModel
    {
        [Key, Display(Name = "Id")]
        public int Id { get; set; }

        [Required, Display(Name = "名稱")]
        public string Name { get; set; }

        [Required, Display(Name = "Controller")]
        public string Controller { get; set; }

        [Required, Display(Name = "Action")]
        public string Action { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "描述")]        
        public string Definition { get; set; }

        [Display(Name = "父層Id")]        
        public int? ParentId { get; set; }

        [Required, Display(Name = "狀態")]
        public int Status { get; set; }

        [Required, Display(Name = "順序")]
        public int OderSerial { get; set; }

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
