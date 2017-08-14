using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(RoleMenuMappingViewModel))]
    public class RoleMenuMappingViewModel
    {
        [Key, Display(Name = "Id")]
        public int Id { get; set; }

        [Required, Display(Name = "角色")]
        public int RoleId { get; set; }

        [Required, Display(Name = "選單")]
        public int MenuId { get; set; }

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
