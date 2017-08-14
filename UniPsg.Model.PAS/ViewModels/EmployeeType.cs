
using System.ComponentModel.DataAnnotations;


namespace UniPsg.Model.PAS.ViewModels
{
    public enum EmployeeType
    {
        [Display(Name = "高階主管")]
        A,
        [Display(Name = "中階主管")]
        B,
        [Display(Name = "營業主管")]
        C,
        [Display(Name = "行政人員")]
        D,
        [Display(Name = "證券營業員")]
        E,
        [Display(Name = "債券營業員")]
        F,
        [Display(Name = "約雇人員")]
        G,
        [Display(Name = "董監事")]
        H,
        [Display(Name = "工讀生")]
        I,
        [Display(Name = "計件工讀生")]
        J,
        [Display(Name = "顧問")]
        K,
        [Display(Name = "專業人員")]
        L,
        [Display(Name = "理財人員")]
        M,
        [Display(Name = "菁英人員")]
        N        
    }
}
