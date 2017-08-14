using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniPsg.Model.PAS.ViewModels
{
    public class FlowLogViewModel
    {
        public int Id { get; set; }
        public string BeforContent { get; set; }
        public string AfterContent { get; set; }
        public string Creator { get; set; }
        public string CreatedDate { get; set; }
    }
}
