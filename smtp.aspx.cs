using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.IO;

public partial class smtp : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AppendHeader("Access-Control-Allow-Origin", "*");
        string strFrom = Request.QueryString["From"];
        string strTo = Request.QueryString["To"];
        string strSubject = Request.QueryString["Subject"];
        string strBody = Request.QueryString["Body"];
        string strHost = Request.QueryString["Host"];
        string strUsername = Request.QueryString["Username"];
        string strPassword = Request.QueryString["Password"];
        string strAction = Request.QueryString["Action"];
        string strSecureToken = Request.QueryString["SecureToken"];
        string strAttachment = Request.QueryString["Attachment"];

        switch (strAction)
        {
            case "Send":
                    var strStatus = "OK";
                    if (String.IsNullOrEmpty(strAttachment))
                    {
                        strStatus = SendEmail(  strUsername,
                                                strPassword,
                                                strHost,
                                                strTo,
                                                strFrom,
                                                strSubject,
                                                strBody);
                    }
                    else
                    {
                        strStatus = SendEmailWithAttachment(strUsername,
                                                strPassword,
                                                strHost,
                                                strTo,
                                                strFrom,
                                                strSubject,
                                                strBody,
                                                new Uri(strAttachment));
                    }
                    Response.Write(strStatus);
                break;
        }
    }


    private string SendEmail(string Username, string Password, string Host, string To, string From, string Subject, string Body)
    {
        MailMessage mail = new MailMessage(From, To);
        SmtpClient client = new SmtpClient();
        client.Port = 25;
        client.EnableSsl = true;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential(Username, Password);
        client.Host = Host;
        mail.Subject = Subject;
        mail.Headers.Add("X-PLEASE-REPORT-ANY-SPAM", "Is this spam?, please report to http://www.smtpjs.com/#spam");
        mail.Body = Body;
        try
        {
            client.Send(mail);
            return "OK";
        }
        catch(Exception ex)
        {
            return ex.Message;
        }
    }

    private string SendEmailWithAttachment(string Username, string Password, string Host, string To, string From, string Subject, string Body, Uri Attachment)
    {
        WebClient wc = new WebClient();
        var bAttachment = wc.DownloadData(Attachment);
        var strName = Attachment.Segments.Last();
        MailMessage mail = new MailMessage(From, To);
        SmtpClient client = new SmtpClient();
        client.Port = 25;
        client.EnableSsl = true;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential(Username, Password);
        client.Host = Host;
        mail.Subject = Subject;
        mail.Headers.Add("X-PLEASE-REPORT-ANY-SPAM", "Is this spam?, please report to http://www.smtpjs.com/#spam");
        mail.Body = Body;
        mail.Attachments.Add(new Attachment(new MemoryStream(bAttachment), strName));
        try
        {
            client.Send(mail);
            return "OK";
        }
        catch(Exception ex)
        {
            return ex.Message;
        }
    }

}
