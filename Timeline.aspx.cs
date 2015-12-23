using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Timeline : System.Web.UI.Page
{
    #region GlobalVariable
    public StringBuilder strTimeline = new StringBuilder();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Request.QueryString["date"] == null)
            {
                ucDateJSDate.DateTime = DateTime.Now;
                var url = "Timeline.aspx?date=" + ucDateJSDate.Text +
                    "&user=all" + 
                    "&site=all" + 
                    "&group=all";
                Response.Redirect(url);
            }
            else
            {
                setDefault();
            }
            getSearch();
        }
    }
    private void getSearch()
    {
        var dt = new DataTable();
        var clsSQL = new clsSQL();
        var strSQL = "";
        #region Date
        if(Request.QueryString["date"]!= null)
        {
            ucDateJSDate.Text = Request.QueryString["date"].ToString();
        }
        #endregion
        #region Site
        strSQL = "SELECT UID,Name FROM Site WHERE Active='1' ORDER BY Sort,Name";
        dt = clsSQL.Bind(strSQL, clsSQL.DBType.SQLServer, "cs");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlSite.DataSource = dt;
            ddlSite.DataValueField = "UID";
            ddlSite.DataTextField = "Name";
            ddlSite.DataBind();
            ddlSite.Items.Insert(0, new ListItem("ทั้งหมด", "all"));
            dt = null;
            if(Request.QueryString["site"]!= null)
            {
                if (Request.QueryString["site"].ToString() != "all")
                {
                    ddlSite.SelectedValue = Request.QueryString["site"].ToString();
                }
            }
        }
        strSQL = "";
        #endregion

        #region Group
        strSQL = "SELECT UID,Name FROM Groups WHERE Active='1' ORDER BY Sort,Name";
        dt = clsSQL.Bind(strSQL, clsSQL.DBType.SQLServer, "cs");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlGroup.DataSource = dt;
            ddlGroup.DataValueField = "UID";
            ddlGroup.DataTextField = "Name";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("ทั้งหมด", "all"));
            dt = null;
            if (Request.QueryString["group"] != null)
            {
                if (Request.QueryString["group"].ToString() != "all")
                {
                    ddlGroup.SelectedValue = Request.QueryString["group"].ToString();
                }
            }
        }
        strSQL = "";
        #endregion
        #region User
        strSQL = "SELECT Users.UID,(Users.Name+' ('+Site.Name+')')Name FROM Users INNER JOIN Site ON Users.SiteUID=Site.UID WHERE Users.Active='1' ORDER BY Users.Sort,Users.Name";
        dt = clsSQL.Bind(strSQL, clsSQL.DBType.SQLServer, "cs");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlUser.DataSource = dt;
            ddlUser.DataValueField = "UID";
            ddlUser.DataTextField = "Name";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("ทั้งหมด", "all"));
            dt = null;
            if (Request.QueryString["user"] != null)
            {
                if (Request.QueryString["user"].ToString() != "all")
                {
                    ddlUser.SelectedValue = Request.QueryString["user"].ToString();
                }
            }
        }
        strSQL = "";
        #endregion
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        var url = "Timeline.aspx?date="+ ucDateJSDate.Text + 
            "&user="+ ddlUser.SelectedItem.Value + 
            "&site="+ ddlSite.SelectedItem.Value + 
            "&group="+ ddlGroup.SelectedItem.Value;
        Response.Redirect(url);
    }
    private void setDefault()
    {
        #region Variable
        var clsSQL = new clsSQL();
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        var tempUserUID = "";
        var scheduleClass = 0;
        var mdTimeline = new mdTimeline();
        List<string> userUIDs = new List<string>();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("ST.Name StatusName,R.Name ReasonName,J.Name,J.Detail,U.UsersUID,U.Command,U.CWhen,US.Name UserName,S.Name SiteName, US.Photo UserPhoto ");
        strSQL.Append("FROM ");
        strSQL.Append("UserTrace U ");
        strSQL.Append("INNER JOIN Users US ON U.UsersUID = US.UID AND US.Active = '1' ");
        strSQL.Append("INNER JOIN Site S ON US.SiteUID=S.UID AND S.Active='1' ");
        strSQL.Append("INNER JOIN Status ST ON U.StatusUID=ST.UID AND ST.Active='1' ");
        //strSQL.Append("INNER JOIN Job J ON U.JobUID = J.UID AND J.Active = '1' ");
        strSQL.Append("LEFT JOIN Job J ON U.JobUID = J.UID AND J.Active = '1' ");
        strSQL.Append("LEFT JOIN Reason R ON U.ReasonUID = R.UID AND R.Active = '1' ");
        strSQL.Append("WHERE ");
        strSQL.Append("U.Command<>'' AND U.Command<>'AUTO' ");
        if (Request.QueryString["date"] != null)
        {
            strSQL.Append("AND CONVERT(DATE,U.CWhen) = '" + Request.QueryString["date"].ToString().Trim() + "' ");
        }
        else
        {
            strSQL.Append("AND CONVERT(DATE,U.CWhen) = CONVERT(DATE,GETDATE()) ");
        }
        if (Request.QueryString["user"] != null)
        {
            if (Request.QueryString["user"].ToString() != "all")
            {
                strSQL.Append("AND U.UsersUID = " + Request.QueryString["user"].ToString() + " ");
            }
        }
        if (Request.QueryString["site"] != null)
        {
            if (Request.QueryString["site"].ToString() != "all")
            {
                strSQL.Append("AND US.SiteUID = " + Request.QueryString["site"].ToString() + " ");
            }
        }
        if (Request.QueryString["group"] != null)
        {
            if (Request.QueryString["group"].ToString() != "all")
            {
                strSQL.Append("AND US.GroupsUID = " + Request.QueryString["group"].ToString() + " ");
            }
        }
        strSQL.Append("ORDER BY U.UID,U.CWhen;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, "cs");
        if (dt != null && dt.Rows.Count > 0)
        {
            #region SELECT Distinct
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["UsersUID"].ToString() != tempUserUID)
                {
                    userUIDs.Add(dt.Rows[i]["UsersUID"].ToString());
                }
                tempUserUID = dt.Rows[i]["UsersUID"].ToString();
            }
            #endregion
            for (int i = 0; i < userUIDs.Count; i++)
            {
                scheduleClass += 1;
                if (scheduleClass > 10) scheduleClass = 1;
                mdTimeline.Clear();
                DataRow[] rows = dt.Select("UsersUID=" + userUIDs[i]);
                strTimeline.Append("{");
                strTimeline.Append("'name':'" + rows[0]["UserName"].ToString() + " ["+rows[0]["SiteName"].ToString()+"]',");
                strTimeline.Append("'appointments':[");

                for (int r = 0; r < rows.Count(); r++)
                {
                    if (r==0 && rows[r]["Command"].ToString().ToUpper() != "START")
                    {
                        //ถ้าข้อมูลแรกของพนักงานท่านนี้ ไม่ใช่สถานะ START (แสดงว่าเปิดงานเมื่อวานแล้วลืมปิด) ให้ข้ามไป
                        continue;
                    }
                    if (rows[r]["StatusName"].ToString().ToUpper() == "BREAK")
                    {
                        mdTimeline.cssClass = "reason";
                        mdTimeline.Name = "Break";
                        mdTimeline.Detail = rows[r]["ReasonName"].ToString();
                    }
                    else
                    {
                        if(rows[r]["Detail"].ToString() != "")
                        {
                            mdTimeline.Name = (rows[r]["Name"].ToString() != "" ? rows[r]["Name"].ToString().Replace("'","\"") : "[-]");
                            mdTimeline.Detail = rows[r]["Detail"].ToString().Replace("'", "\"");
                        }
                    }
                    mdTimeline.UserUID = rows[r]["UsersUID"].ToString();
                    mdTimeline.UserName = rows[r]["UserName"].ToString();
                    mdTimeline.UserPhoto = rows[r]["UserPhoto"].ToString();
                    if (rows[r]["Command"].ToString().ToUpper() == "START")
                    {
                        mdTimeline.StartWhen = rows[r]["CWhen"].ToString();
                    }
                    else if (rows[r]["Command"].ToString().ToUpper() == "STOP" || rows[r]["Command"].ToString().ToUpper() == "HOLD")
                    {
                        mdTimeline.EndWhen = rows[r]["CWhen"].ToString();
                    }

                    if (mdTimeline.UserUID != "" && mdTimeline.UserName != "" && mdTimeline.UserPhoto != "" && mdTimeline.Name != "" && mdTimeline.Detail != "" && mdTimeline.StartWhen != "" && mdTimeline.EndWhen != "")
                    {
                        strTimeline.Append("{'start':'" + DateTime.Parse(mdTimeline.StartWhen).ToString("HH:mm:ss") + "',");
                        strTimeline.Append("'end':'" + DateTime.Parse(mdTimeline.EndWhen).ToString("HH:mm:ss") + "',");
                        strTimeline.Append("'title':'" + mdTimeline.Name + " : " + mdTimeline.Detail.Replace(Environment.NewLine, " ") + "',");
                        if (mdTimeline.cssClass != "")
                        {
                            strTimeline.Append("'class': '" + mdTimeline.cssClass + scheduleClass.ToString() + "' },");
                        }
                        else
                        {
                            strTimeline.Append("'class': 'schedule" + scheduleClass.ToString() + "' },");
                        }
                        mdTimeline.Clear();
                    }
                    else if (r == rows.Count() - 1)
                    {
                        strTimeline.Append("{'start':'" + (mdTimeline.StartWhen!=""?DateTime.Parse(mdTimeline.StartWhen).ToString("HH:mm:ss"):"00:00") + "',");
                        strTimeline.Append("'end':'" + DateTime.Now.ToString("HH:mm:ss") + "',");
                        strTimeline.Append("'title':'" + mdTimeline.Name + " : " + mdTimeline.Detail.Replace(Environment.NewLine, " ") + "',");
                        if (mdTimeline.cssClass != "")
                        {
                            strTimeline.Append("'class': '" + mdTimeline.cssClass + scheduleClass.ToString() + "' },");
                        }
                        else
                        {
                            strTimeline.Append("'class': 'schedule" + scheduleClass.ToString() + "' },");
                        }
                        mdTimeline.Clear();
                    }
                }
                strTimeline.Append("]");
                strTimeline.Append("},");
            }
        }
        else
        {

        }
        #endregion
    }
}