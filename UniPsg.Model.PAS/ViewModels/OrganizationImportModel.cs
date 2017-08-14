using System.ComponentModel.DataAnnotations;

namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(OrganizationImportModel))]
    public class OrganizationImportModel
    {

        [Key, Display(Name = "專案代碼")]
        public int ProjectId { get; set; }

        [Display(Name = "專案名稱")]
        public string ProjectName { get; set; }

        [Required, Display(Name = "建立者")]
        public string Creator { get; set; }
    }
}
