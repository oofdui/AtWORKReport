using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;

/// <summary>
/// Summary description for clsNet
/// </summary>
public class clsNet
{
	public clsNet()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool MailSend(string mail_to, string mail_cc, string mail_bcc, string mail_subject, string mail_body, string mail_signature)
    {
        #region Remark
        /*############################ Example ############################
        ส่งเมล์
        clsNet.MailSend("off_dui@Hotmail.com","","","ทดสอบ","ทดสอบส่งเมล์","");
        #################################################################*/
        #endregion

        if (string.IsNullOrEmpty(mail_to))  //ถ้าไม่ระบุ mail_to ให้คืนค่าและออกจากโปรแกรม
        {
            return false;
        }

        string smtpMail_Host = System.Configuration.ConfigurationManager.AppSettings["smtpMail_Host"];
        string smtpMail_Username = System.Configuration.ConfigurationManager.AppSettings["smtpMail_Username"];
        string smtpMail_Password = System.Configuration.ConfigurationManager.AppSettings["smtpMail_Password"];
        string mail_from = System.Configuration.ConfigurationManager.AppSettings["mail_from"];
        string web_name = System.Configuration.ConfigurationManager.AppSettings["webname"];

        MailMessage myMail = new MailMessage();
        StringBuilder strMailBody = new StringBuilder();

        //Mail From
        myMail.From = new MailAddress(mail_from, web_name);
        // Mail To
        myMail.To.Add(mail_to);

        // Mail Cc
        if (!string.IsNullOrEmpty(mail_cc))
        {
            myMail.CC.Add(mail_cc);
        }

        // Mail Bcc
        if (!string.IsNullOrEmpty(mail_bcc))
        {
            myMail.Bcc.Add(mail_bcc);
        }

        // Mail Subject
        if (!string.IsNullOrEmpty(mail_subject))
        {
            myMail.Subject = mail_subject;
        }
        else
        {
            myMail.Subject = "";
        }

        // Mail Body
        strMailBody.Append("<html>");
        strMailBody.Append("<head></head>");
        strMailBody.Append("<body>");
        if (!string.IsNullOrEmpty(mail_body))
        {
            strMailBody.Append(mail_body);
        }
        // Mail Signature
        if (!string.IsNullOrEmpty(mail_signature))
        {
            strMailBody.Append("<hr/>");
            strMailBody.Append(mail_signature);
        }
        strMailBody.Append("</body>");
        strMailBody.Append("</html>");

        myMail.Body = strMailBody.ToString();

        myMail.IsBodyHtml = true;
        myMail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-874");

        System.Net.NetworkCredential Auth = new System.Net.NetworkCredential(smtpMail_Username, smtpMail_Password);
        SmtpClient smtpMail = new SmtpClient();
        smtpMail.Host = smtpMail_Host;
        smtpMail.UseDefaultCredentials = false;
        smtpMail.Credentials = Auth;
        try
        {
            smtpMail.Send(myMail);
            return true;
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write(ex.Message);
            return false;
        }
    }
	
	public bool MailSend(string mail_to, string mail_cc, string mail_bcc, string mail_subject, string mail_body, string mail_signature,string mail_from,string mail_from_aliasname)
    {
        #region Remark
        /*############################ Example ############################
        ส่งเมล์
        clsNet.MailSend("off_dui@Hotmail.com","","","ทดสอบ","ทดสอบส่งเมล์","","off.dui@gmail.com","Oofdui");
        #################################################################*/
        #endregion

        if (string.IsNullOrEmpty(mail_to))  //ถ้าไม่ระบุ mail_to ให้คืนค่าและออกจากโปรแกรม
        {
            return false;
        }

        string smtpMail_Host = System.Configuration.ConfigurationManager.AppSettings["smtpMail_Host"];
        string smtpMail_Username = System.Configuration.ConfigurationManager.AppSettings["smtpMail_Username"];
        string smtpMail_Password = System.Configuration.ConfigurationManager.AppSettings["smtpMail_Password"];

        MailMessage myMail = new MailMessage();
        StringBuilder strMailBody = new StringBuilder();

        //Mail From
        myMail.From = new MailAddress(mail_from, mail_from_aliasname);
        // Mail To
        myMail.To.Add(mail_to);

        // Mail Cc
        if (!string.IsNullOrEmpty(mail_cc))
        {
            myMail.CC.Add(mail_cc);
        }

        // Mail Bcc
        if (!string.IsNullOrEmpty(mail_bcc))
        {
            myMail.Bcc.Add(mail_bcc);
        }

        // Mail Subject
        if (!string.IsNullOrEmpty(mail_subject))
        {
            myMail.Subject = mail_subject;
        }
        else
        {
            myMail.Subject = "";
        }

        // Mail Body
        strMailBody.Append("<html>");
        strMailBody.Append("<head></head>");
        strMailBody.Append("<body>");
        if (!string.IsNullOrEmpty(mail_body))
        {
            strMailBody.Append(mail_body);
        }
        // Mail Signature
        if (!string.IsNullOrEmpty(mail_signature))
        {
            strMailBody.Append("<hr/>");
            strMailBody.Append(mail_signature);
        }
        strMailBody.Append("</body>");
        strMailBody.Append("</html>");

        myMail.Body = strMailBody.ToString();

        myMail.IsBodyHtml = true;
        myMail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-874");

        System.Net.NetworkCredential Auth = new System.Net.NetworkCredential(smtpMail_Username, smtpMail_Password);
        SmtpClient smtpMail = new SmtpClient();
        smtpMail.Host = smtpMail_Host;
        smtpMail.UseDefaultCredentials = false;
        smtpMail.Credentials = Auth;
        try
        {
            smtpMail.Send(myMail);
            return true;
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write(ex.Message);
            return false;
        }
    }

