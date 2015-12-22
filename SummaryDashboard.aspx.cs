using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class SummaryDashboard : System.Web.UI.Page
{
    public string pathPhoto = System.Configuration.ConfigurationManager.AppSettings["pathPhoto"];
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";

    protected void Page_Load(object sender, EventArgs e)
    {
        BindSite();
    }

    private void BindSite()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region DataBuilder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID SiteUID,'' GroupsUID,'4' GroupsUIDException,Name,Detail,Sort ");
        strSQL.Append("FROM ");
        strSQL.Append("Site ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("UNION ");
        strSQL.Append("SELECT '' SiteUID,'4' GroupsUID,'' GroupsUIDException,'Innovation' Name,'Innovation' Detail,0 Sort FROM Site ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort,Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            
            dlDefault.DataSource = dt;
            dlDefault.DataBind();
        }
        else
        {
            lblDefault.Text = "ไม่พบข้อมูล";
        }
        #endregion
    }

    public string GetCountSummary(string siteUID, string groupUID, string groupUIDException)
    {
        #region Variable
        string rtnValue = "";
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("S.Name,");
        strSQL.Append("(");
        strSQL.Append("SELECT ");
        strSQL.Append("COUNT(U.UID) ");
        strSQL.Append("FROM ");
        strSQL.Append("Users U ");
        strSQL.Append("WHERE ");
        if (!string.IsNullOrEmpty(groupUID))
        {
            strSQL.Append("U.GroupsUID="+groupUID+" ");
        }
        if (!string.IsNullOrEmpty(groupUIDException))
        {
            strSQL.Append("U.GroupsUID<>" + groupUIDException + " ");
        }
        if (!string.IsNullOrEmpty(siteUID) && siteUID!="0")
        {
            strSQL.Append("AND U.SiteUID="+siteUID+" ");
        }
        strSQL.Append("AND U.GroupsUID<>1 ");
        strSQL.Append("AND (");
        strSQL.Append("SELECT TOP 1 Status.UID ");
        strSQL.Append("FROM UserTrace INNER JOIN [Status] ON UserTrace.StatusUID=[Status].UID ");
        strSQL.Append("WHERE UserTrace.UsersUID=U.UID ");
        strSQL.Append("ORDER BY UserTrace.UID DESC");
        strSQL.Append(")=S.UID");
        strSQL.Append(")CountStaff ");
        strSQL.Append("FROM ");
        strSQL.Append("[Status] S ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("S.Sort");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) rtnValue += " | ";
                rtnValue += dt.Rows[i]["Name"].ToString() + ":<span style='color:#CE7400;font-weight:normal;'>" + dt.Rows[i]["CountStaff"].ToString() + "</span>";
            }
        }
        return rtnValue;
    }
}