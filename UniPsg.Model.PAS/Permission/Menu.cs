//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniPsg.Model.PAS.Permission
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public int MenuId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
        public int Staturs { get; set; }
        public int OderSerial { get; set; }
        public string Creator { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Modifier { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
