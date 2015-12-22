using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class UserControl_ucDateTime_ucDateTimeJS : System.Web.UI.UserControl
{
    private DateTime _dateTime;
    public DateTime DateTime
    {
        get 
        {
            if (!string.IsNullOrEmpty(txtDateTime.Text.Trim()))
            {
                _dateTime = System.DateTime.Parse(txtDateTime.Text.Trim());
            }
            return _dateTime;
        }
        set
        {
            _dateTime = value;
            txtDateTime.Text = _dateTime.ToString("yyyy-MM-dd HH:mm");
        }
    }

    private string _text;
    public string Text
    {
        get 
        {
            _text = txtDateTime.Text.Trim();
            return _text; 
        }
        set 
        {
            _text = value;
            txtDateTime.Text = _text;
        }
    }


    private bool _validateRequire=false;
    public bool ValidateRequire
    {
        get { return _validateRequire; }
        set { _validateRequire = value; }
    }

    private string _validateGroup;
    public string ValidateGroup
    {
        get { return _validateGroup; }
        set { _validateGroup = value; }
    }

    private string _errorMessage;
    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }

    private bool _monthChange = true;
    public bool MonthChange
    {
        get { return _monthChange; }
        set { _monthChange = value; }
    }

    private bool _yearChange = true;
    public bool YearChange
    {
        get { return _yearChange; }
        set { _yearChange = value; }
    }

    private string _yearRange = "-50:+5";
    public string YearRange
    {
        get { return _yearRange; }
        set { _yearRange = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //ImportJQuery();
        vldDateTime.Enabled = _validateRequire;
        if (!string.IsNullOrEmpty(_validateGroup))
        {
            vldDateTime.ValidationGroup = _validateGroup;
        }

        if (!string.IsNullOrEmpty(_errorMessage))
        {
            vldDateTime.ErrorMessage = _errorMessage;
        }
    }

    public void ImportJQuery()
    {
        HtmlGenericControl js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = "/UserControl/ucDateTime/Plugin/jQuery/jquery-1.10.2.min.js";
        Page.Header.Controls.Add(js);
    }
}