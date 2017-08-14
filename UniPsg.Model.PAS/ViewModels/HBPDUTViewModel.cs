using System.ComponentModel.DataAnnotations;


namespace UniPsg.Model.PAS.ViewModels
{
    [MetadataType(typeof(HBPDUTViewModel))]
    public class HBPDUTViewModel
    {
        public string BRHCOD { get; set; }
        public string DEPTCD { get; set; }
        public string DEPTNAM { get; set; }
        public string EMPNO { get; set; }
    }
}
