using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class JobReport : System.Web.UI.Page
{
    public string pathPhoto = System.Configuration.ConfigurationManager.AppSettings["pathPhoto"];
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    DataTable dtDeptBRH = new DataTable();
    DataTable dtDeptBCH = new DataTable();
    DataTable dtDeptBTH = new DataTable();
    DataTable dtDeptBPH = new DataTable();
    DataTable dtDeptBPL = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["usern"] == null)
            {
                pnDefault.Visible = false;
                lblDefaultWarn.Text = clsDefault.AlertMessageColor("กรุณาล็อนอินผ่าน BDMS App Store ก่อนเข้าใช้งาน", "warn", "Images/Icon/");
            }
            else
            {
                pnDefault.Visible = true;
                lblDefaultWarn.Text = "";
            }
            BindSearchControl();
            BindSearch(true);
            ucDateJSFrom.DateTime = DateTime.Now;
            ucDateJSTo.DateTime = DateTime.Now;
            /*
            lblDefault.Text = "<div style='padding:10px;border:1px solid #ddd;background-color:#fff;'>" +
                   clsDefault.AlertMessageColor("โปรดเลือกเงื่อนไข", "info", "Images/Icon/") +
                   "</div>";
             */
        }
    }

    private void BindSearchControl()
    {
        string strSQL;
        DataTable dt = new DataTable();

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
        }
        strSQL = "";
        #endregion

        #region Category
        strSQL = "SELECT UID,Name FROM JobCategory WHERE Active='1' ORDER BY Sort,Name";
        dt = clsSQL.Bind(strSQL, clsSQL.DBType.SQLServer, "cs");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlCategory.DataSource = dt;
            ddlCategory.DataValueField = "UID";
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("ทั้งหมด", "all"));
            dt = null;
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
        }
        strSQL = "";
        #endregion

        #region ResolveType
        strSQL = "SELECT UID,Name,Detail FROM ResolveType WHERE Active='1' ORDER BY Sort,Name;";
        dt = clsSQL.Bind(strSQL, clsSQL.DBType.SQLServer, "cs");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlResolveType.DataSource = dt;
            ddlResolveType.DataValueField = "UID";
            ddlResolveType.DataTextField = "Name";
            ddlResolveType.DataBind();
            ddlResolveType.Items.Insert(0, new ListItem("ทั้งหมด", "all"));
            dt = null;
        }
        strSQL = "";
        #endregion
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        BindSearch();
    }

    private void BindSearch(bool IsToday=false)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Job.UID,");
        strSQL.Append("Site.Name Site,Users.Name Username,ISNULL(Users.Photo,'Default.png') Photo,Groups.Name GroupName,");
        strSQL.Append("RequestType.Name RequestType,RequestType.Detail RequestTypeDetail,");
        strSQL.Append("ResolveType.Name ResolveType,ResolveType.Detail ResolveTypeDetail,");
        strSQL.Append("JobCategory.Name JobCategory,");
        strSQL.Append("Job.Name JobName,Job.ReferenceID JobReferenceID,Job.Detail JobDetail,Job.CWhen CreateDate,Job.MWhen CloseDate,");
        strSQL.Append("Job.DepartmentID,Job.Processing,Job.Complete ");
        strSQL.Append("FROM ");
        strSQL.Append("Job ");
        strSQL.Append("INNER JOIN Users ON Job.UsersUID=Users.UID ");
        strSQL.Append("INNER JOIN Site ON Users.SiteUID=Site.UID ");
        if (ddlSite.SelectedItem.Value != "all")
        {
            strSQL.Append("AND Site.UID='" + ddlSite.SelectedItem.Value + "' ");
        }
        strSQL.Append("INNER JOIN RequestType ON Job.RequestTypeUID=RequestType.UID ");
        strSQL.Append("INNER JOIN ResolveType ON Job.ResolveTypeUID=ResolveType.UID ");
        strSQL.Append("INNER JOIN Groups ON Users.GroupsUID=Groups.UID ");
        if (ddlGroup.SelectedItem.Value != "all")
        {
            strSQL.Append("AND Groups.UID='" + ddlGroup.SelectedItem.Value + "' ");
        }
        strSQL.Append("LEFT JOIN JobCategory ON Job.JobCategoryUID=JobCategory.UID ");
        if (ddlCategory.SelectedItem.Value != "all")
        {
            strSQL.Append("AND JobCategory.UID='" + ddlCategory.SelectedItem.Value + "' ");
        }
        strSQL.Append("WHERE ");
        strSQL.Append("Job.Active='1' ");
        if (ddlUser.SelectedItem.Value != "all")
        {
            strSQL.Append("AND Job.UsersUID='" + ddlUser.SelectedItem.Value + "' ");
        }
        if (ddlResolveType.SelectedItem.Value != "all")
        {
            strSQL.Append("AND Job.ResolveTypeUID=" + ddlResolveType.SelectedItem.Value + " ");
        }
        //strSQL.Append("--AND Job.Processing='1' ");
        if (IsToday)
        {
            strSQL.Append("AND Job.MWhen >= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' ");
            strSQL.Append("AND Job.MWhen < '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00' ");
        }
        else
        {
            if (ucDateJSFrom.Text != "")
            {
                strSQL.Append("AND Job.MWhen >= '" + ucDateJSFrom.DateTime.ToString("yyyy-MM-dd") + " 00:00:00' ");
            }
            if (ucDateJSTo.Text != "")
            {
                strSQL.Append("AND Job.MWhen < '" + ucDateJSTo.DateTime.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00' ");
            }
        }
        if (ddlJobStatus.SelectedItem.Value == "Complete")
        {
            strSQL.Append("AND Job.Complete='1' ");
        }
        else if (ddlJobStatus.SelectedItem.Value == "OnProcess")
        {
            strSQL.Append("AND Job.Processing='1' ");
        }
        else if (ddlJobStatus.SelectedItem.Value == "OnHold")
        {
            strSQL.Append("AND Job.Complete='0' ");
            strSQL.Append("AND Job.Processing='0' ");
        }
        strSQL.Append("ORDER BY ");
        strSQL.Append("Job.MWhen ASC");
        #endregion

        try
        {
            dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, "cs");
            //lblDefault.Text = strSQL.ToString(); return;
            if (dt != null && dt.Rows.Count > 0)
            {
                lblJobCount.Text = "<span style='color:#ED7615;padding-left:10px;'>พบข้อมูลทั้งหมด " + dt.Rows.Count.ToString() + " รายการ</span>";
                gvDefault.Visible = true; lblDefault.Text = "";
                gvDefault.DataSource = dt;
                gvDefault.DataBind();
            }
            else
            {
                lblJobCount.Text = "<span style='color:#ED7615;padding-left:10px;'>พบข้อมูลทั้งหมด 0 รายการ</span>";
                lblDefault.Text = "<div style='padding:10px;border:1px solid #ddd;background-color:#fff;'>" +
                       clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", "warn", "Images/Icon/") +
                       "</div>";
                gvDefault.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblDefault.Text = clsDefault.AlertMessageColor("<b>เกิดข้อผิดพลาดขณะรันคำสั่ง</b> : "+strSQL.ToString(),"fail","Images/Icon/");
        }
    }

    public string GetDepartment(string siteCode, string deptCode)
    {
        string rtnValue = "";

        if (deptCode != "")
        {
            try
            {
                switch (siteCode)
                {
                    case "BRH":
                        if (dtDeptBRH == null || dtDeptBRH.Rows.Count == 0)
                        {
                            try
                            {
                                dtDeptBRH = clsSQL.Bind("SELECT dept_id,dept_name_th FROM DEPARTMENT", clsSQL.DBType.SQLServer, "csCenterBRH");
                            }
                            catch (Exception ex) { return ex.Message; }
                        }
                        if (dtDeptBRH != null && dtDeptBRH.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDeptBRH.Rows.Count; i++)
                            {
                                if (dtDeptBRH.Rows[i]["dept_id"].ToString() == deptCode)
                                {
                                    rtnValue = dtDeptBRH.Rows[i]["dept_name_th"].ToString();
                                    break;
                                }
                            }
                        }
                        break;
                    case "BCH":
                        if (dtDeptBCH == null || dtDeptBCH.Rows.Count == 0)
                        {
                            try
                            {
                                dtDeptBCH = clsSQL.Bind("SELECT dept_id,dept_name_th FROM DEPARTMENT", clsSQL.DBType.SQLServer, "csCenterBCH");
                            }
                            catch (Exception ex) { return ex.Message; }
                        }
                        if (dtDeptBCH != null && dtDeptBCH.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDeptBCH.Rows.Count; i++)
                            {
                                if (dtDeptBCH.Rows[i]["dept_id"].ToString() == deptCode)
                                {
                                    rtnValue = dtDeptBCH.Rows[i]["dept_name_th"].ToString();
                                    break;
                                }
                            }
                        }
                        break;
                    case "BTH":
                        if (dtDeptBTH == null || dtDeptBTH.Rows.Count == 0)
                        {
                            try
                            {
                                dtDeptBTH = clsSQL.Bind("SELECT dept_id,dept_name_th FROM DEPARTMENT", clsSQL.DBType.SQLServer, "csCenterBTH");
                            }
                            catch (Exception ex) { return ex.Message; }
                        }
                        if (dtDeptBTH != null && dtDeptBTH.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDeptBTH.Rows.Count; i++)
                            {
                                if (dtDeptBTH.Rows[i]["dept_id"].ToString() == deptCode)
                                {
                                    rtnValue = dtDeptBTH.Rows[i]["dept_name_th"].ToString();
                                    break;
                                }
                            }
                        }
                        break;
                    case "BPH":
                        if (dtDeptBPH == null || dtDeptBPH.Rows.Count == 0)
                        {
                            try
                            {
                                dtDeptBPH = clsSQL.Bind("SELECT dept_id,dept_name_th FROM DEPARTMENT", clsSQL.DBType.SQLServer, "csCenterBPH");
                            }
                            catch (Exception ex) { return ex.Message; }
                        }
                        if (dtDeptBPH != null && dtDeptBPH.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDeptBPH.Rows.Count; i++)
                            {
                                if (dtDeptBPH.Rows[i]["dept_id"].ToString() == deptCode)
                                {
                                    rtnValue = dtDeptBPH.Rows[i]["dept_name_th"].ToString();
                                    break;
                                }
                            }
                        }
                        break;
                    case "BPL":
                        if (dtDeptBPL == null || dtDeptBPL.Rows.Count == 0)
                        {
                            try
                            {
                                dtDeptBPL = clsSQL.Bind("SELECT dept_id,dept_name_th FROM DEPARTMENT", clsSQL.DBType.SQLServer, "csCenterBPL");
                            }
                            catch (Exception ex) { return ex.Message; }
                        }
                        if (dtDeptBPL != null && dtDeptBPL.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDeptBPL.Rows.Count; i++)
                            {
                                if (dtDeptBPL.Rows[i]["dept_id"].ToString() == deptCode)
                                {
                                    rtnValue = dtDeptBPL.Rows[i]["dept_name_th"].ToString();
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                rtnValue = "Exception: "+ex.Message;
            }
        }

        return rtnValue;
    }
}