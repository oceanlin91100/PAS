
namespace UniPsg.Model.PAS.AS400
{
    public class ASSPWEIGHT
    {
        #region FormWeight 考核表權重資料檔
        public int FWID { get; set; }
        public int AFID { get; set; }
        public int ASID { get; set; }
        public decimal WEIGHT { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
