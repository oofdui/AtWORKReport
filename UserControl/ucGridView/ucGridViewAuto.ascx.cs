using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_ucGridView_ucGridViewAuto : System.Web.UI.UserControl
{
    private DataTable _dataSource;
    public DataTable DataSource
    {
        get { return _dataSource; }
        set { _dataSource = value; }
    }

    private string _title="ตารางข้อมูล";
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    private int _pageSize=10;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_dataSource != null && _dataSource.Rows.Count > 0)
            {
                lblTitle.Text = _title;
                gvDefault.PageSize = _pageSize;
                gvDefault.DataSource = _dataSource;
                gvDefault.DataBind();
                gvDefault.BottomPagerRow.Visible = true;
            }
            else
            {
                clsDefault clsDefault = new clsDefault();
                lblMessage.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล", "warn");
                pnGVHeader.Visible = false;
            }
        }
    }
}