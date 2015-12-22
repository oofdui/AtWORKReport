using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class SummaryDashboardDetail : System.Web.UI.UserControl
{
    public string pathPhoto = System.Configuration.ConfigurationManager.AppSettings["pathPhoto"];
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";

    #region Property
    private string _siteUID;
    public string SiteUID
    {
        get { return _siteUID; }
        set { _siteUID = value; }
    }
    private string _groupUID;
    public string GroupUID
    {
        get { return _groupUID; }
        set { _groupUID = value; }
    }
    private string _groupUIDException;
    public string GroupUIDException
    {
        get { return _groupUIDException; }
        set { _groupUIDException = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        BindDetail(_siteUID, _groupUID,_groupUIDException);
    }

    private void BindDetail(string siteUID,string groupUID,string groupUIDException)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region DataBuilder
        #region SQL Query
        strSQL.Append("");
        strSQL.Append("SELECT ");
        strSQL.Append("U.UID,");
        strSQL.Append("U.SiteUID,");
        strSQL.Append("G.Name GroupName,");
        strSQL.Append("U.Name UserName,");
        strSQL.Append("U.Photo,");
        strSQL.Append("(");
        strSQL.Append("SELECT TOP 1 Status.Name ");
        strSQL.Append("FROM UserTrace INNER JOIN [Status] ON UserTrace.StatusUID=[Status].UID ");
        strSQL.Append("WHERE UserTrace.UsersUID=U.UID ");
        strSQL.Append("ORDER BY UserTrace.UID DESC");
        strSQL.Append(")StatusName,");
        strSQL.Append("(");
        strSQL.Append("SELECT Name+' : '+CONVERT(VARCHAR,Detail) FROM Job WHERE Processing='1' AND Active='1' AND UsersUID=U.UID");
        strSQL.Append(") ProcessingName,");
        strSQL.Append("(");
        strSQL.Append("SELECT ResolveType.Name+','+Job.DepartmentID FROM Job ");
        strSQL.Append("INNER JOIN ResolveType ON Job.ResolveTypeUID=ResolveType.UID AND ResolveType.Active='1' ");
        strSQL.Append("WHERE Job.Processing='1' AND Job.Active='1' AND Job.UsersUID=U.UID");
        strSQL.Append(") ResolveType,");
        strSQL.Append("(");
        strSQL.Append("SELECT COUNT(UID) FROM Job WHERE Active='1' AND Complete='0' AND UsersUID=U.UID");
        strSQL.Append(") CountOnHand ");
        strSQL.Append("FROM ");
        strSQL.Append("Users U ");
        strSQL.Append("INNER JOIN Groups G ON U.GroupsUID=G.UID AND G.Active='1' ");
        strSQL.Append("WHERE ");
        strSQL.Append("U.Active='1' ");
        if (!string.IsNullOrEmpty(_siteUID) && _siteUID!="0")
        {
            strSQL.Append("AND U.SiteUID=" + siteUID + " ");
        }
        if (!string.IsNullOrEmpty(_groupUID))
        {
            strSQL.Append("AND U.GroupsUID=" + groupUID + " ");
        }
        if (!string.IsNullOrEmpty(groupUIDException))
        {
            strSQL.Append("AND U.GroupsUID<>" + groupUIDException + " ");
        }
        strSQL.Append("AND G.UID <> 1 ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("G.Sort,U.Sort,U.Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvDefault.DataSource = dt;
            gvDefault.DataBind();
        }
        else
        {
            lblDefault.Text = "ไม่พบข้อมูล";
        }
        #endregion
    }

    public string GetProcessing(string ResolveTypeWithDepartmentID)
    {
        #region Variable
        string rtnValue = "";
        StringBuilder strSQL = new StringBuilder();
        string[] arrValue = ResolveTypeWithDepartmentID.Split(',');
        string cs;
        #endregion

        if (arrValue.Length > 1)
        {
            if (arrValue[0] == "Walk")
            {
                rtnValue = " | <b>Processing</b> : ";
                rtnValue += "<b>Processing</b> : <span style='color:#E6720C;'>";
                rtnValue += "Walk to ";
                switch (_siteUID)
                {
                    case "1":
                        cs = "csCenterBRH";
                        break;
                    case "2":
                        cs = "csCenterBTH";
                        break;
                    case "3":
                        cs = "csCenterBCH";
                        break;
                    case "4":
                        cs = "csCenterBPH";
                        break;
                    default :
                        cs = "";
                        break;
                }
                rtnValue += clsSQL.Return("SELECT dept_name_th FROM DEPARTMENT WHERE dept_id='"+arrValue[1]+"'",dbType,cs);
                rtnValue += "</span>";
            }
            else
            {
                //rtnValue = "<img src='Images/anLoading.GIF'/>";
            }
        }

        return rtnValue;
    }
}