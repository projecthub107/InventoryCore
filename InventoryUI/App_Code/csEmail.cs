using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for csEmail
/// </summary>
public class csEmail
{
	public csEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void SendMail(string @from, string to, string cc, string bcc, string subject, string body)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"); // System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"];//

        mail.From = new MailAddress("etor.soft2@gmail.com");
        mail.To.Add(to);

        if (!string.IsNullOrEmpty(cc))
            mail.CC.Add(cc);
        if (!string.IsNullOrEmpty(bcc))
            mail.Bcc.Add(bcc);

        mail.Subject = subject;
        //body = body.Replace("{UserName}", userName);
        //body = body.Replace("{Title}", title);
        //body = body.Replace("{Url}", url);
        string html = string.Empty;


        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/emailTemplate.html")))
        {
            html = reader.ReadToEnd();
        };
        body = html.Replace("{Description}", body);
        mail.Body = body;
        mail.BodyEncoding = UTF8Encoding.UTF8;
        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        mail.IsBodyHtml = true;


        System.Net.Mail.Attachment attachment;
        //  attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
        // mail.Attachments.Add(attachment);

        SmtpClient ss = new SmtpClient("smtp.gmail.com", 587);
        ss.EnableSsl = true;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("etor.soft2@gmail.com", "e@rs0ft_007");
        ss.Send(mail);
    }
}