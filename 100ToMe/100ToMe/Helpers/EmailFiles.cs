using _100ToMe.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _100ToMe.Helpers
{
    public class EmailFiles
    {
        public static async Task EnviarAsync(List<Files> files, string email, string name)
        {
            try
            {

                //var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;   
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("felipeffcwb@gmail.com", "100toMe");
                var to = new EmailAddress(email, name);


                var subject = "Teste Files";

                string corpoEmail = "<p>" + "Testando envio de arquivos" + "</p>";

                //var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", corpoEmail);
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
                foreach (Files item in files)
                {
                    var path = File.ReadAllBytes(item.FilePath + item.Name);
                    var file = Convert.ToBase64String(path);
                    msg.AddAttachment(item.Name, file);
                }

                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
