using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    clsSQL clsSQL = new clsSQL();
    clsChart clsChart = new clsChart();
    clsDefault clsDefault = new clsDefault();
    clsData clsData = new clsData();
    string isDaily = "0";
    string isDate = "";

    /// <summary>
    /// วิธีเข้าโปรแกรม มีหลายโหมด ดังนี้
    /// Default.aspx    แสดงทั้งหมด
    /// Default.aspx?date=2013-12-01    แสดงเฉพาะวันที่กำหนด
    /// Default.aspx?daily=1    แสดงวันปัจจุบัน
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["daily"] != null)
            {
                if (Request.QueryString["daily"].ToString() == "1")
                {
                    isDaily = "1";
                    isDate = "";
                }
            }
            if (Request.QueryString["date"] != null)
            {
                isDate = Request.QueryString["date"].ToString();
                isDaily = "0";
            }

            BindDefault();
            BindDepartmentReport();
            BindCategoryReport();
        }
    }

    private void BindDefault()
    {
        chartCount.Text = "<span style='color:#1babb4;'>จำนวนงานทั้งหมด</span> : " + 
            clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1' "+
            (isDaily == "1" ? "AND CAST(CWhen AS DATE)=CAST(GETDATE() AS DATE) " : "")+
            (isDate != "" ? "AND CAST(CWhen AS DATE)='"+isDate+"' " : ""), 
            clsSQL.DBType.SQLServer, "cs");
        if (isDaily == "1") chartCount.Text += "<span style='color:#606060;font-size:12pt;padding-left:10px;'>(วันที่ " + DateTime.Now.ToString("dd/MM/yyyy") + ")</span>";
        if (isDate != "") chartCount.Text += "<span style='color:#606060;font-size:12pt;padding-left:10px;'>(วันที่ " + DateTime.Parse(isDate).ToString("dd/MM/yyyy") + ")</span>";

        chartCountDetail.Text += "<span style='color:#8ed952;'>Complete</span> : " + 
            clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1' AND Complete='1' " +
            (isDaily == "1" ? "AND CAST(CWhen AS DATE)=CAST(GETDATE() AS DATE)" : "")+
            (isDate != "" ? "AND CAST(CWhen AS DATE)='" + isDate + "' " : ""), 
            clsSQL.DBType.SQLServer, "cs");
        chartCountDetail.Text += " | ";
        chartCountDetail.Text += "<span style='color:#db4a2e;'>OnHold</span> : " + 
            clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1' AND Processing='0' AND Complete='0' "+
            (isDaily == "1" ? "AND CAST(CWhen AS DATE)=CAST(GETDATE() AS DATE)" : "") +
            (isDate != "" ? "AND CAST(CWhen AS DATE)='" + isDate + "' " : ""), 
            clsSQL.DBType.SQLServer, "cs");
        chartCountDetail.Text += " | ";
        chartCountDetail.Text += "<span style='color:#e4c80d;'>OnProcessing</span> : " + 
            clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1' AND Processing='1' "+
            (isDaily == "1" ? "AND CAST(CWhen AS DATE)=CAST(GETDATE() AS DATE)" : "") +
            (isDate != "" ? "AND CAST(CWhen AS DATE)='" + isDate + "' " : ""), 
            clsSQL.DBType.SQLServer, "cs");

    }

    private void BindDepartmentReport()
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        DataTable dtDept = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("DepartmentID,COUNT(UID) As JobCount ");
        strSQL.Append("FROM ");
        strSQL.Append("Job ");
        strSQL.Append("WHERE ");
        strSQL.Append("DepartmentID<>'' ");
        strSQL.Append("AND Active='1' ");
        strSQL.Append(isDaily == "1" ? "AND CAST(CWhen AS DATE)=CAST(GETDATE() AS DATE) " : "");
        strSQL.Append(isDate != "" ? "AND CAST(CWhen AS DATE)='" + isDate + "' " : "");
        strSQL.Append("GROUP BY ");
        strSQL.Append("DepartmentID ");

        strSQL.Append("UNION ALL ");
        strSQL.Append("SELECT '0' as DepartmentID,COUNT(UID) FROM Job WHERE DepartmentID ='' ");
        strSQL.Append(isDaily == "1" ? "AND CAST(CWhen AS DATE)=CAST(GETDATE() AS DATE) " : "");
        strSQL.Append(isDate != "" ? "AND CAST(CWhen AS DATE)='" + isDate + "' " : "");
        
        strSQL.Append("ORDER BY ");
        strSQL.Append("COUNT(UID) DESC ");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, "cs");
        dtDept = clsSQL.Bind("SELECT dept_id As DepartmentID,dept_name_th As DepartmentName FROM Department WHERE dept_active='1' UNION ALL SELECT TOP 1 '0' as DepartmentID,'No Choose' as DepartmentName FROM Department", clsSQL.DBType.SQLServer, "csCenter");

        if (dt != null && dt.Rows.Count > 0)
        {
            dt = clsData.JoinTwoDataTable(dt, dtDept, "DepartmentID", clsData.JoinType.Left);
            string[] strDept = new string[dt.Rows.Count];
            StringBuilder strCount = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strDept[i] = dt.Rows[i]["DepartmentName"].ToString();
                strCount.Append(dt.Rows[i]["JobCount"].ToString());
                if (i < dt.Rows.Count - 1)
                {
                    strCount.Append(",");
                }
            }

            chartDepartment.Text = clsChart.BarHorizontal("ID",
                 "B-Connect Problem by Department",
                 "สรุปจำนวนใบงานแยกตามข้อมูลแผนก",
                 "จำนวนใบงาน",
                 "งาน",
                 new string[] { "จำนวนใบงาน" },
                 strDept,
                 new string[] { strCount.ToString()});
        }
        else
        {
            chartDepartment.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล", "warn", "Images/Icon/");
        }
    }

    private void BindCategoryReport()
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        DataTable dtDept = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("JobCategory.Name,COUNT(Job.UID)As JobCount ");
        strSQL.Append("FROM ");
        strSQL.Append("Job ");
        strSQL.Append("INNER JOIN JobCategory ON Job.JobCategoryUID=JobCategory.UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("Job.Active='1' ");
        strSQL.Append("AND JobCategory.Active='1' ");
        strSQL.Append("AND NOT Job.JobCategoryUID IS NULL ");
        strSQL.Append(isDaily == "1" ? "AND CAST(Job.CWhen AS DATE)=CAST(GETDATE() AS DATE) " : "");
        strSQL.Append(isDate != "" ? "AND CAST(Job.CWhen AS DATE)='" + isDate + "' " : "");
        strSQL.Append("GROUP BY ");
        strSQL.Append("Job.JobCategoryUID,JobCategory.Name ");

        strSQL.Append("UNION ALL ");
        strSQL.Append("SELECT 'No Choose' As Name,COUNT(UID) FROM Job WHERE JobCategoryUID IS NULL ");
        strSQL.Append(isDaily == "1" ? "AND CAST(Job.CWhen AS DATE)=CAST(GETDATE() AS DATE) " : "");
        strSQL.Append(isDate != "" ? "AND CAST(Job.CWhen AS DATE)='" + isDate + "' " : "");

        strSQL.Append("ORDER BY ");
        strSQL.Append("COUNT(Job.UID) DESC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, "cs");

        if (dt != null && dt.Rows.Count > 0)
        {
            string[] strCategory = new string[dt.Rows.Count];
            StringBuilder strCount = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strCategory[i] = dt.Rows[i]["Name"].ToString();
                strCount.Append("{y:" + dt.Rows[i]["JobCount"].ToString() + ",color:'#50B432'}");
                if (i < dt.Rows.Count - 1)
                {
                    strCount.Append(",");
                }
            }

            chartCategory.Text = clsChart.BarHorizontal("ID2",
                 "B-Connect Problem by Category",
                 "สรุปจำนวนใบงานแยกตามประเภทใบงาน",
                 "จำนวนใบงาน",
                 "งาน",
                 new string[] { "จำนวนใบงาน" },
                 strCategory,
                 new string[] { strCount.ToString() });
        }
        else
        {
            chartCategory.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล", "warn", "Images/Icon/");
        }
    }
}