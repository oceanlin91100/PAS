using System.IO;
using UniPsg.Model.Schedule;
using System.Net;
using System.Runtime.Serialization.Json;

namespace UniPsg.Web.UI.PAS.Models
{
    public class MailClient
    {
        private string BaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeUrl"];

        public bool SendNotice(InstantMailModel model)       
        {
            try
            {
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(BaseUrl);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response = client.PostAsJsonAsync("InstantSendMail/SendMail", model).Result;
                //return response.IsSuccessStatusCode;

                using (WebClient client = new WebClient())
                {
                    bool result;
                    string MailService = BaseUrl;
                    client.Headers["Content-type"] = "application/json";
                    MemoryStream stream = new MemoryStream();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UniPsg.Model.Schedule.InstantMailModel));
                    serializer.WriteObject(stream, model);

                    byte[] data = client.UploadData(MailService + "InstantSendMail/SendMail", "POST", stream.ToArray());

                    stream = new MemoryStream(data);
                    serializer = new DataContractJsonSerializer(typeof(bool));
                    result = (bool)serializer.ReadObject(stream);
                    return result;
                }

            }
            catch
            {
                return false;
            }
        }


        public void SendMail(string toMail, string subject, string body, string fromMailName, string fromMailAddess)
        {
            System.Net.Mail.MailMessage Mail = new System.Net.Mail.MailMessage();
            Mail.From = new System.Net.Mail.MailAddress(fromMailAddess, fromMailName);
            Mail.Subject = subject;
            Mail.IsBodyHtml = true;

            Mail.BodyEncoding = System.Text.Encoding.UTF8;
            Mail.SubjectEncoding = System.Text.Encoding.UTF8;
            Mail.Body = body;
            Mail.To.Add(toMail) ;

            System.Net.Mail.SmtpClient SMTPServer = new System.Net.Mail.SmtpClient(System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPServer"]);
            SMTPServer.Send(Mail);

        }
    }
}