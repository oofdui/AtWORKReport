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

/// <summary>
/// Summary description for clsJS
/// </summary>
public class clsJS
{
	public clsJS()
	{
        
	}
	
	public void Confirm(string functionName, string msg)
    {
        //################## Example ####################
        //clsJS.Confirm("ชื่อฟังค์ชัน", "ทดสอบ Popup Confirm แบบ Code Behind");
        //lblLink.Text="<a onClick='return ชื่อฟังค์ชัน()'>เรียก Confirm Function</a>";
        //###############################################

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), functionName))
        {
            strScript.Append("function " + functionName + "() {");
            strScript.Append("$('#loadpage').show();");
            strScript.Append("var confirmed = confirm('" + msg + "');");
            strScript.Append("if (confirmed == false) {");
            strScript.Append("$('#loadpage').hide();");
            strScript.Append("}");
            strScript.Append("return confirmed;");
            strScript.Append("}");

            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), functionName, strScript.ToString(), true);
        }
    }
	
	public void AutoReload(int reloadSecond, int stop_reloadSecond,bool stopReload)
    {
        //########### Example ############
        //AutoReload(30,60,false);
        //Detail :  สั่งให้ทำการ Auto Reload ทุกๆ 30 วินาที
        //          หน้า Design สามารถระบุ onclick = StopReload() ได้ โดยที่จะเปลี่ยนเวลา Auto Reload เป็น 60 วินาทีแทน

        //AutoReload(30,60,true);
        //Detail :  สั่งให้ทำการ Delay การ Reload เป็น 60 วินาที จากเดิม 30 วินาที
        //################################

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        //## แปลงวินาทีให้เป็นหน่วยที่โปรแกรมคำนวนได้ ##
        reloadSecond = reloadSecond * 1000;
        stop_reloadSecond = stop_reloadSecond * 1000;

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "AutoReload"))
        {
            strScript.Append("var ReloadID = setInterval('window.location.reload()'," + reloadSecond + ");");
            strScript.Append("function StopReload(){");
            strScript.Append("clearInterval(ReloadID);");
            strScript.Append("ReloadID = setInterval('window.location.reload()', " + stop_reloadSecond + ");");
            strScript.Append("}");
            strScript.Append("function RestartReload() {");
            strScript.Append("ReloadID = setInterval('window.location.reload()'," + reloadSecond + ");");
            strScript.Append("}");

            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "AutoReload", strScript.ToString(), true);
        }

        if (stopReload)
        {
            if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "AutoReload_Stop"))
            {
                currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "AutoReload_Stop", "StopReload();", true);
            }
        }
    }

    public void Alert(string msg)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "Alert"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "Alert", "alert('" + msg.Replace("'","") + "');", true);
        }
    }

    public void Alert(string msg, string url)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "AlertRedirect"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "AlertRedirect", "alert('" + msg.Replace("'","") + "'); location.href='" + url + "';", true);
        }
    }

    public void SoundPlay(string pathShort)
    {
        clsNet clsNet = new clsNet();
        string strBrowser=clsNet.BrowserGet();
        if (strBrowser != "IE6" && strBrowser != "IE7")
        {
            System.Web.UI.Page currentPage;
            currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "SoundPlay_Regis"))
            {
                System.Text.StringBuilder strScript = new System.Text.StringBuilder();
                strScript.Append("var soundObject = null;");
                strScript.Append("function PlaySound() {");
                strScript.Append("if (soundObject != null) {");
                strScript.Append("document.body.removeChild(soundObject);");
                strScript.Append("soundObject.removed = true;");
                strScript.Append("soundObject = null;");
                strScript.Append("}");
                strScript.Append("soundObject = document.createElement('embed');");
                strScript.Append("soundObject.setAttribute('src', '" + pathShort + "');");
                strScript.Append("soundObject.setAttribute('hidden', true);");
                strScript.Append("soundObject.setAttribute('autostart', true);");
                strScript.Append("document.body.appendChild(soundObject);");
                strScript.Append("}");

                currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "SoundPlay_Regis", strScript.ToString(), true);
            }
            if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "SoundPlay_Call"))
            {
                currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "SoundPlay_Call", "PlaySound();", true);
            }
        }
    }

    public void Import(string strPath)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        //## Import JS ##
        HtmlGenericControl js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = strPath;
        currentPage.Header.Controls.Add(js);
    }

    public void ClickLoading(string ctClientID,string vadGroupName)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();

        strScript.Append("<script type='text/javascript'>");
        strScript.Append("$('#" + ctClientID + "').click(function(){");
        if (string.IsNullOrEmpty(vadGroupName))
        {
            strScript.Append("if(Page_ClientValidate()==true){");
        }
        else
        {
            strScript.Append("if(Page_ClientValidate('" + vadGroupName + "')==true){");
        }
        strScript.Append("$('#" + ctClientID + "').before('<img src=Images/clickLoading.gif/>');");
        strScript.Append("$('#" + ctClientID + "').hide(300);");
        strScript.Append("}");
        strScript.Append("});");
        strScript.Append("</script>");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ClickLoading"))
        {
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "ClickLoading", strScript.ToString());
            strScript = null;
        }
    }

    public void PreLoading()
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();

        //## BindDiv ##
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "PreLoading_BindDiv"))
        {
            System.Text.StringBuilder strDIV = new System.Text.StringBuilder();

            strDIV.Append("<div id='prepage' style='text-align:center;position:fixed;left:0;right:0;top:0;bottom:0;width:100%;height:100%;z-index:1000;background-color:#000000;filter:alpha(opacity=40);opacity:0.5;overflow:auto;'>");
            strDIV.Append("<div style='position:absolute;top: 50%;left: 50%;margin-top:-16px;margin-left:-16px;'>");
            strDIV.Append("<img src='/Images/Animated/anPreLoading.gif' alt=''/>");
            strDIV.Append("</div>");
            strDIV.Append("</div>");

            currentPage.ClientScript.RegisterClientScriptBlock(
                currentPage.GetType(), 
                "PreLoading_BindDiv", 
                strDIV.ToString()
                );
        }

        //## OnLoad ##
        strScript.Append("<script type='text/javascript'>");
        strScript.Append("window.onload = function PreLoading() {");
        strScript.Append("if (document.getElementById)");
        strScript.Append("{");
        strScript.Append("document.getElementById('prepage').style.visibility='hidden';");
        strScript.Append("}");
        strScript.Append("else");
        strScript.Append("{");
        strScript.Append("if (document.layers)");
        strScript.Append("{");
        strScript.Append("document.prepage.visibility = 'hidden';");
        strScript.Append("}");
        strScript.Append("else");
        strScript.Append("{");
        strScript.Append("document.all.prepage.style.visibility = 'hidden';");
        strScript.Append("}");
        strScript.Append("}");
        strScript.Append("}");
        strScript.Append("</script>");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "PreLoading_Onload"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "PreLoading_Onload", strScript.ToString());
            strScript = null;
        }
    }

    public void PreLoadingByClick()
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        //## Bind Div ##
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "PreLoadingByClick_BindDiv"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "PreLoadingByClick_BindDiv", "<div id='loadPage' style='display:none;top:0px;margin:0px;text-align:center;position:absolute;width:100%;height:100%;background-color:#000000;filter:alpha(opacity=40);opacity:0.5;z-index:500;overflow:auto;'><div style='position:absolute;top: 50%;left: 50%;margin-top:-16px;margin-left:-16px;'><img src='Images/preLoading.gif' alt=''/></div></div>");
        }

        //## OnLoad ##
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "PreLoadingByClick_Regis"))
        {
            System.Text.StringBuilder strScript = new System.Text.StringBuilder();
            strScript.Append("$(document).ready(function () {");
            strScript.Append("$('#loadPage').hide();");
            strScript.Append("});");
            strScript.Append("function ifLoaded(){");
            strScript.Append("$('#loadPage').hide();");
            strScript.Append("}");
            strScript.Append("function PreLoading(strValidationGroup){");
            strScript.Append("if (strValidationGroup != ''){");
            strScript.Append("if(Page_ClientValidate(strValidationGroup)==true){");
            strScript.Append("$('#loadPage').show();");
            strScript.Append("}");
            strScript.Append("}");
            strScript.Append("else");
            strScript.Append("{");
            strScript.Append("$('#loadPage').show();");
            strScript.Append("}");
            strScript.Append("}");
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "PreLoadingByClick_Regis", strScript.ToString(), true);
            strScript = null;
        }
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "PreLoadingByClick_Call"))
        {
            //currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "PreLoadingByClick_Call", "showLoading('');", true);
        }
    }
	
	public void ReloadParent(string url)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ReloadParent"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "ReloadParent", "parent.location.href='"+url+"';", true);
        }
    }
}
