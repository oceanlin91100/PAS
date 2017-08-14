using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UniPsg.Model.Schedule
{
    public class InstantMailModel
    {
        public MailMessage MailInformation { get; set; }

        public List<string> parameterValues { get; set; }

        public int systemScheduleId { get; set; }

        public int systemId { get; set; }

        public int patterId { get; set; }

        public string mailTo { get; set; }

        public string mailCc { get; set; }

        public string creator { get; set; }

    }
}