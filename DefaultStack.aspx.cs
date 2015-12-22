using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class DefaultStack : System.Web.UI.Page
{
    clsSQL clsSQL = new clsSQL();
    clsChart clsChart = new clsChart();
    clsDefault clsDefault = new clsDefault();
    clsData clsData = new clsData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDefault();
            //BindDepartmentReport();
            BindCategoryReport();
        }
    }

    private void BindDefault()
    {
        chartCount.Text = "<span style='color:#1babb4;'>จำนวนงานทั้งหมด</span> : " + clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1'", clsSQL.DBType.SQLServer, "cs");
        chartCountDetail.Text += "<span style='color:#8ed952;'>Complete</span> : " + clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1' AND Complete='1'", clsSQL.DBType.SQLServer, "cs");
        chartCountDetail.Text += " | ";
        chartCountDetail.Text += "<span style='color:#db4a2e;'>OnHold</span> : " + clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1' AND Processing='0' AND Complete='0'", clsSQL.DBType.SQLServer, "cs");
        chartCountDetail.Text += " | ";
        chartCountDetail.Text += "<span style='color:#e4c80d;'>OnProcessing</span> : " + clsSQL.Return("SELECT COUNT(UID) FROM Job WHERE Job.Active='1' AND Processing='1'", clsSQL.DBType.SQLServer, "cs");

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
        strSQL.Append("GROUP BY ");
        strSQL.Append("DepartmentID ");

        strSQL.Append("UNION ALL ");
        strSQL.Append("SELECT '0' as DepartmentID,COUNT(UID) FROM Job WHERE DepartmentID ='' ");

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
                 new string[] { strCount.ToString() });
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
        strSQL.Append("JC.Name,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID=JC.UID AND Job.Active='1')As CountAll,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID=JC.UID AND Job.Active='1' AND Job.Complete='1')As CountComplete,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID=JC.UID AND Job.Active='1' AND Job.Complete='0' AND Job.Processing='0')As CountOnHold,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID=JC.UID AND Job.Active='1' AND Job.Complete='0' AND Job.Processing='1')As CountOnProcessing ");
        strSQL.Append("FROM ");
        strSQL.Append("JobCategory JC ");
        strSQL.Append("WHERE ");
        strSQL.Append("JC.Active='1' ");
        strSQL.Append("AND (SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID=JC.UID AND Job.Active='1')>0 ");
        strSQL.Append("UNION ALL ");
        strSQL.Append("SELECT TOP 1 ");
        strSQL.Append("'No Choose' As Name,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID IS NULL AND Job.Active='1')As CountAll,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID IS NULL AND Job.Active='1' AND Job.Complete='1')As CountComplete,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID IS NULL AND Job.Active='1' AND Job.Complete='0' AND Job.Processing='0')As CountOnHold,");
        strSQL.Append("(SELECT COUNT(UID) FROM Job WHERE Job.JobCategoryUID IS NULL AND Job.Active='1' AND Job.Complete='0' AND Job.Processing='1')As CountOnProcessing ");
        strSQL.Append("FROM Job");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, "cs");

        if (dt != null && dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = "CountAll DESC";
            dt = dt.DefaultView.ToTable();

            string[] strCategory = new string[dt.Rows.Count];
            string[] strValue = new string[4];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strCategory[i] = dt.Rows[i]["Name"].ToString();
                //strCount.Append("{y:" + dt.Rows[i]["JobCount"].ToString() + ",color:'#50B432'}");
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue[0] += dt.Rows[i]["CountAll"].ToString();
                if (i < dt.Rows.Count - 1) strValue[0] += ",";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue[1] += dt.Rows[i]["CountComplete"].ToString();
                if (i < dt.Rows.Count - 1) strValue[1] += ",";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue[2] += dt.Rows[i]["CountOnHold"].ToString();
                if (i < dt.Rows.Count - 1) strValue[2] += ",";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue[3] += dt.Rows[i]["CountOnProcessing"].ToString();
                if (i < dt.Rows.Count - 1) strValue[3] += ",";
            }

            chartCategory.Text = clsChart.BarHorizontalStack("ID2",
                 "B-Connect Problem by Category",
                 "สรุปจำนวนใบงานแยกตามประเภทใบงาน",
                 "จำนวนใบงาน",
                 "งาน",
                 new string[] { "All","Complete","OnHold","OnProcessing" },
                 strCategory,
                 strValue);
        }
        else
        {
            chartCategory.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล", "warn", "Images/Icon/");
        }
    }
}