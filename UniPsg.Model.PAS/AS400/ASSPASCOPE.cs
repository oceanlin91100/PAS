﻿namespace UniPsg.Model.PAS.AS400
{
    public class ASSPASCOPE
    {
        #region AssessScope 考核範圍資料檔
        public int ASID { get; set; }
        public string ASNAME { get; set; }
        public int HSITEM { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