    public string IPGet()
    {
        #region Remark
        /*############################ Example ############################
        หา IP ของ Client
        clsNet.IPGet();
        #################################################################*/
        #endregion

        string ip;
        ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ip))
        {
            string[] ipRang = ip.Split(',');
            ip = ipRang[ipRang.Length - 1];
        }
        else
        {
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }

    public string ComNameGet()
    {
        #region Remark
        /*############################ Example ############################
        หา Computer Name ของ Client
        clsNet.ComNameGet();
        #################################################################*/
        #endregion

        string[] comname;
        comname = System.Net.Dns.Resolve(System.Web.HttpContext.Current.Request.ServerVariables["remote_addr"].ToString()).HostName.ToString().Split('.');
        return comname[0].ToString().ToUpper();
    }

    public string ComNameGetFull()
    {
        #region Remark
        /*############################ Example ############################
        หา Computer Name แบบชื่อเต็ม ของ Client
        clsNet.ComNameGetFull();
        #################################################################*/
        #endregion

        string comname;
        string ip;
        ip = IPGet();
        try
        {
            comname = System.Net.Dns.GetHostEntry(ip).HostName.ToUpper();
        }
        catch (Exception ex)
        {
            comname = System.Net.Dns.GetHostName().ToString().ToUpper();
        }
        return comname;
    }

    public string ServerNameGet()
    {
        #region Remark
        /*############################ Example ############################
        หา Server Name
        clsNet.ServerNameGet();
        #################################################################*/
        #endregion

        return System.Net.Dns.GetHostName();
    }
	
	public string BrowserGet()
    {
        System.Text.StringBuilder strBrowser = new System.Text.StringBuilder();
        System.Web.HttpBrowserCapabilities browser = System.Web.HttpContext.Current.Request.Browser;
        string strType = browser.Type;

        strBrowser.Append("<div style='font-size:11pt;font-weight:bold;'>Browser Capabilities</div>");
        strBrowser.Append("Type = " + browser.Type); strBrowser.Append("<br/>");
        strBrowser.Append("Name = " + browser.Browser); strBrowser.Append("<br/>");
        strBrowser.Append("Version = " + browser.Version); strBrowser.Append("<br/>");
        strBrowser.Append("Major Version = " + browser.MajorVersion); strBrowser.Append("<br/>");
        strBrowser.Append("Minor Version = " + browser.MinorVersion); strBrowser.Append("<br/>");
        strBrowser.Append("Platform = " + browser.Platform); strBrowser.Append("<br/>");
        strBrowser.Append("Is Beta = " + browser.Beta); strBrowser.Append("<br/>");
        strBrowser.Append("Is Crawler = " + browser.Crawler); strBrowser.Append("<br/>");
        strBrowser.Append("Is AOL = " + browser.AOL); strBrowser.Append("<br/>");
        strBrowser.Append("Is Win16 = " + browser.Win16); strBrowser.Append("<br/>");
        strBrowser.Append("Is Win32 = " + browser.Win32); strBrowser.Append("<br/>");
        strBrowser.Append("Supports Frames = " + browser.Frames); strBrowser.Append("<br/>");
        strBrowser.Append("Supports Tables = " + browser.Tables); strBrowser.Append("<br/>");
        strBrowser.Append("Supports Cookies = " + browser.Cookies); strBrowser.Append("<br/>");
        strBrowser.Append("Supports VBScript = " + browser.VBScript); strBrowser.Append("<br/>");
        strBrowser.Append("Supports JavaScript = " + browser.EcmaScriptVersion.ToString()); strBrowser.Append("<br/>");
        strBrowser.Append("Supports Java Applets = " + browser.JavaApplets); strBrowser.Append("<br/>");
        strBrowser.Append("Supports ActiveX Controls = " + browser.ActiveXControls); strBrowser.Append("<br/>");
        strBrowser.Append("Supports JavaScript Version = " + browser["JavaScriptVersion"]);

        //return strBrowser.ToString();
        return strType;
    }
	
	public bool Ping(string IPAddress)
        {
            #region Remark
            /*############################ Example ############################
            ทดสอบ Ping ไปยัง IP ปลายทาง
            clsNet.Ping();
            #################################################################*/
            #endregion

            bool rtnStatus = false;

            Ping pingSender = new Ping();
            //PingOptions options = new PingOptions();
            //options.DontFragment = true;
            //string data = "Test";
            //byte[] buffer = Encoding.ASCII.GetBytes(data);
            //int timeout = 120;

            try
            {
                //PingReply reply = pingSender.Send(IPAddress, timeout, buffer, options);
                PingReply reply = pingSender.Send(IPAddress);
                if (reply.Status == IPStatus.Success)
                {
                    rtnStatus = true;
                }
            }
            catch (Exception ex)
            {
                //MOSTLY HOST NOT FOUND
            }

            return rtnStatus;
        }
}
