using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniPsg.Model.PAS.ViewModels
{
    public class AuditorViewModel
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Manager { get; set; }
        public int Status { get; set; }
        public string Definition { get; set; }
        public string Creator { get; set; }
        public string CreatedDate { get; set; }
        public string Modifier { get; set; }
        public string ModifiedDate { get; set; }
    }
}
