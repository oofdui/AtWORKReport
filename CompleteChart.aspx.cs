using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class CompleteChart : System.Web.UI.Page
{
    clsSQL clsSQL = new clsSQL();
    clsChart clsChart = new clsChart();
    clsDefault clsDefault = new clsDefault();
    clsData clsData = new clsData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DailyBuilder();
            MonthlyBuilder();
        }
    }

    private void DailyBuilder()
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        DataTable dtDept = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("(Name+' ('+");
        strSQL.Append("(");
        strSQL.Append("SELECT TOP 1 Status.Name ");
        strSQL.Append("FROM UserTrace INNER JOIN Status ON UserTrace.StatusUID=Status.UID ");
        strSQL.Append("WHERE UserTrace.UsersUID=U.UID ");
        strSQL.Append("ORDER BY UserTrace.UID DESC");
        strSQL.Append(")");
        strSQL.Append("+')')Name,");
        strSQL.Append("(");
        strSQL.Append("SELECT COUNT(UID) ");
        strSQL.Append("FROM Job ");
        strSQL.Append("WHERE UsersUID=U.UID AND Complete='0' ");
        strSQL.Append("AND CONVERT(DATE,CWhen)=CONVERT(DATE,GETDATE())");
        strSQL.Append(")CountProcess,");
        strSQL.Append("(");
        strSQL.Append("SELECT COUNT(UID) ");
        strSQL.Append("FROM Job ");
        strSQL.Append("WHERE UsersUID=U.UID AND Complete='1' ");
        strSQL.Append("AND CONVERT(DATE,CWhen)=CONVERT(DATE,GETDATE()) ");
        strSQL.Append("AND CONVERT(DATE,MWhen)=CONVERT(DATE,GETDATE())");
        strSQL.Append(")CountComplete ");
        strSQL.Append("FROM ");
        strSQL.Append("Users U ");
        strSQL.Append("WHERE ");
        strSQL.Append("SiteUID=1 ");
        strSQL.Append("AND GroupsUID=3 ");
        strSQL.Append("AND Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("U.Name;");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, "cs");

        if (dt != null && dt.Rows.Count > 0)
        {
            //dt.DefaultView.Sort = "CountAll DESC";
            //dt = dt.DefaultView.ToTable();

            string[] strCategory = new string[dt.Rows.Count];
            string[] strValue = new string[2];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strCategory[i] = dt.Rows[i]["Name"].ToString();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue[0] += dt.Rows[i]["CountComplete"].ToString();
                if (i < dt.Rows.Count - 1) strValue[0] += ",";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue[1] += dt.Rows[i]["CountProcess"].ToString();
                if (i < dt.Rows.Count - 1) strValue[1] += ",";
            }

            ucChart2.ChartUID = "Daily";
            ucChart2.ChartType = ucChart.Type.BarHorizontalStack;
            ucChart2.Title = "Complete Job by Daily";
            ucChart2.SubTitle = "สรุปจำนวนใบงานที่ดำเนินการเสร็จ และ อยู่ในมือ ในวันนี้";
            ucChart2.YName = "จำนวนงาน";
            ucChart2.YUnit = "งาน";
            ucChart2.XNames = strCategory;
            ucChart2.SeriesName = new string[] { "Complete","Process" };
            ucChart2.SeriesValues = strValue;
            //ucChart2.TargetName = "ห้ามเลย";
            //ucChart2.TargetColor = "#FFB10B";
            ucChart2.ShowTooltip = true;
            ucChart2.ShowLabel = true;
            ucChart2.ShowLegend = true;
        }
        else
        {
            chartDialy.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล", "warn", "Images/Icon/");
        }
    }

    private void MonthlyBuilder()
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        DataTable dtDept = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("(Name+' ('+");
        strSQL.Append("(");
        strSQL.Append("SELECT TOP 1 Status.Name ");
        strSQL.Append("FROM UserTrace INNER JOIN Status ON UserTrace.StatusUID=Status.UID ");
        strSQL.Append("WHERE UserTrace.UsersUID=U.UID ");
        strSQL.Append("ORDER BY UserTrace.UID DESC");
        strSQL.Append(")");
        strSQL.Append("+')')Name,");
        strSQL.Append("(");
        strSQL.Append("SELECT COUNT(UID) ");
        strSQL.Append("FROM Job ");
        strSQL.Append("WHERE UsersUID=U.UID AND Complete='1' ");
        strSQL.Append("AND DATEPART(MONTH,CWhen)=DATEPART(MONTH,GETDATE()) ");
        strSQL.Append("AND DATEPART(MONTH,MWhen)=DATEPART(MONTH,GETDATE())");
        strSQL.Append(")CountComplete ");
        strSQL.Append("FROM ");
        strSQL.Append("Users U ");
        strSQL.Append("WHERE ");
        strSQL.Append("SiteUID=1 ");
        strSQL.Append("AND GroupsUID=3 ");
        strSQL.Append("AND Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("U.Name;");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, "cs");

        if (dt != null && dt.Rows.Count > 0)
        {
            //dt.DefaultView.Sort = "CountAll DESC";
            //dt = dt.DefaultView.ToTable();

            string[] strCategory = new string[dt.Rows.Count];
            string[] strValue = new string[1];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strCategory[i] = dt.Rows[i]["Name"].ToString();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue[0] += dt.Rows[i]["CountComplete"].ToString();
                if (i < dt.Rows.Count - 1) strValue[0] += ",";
            }

            /*
            chartMonthly.Text = clsChart.BarHorizontalStack("ID2",
                 "Complete Job by Monthly",
                 "สรุปจำนวนใบงานที่ดำเนินการเสร็จในเดือนปัจจุบัน",
                 "จำนวนใบงาน",
                 "งาน",
                 new string[] { "Complete"},
                 strCategory,
                 strValue);
            */

            ucChart1.ChartUID = "Monthly";
            ucChart1.ChartType = ucChart.Type.BarHorizontal;
            ucChart1.Title = "Complete Job by Monthly";
            ucChart1.SubTitle = "สรุปจำนวนใบงานที่ดำเนินการเสร็จในเดือนปัจจุบัน";
            ucChart1.YName = "จำนวนงาน";
            ucChart1.YUnit = "งาน";
            ucChart1.XNames = strCategory;
            ucChart1.SeriesName = new string[] { "Complete"};
            ucChart1.SeriesValues = strValue;
            ucChart1.ShowTooltip = true;
            ucChart1.ShowLabel = true;
            ucChart1.ShowLegend = true;
        }
        else
        {
            chartMonthly.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล", "warn", "Images/Icon/");
        }
    }
}