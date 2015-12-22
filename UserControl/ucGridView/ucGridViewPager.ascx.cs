using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class UserControl_ucGridViewPager : System.Web.UI.UserControl
{
    private GridView _gridView;
    private DataTable _dt = new DataTable();
    private string _pageSize;
    public string PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Control c = Parent;
        while (c != null)
        {
            if (c is GridView)
            {
                _gridView = (GridView)c;
                if (ViewState["DataSource"] == null)
                {
                    ViewState["DataSource"] = (DataTable)_gridView.DataSource;
                }
                break;
            }
            c = c.Parent;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (_gridView != null)
        {
            LabelNumberOfPages.Text = _gridView.PageCount.ToString(CultureInfo.CurrentCulture);
            InitPageSize();
            DropDownListPageSize.SelectedValue = _gridView.PageSize.ToString(CultureInfo.CurrentCulture);
            lbTotal.Text = _gridView.Rows.Count.ToString();
            //Add Dropdowlist
            ddlPage.Items.Clear();
            for (var i = 1; i <= (_gridView.PageCount); i++)
            {
                ddlPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            //
            ddlPage.SelectedValue = (_gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
            // 
            if ((_gridView.PageIndex + 1) <= 1)
            {
                ImageButtonFirst.Enabled = false;
                ImageButtonPrev.Enabled = false;
                ImageButtonFirst.ImageUrl = "/UserControl/ucGridView/Images/PgFirstDisable.gif";
                ImageButtonPrev.ImageUrl = "/UserControl/ucGridView/Images/PgPrevDisable.gif";
            }
            else
            {
                ImageButtonFirst.Enabled = true;
                ImageButtonPrev.Enabled = true;
            }
            //
            if (_gridView.PageCount <= (_gridView.PageIndex + 1))
            {
                ImageButtonNext.Enabled = false;
                ImageButtonLast.Enabled = false;
                ImageButtonNext.ImageUrl = "/UserControl/ucGridView/Images/PgNextDisable.gif";
                ImageButtonLast.ImageUrl = "/UserControl/ucGridView/Images/PgLastDisable.gif";
            }
            else
            {
                ImageButtonNext.Enabled = true;
                ImageButtonLast.Enabled = true;
            }
        }
    }

    //Add Event for Dropdownlist
    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_gridView == null)
        {
            return;
        }
        int page;
        if (int.TryParse(ddlPage.SelectedItem.Value.Trim(), out page))
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (page > _gridView.PageCount)
            {
                page = _gridView.PageCount;
            }
            _gridView.PageIndex = page - 1;
        }
        ddlPage.SelectedValue = (_gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
        //Modify By Decha 2011-11-08 fix bug Textchang then Gridview not refresh
        if (ViewState["DataSource"] != null)
        {
            _gridView.DataSource = (DataTable)ViewState["DataSource"];
            _gridView.DataBind();
        }
        if (_gridView.Rows.Count > 0)
        {
            //_gridView.TopPagerRow.Visible = true;
            _gridView.BottomPagerRow.Visible = true;
        }
    }

    protected void DropDownListPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_gridView == null)
        {
            return;
        }
        DropDownList dropdownlistpagersize = (DropDownList)sender;
        _gridView.PageSize = Convert.ToInt32(dropdownlistpagersize.SelectedValue, CultureInfo.CurrentCulture);
        int pageindex = _gridView.PageIndex;
        _gridView.DataSource = (DataTable)ViewState["DataSource"];
        _gridView.DataBind();
        if (_gridView.PageIndex != pageindex)
        {
            //if page index changed it means the previous page was not valid and was adjusted. Rebind to fill control with adjusted page
            _gridView.DataBind();
        }
        if (_gridView.Rows.Count > 0)
        {
            //_gridView.TopPagerRow.Visible = true;
            _gridView.BottomPagerRow.Visible = true;
        }
    }

    protected void NavigateClick(object sender, CommandEventArgs e)
    {
        if (_gridView == null)
        {
            return;
        }

        int page = _gridView.PageIndex;

        switch (e.CommandName)
        {
            case "First":
                if (page > 0)
                {
                    page = 0;
                }
                break;
            case "Prev":
                if (page > 0)
                {
                    page -=1;
                }
                break;
            case "Next":
                if (page < _gridView.PageCount - 1)
                {
                    page += 1;
                }
                break;
            case "Last":
                if (page < _gridView.PageCount - 1)
                {
                    page = _gridView.PageCount - 1;
                }
                break;
            default:
                break;
        }
        
        _gridView.DataSource = (DataTable)ViewState["DataSource"];
        _gridView.PageIndex = page;
        _gridView.DataBind();
        if (_gridView.Rows.Count > 0)
        {
            //_gridView.TopPagerRow.Visible = true;
            _gridView.BottomPagerRow.Visible = true;
        }
    }

    protected void InitPageSize()
    {
        try
        {
            if (_pageSize != "" || !(string.IsNullOrEmpty(_pageSize)))
            {
                string[] strPageSize = Regex.Split(_pageSize, ",");
                DropDownListPageSize.Items.Clear();
                foreach (string size in strPageSize)
                {
                    DropDownListPageSize.Items.Add(new ListItem(size, size));
                }
            }
        }
        catch { }
    }
}