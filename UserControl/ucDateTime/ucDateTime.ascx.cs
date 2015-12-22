using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class UserControl_ucDateTime : System.Web.UI.UserControl
{
    /*################## Example ##################
    DropDownList แสดงวันเดือนปี ชั่วโมง:นาที 
    
    <uc3:ucDateTime ID="ucDateTime1" runat="server" Enable="true"/>
    
    ucDateTime1.DateTime = DateTime.Now.AddHours(2);
    ##############################################*/

    clsDefault clsDefault = new clsDefault();

    private int _day;
    private int _month;
    private int _year;
    private int _hour;
    private int _minute;
    private bool _enable;

    public int Day
    {
        get 
        {
            if (chkEnable.Checked)
            {
                return int.Parse(ddlDay.SelectedItem.Value);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            _day = value;
            try
            {
                ddlDay.SelectedValue = value.ToString();
            }
            catch (Exception ex){}
        }
    }
    public int Month
    {
        get 
        {
            if (chkEnable.Checked)
            {
                return int.Parse(ddlMonth.SelectedItem.Value); 
            }
            else
            {
                return 0;
            }
        }
        set
        {
            _month = value;
            try
            {
                ddlMonth.SelectedValue = value.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
    public int Year
    {
        get 
        {
            if (chkEnable.Checked)
            {
                return int.Parse(ddlYear.SelectedItem.Value);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            _year = value;
            try
            {
                ddlYear.SelectedValue = value.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
    public int Hour
    {
        get 
        {
            if (chkEnable.Checked)
            {
                return int.Parse(ddlHour.SelectedItem.Value);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            _hour = value;
            try
            {
                ddlHour.SelectedValue = value.ToString();
            }
            catch (Exception ex) { }
        }
    }
    public int Minute
    {
        get 
        {
            if (chkEnable.Checked)
            {
                return int.Parse(ddlMinute.SelectedItem.Value);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            _minute = value;
            try
            {
                ddlMinute.SelectedValue = value.ToString();
            }
            catch (Exception ex) { }
        }
    }
    public DateTime DateTime
    {
        get 
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (chkEnable.Checked)
            {
                if (ddlDay.SelectedItem.Value != "null" && ddlMonth.SelectedItem.Value != "null" && ddlYear.SelectedItem.Value != "null" &&
                ddlHour.SelectedItem.Value != "null" && ddlMinute.SelectedItem.Value != "null")
                {
                    return DateTime.Parse(ddlYear.SelectedItem.Value + "-" + ddlMonth.SelectedItem.Value + "-" + ddlDay.SelectedItem.Value + " " + ddlHour.SelectedItem.Value + ":" + ddlMinute.SelectedItem.Value);
                }
                else
                {
                    return new DateTime();
                }
            }
            else
            {
                return new DateTime();
            }
        }
        set
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            ddlDay.SelectedValue = value.Day.ToString();
            ddlMonth.SelectedValue = value.Month.ToString();
            ddlYear.SelectedValue = value.Year.ToString();
            ddlHour.SelectedValue = value.Hour.ToString();
            ddlMinute.SelectedValue = value.Minute.ToString();
        }
    }
    public bool Enable
    {
        get
        {
            return chkEnable.Checked;
        }
        set
        {
            _enable = value;
            chkEnable.Checked = _enable;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        base.OnInit(e);
        int i;

        for (i = 1; i <= 31; i++)
        {
            ddlDay.Items.Add(new ListItem(clsDefault.Right("0" + i.ToString(), 2), i.ToString()));
        }
        for (i = 1; i <= 12; i++)
        {
            ddlMonth.Items.Add(new ListItem(clsDefault.MonthName(i), i.ToString()));
        }
        for (i = DateTime.Now.AddYears(-60).Year; i <= DateTime.Now.AddYears(10).Year; i++)
        {
            ddlYear.Items.Add(i.ToString());
        }
        for (i = 0; i < 24; i++)
        {
            ddlHour.Items.Add(new ListItem(clsDefault.Right("0" + i.ToString(), 2), i.ToString()));
        }
        for (i = 0; i < 60; i++)
        {
            ddlMinute.Items.Add(new ListItem(clsDefault.Right("0" + i.ToString(), 2), i.ToString()));
        }
        ddlDay.Items.Insert(0, new ListItem("-", "null"));
        ddlMonth.Items.Insert(0, new ListItem("-", "null"));
        ddlYear.Items.Insert(0, new ListItem("-", "null"));
        ddlHour.Items.Insert(0, new ListItem("-", "null"));
        ddlMinute.Items.Insert(0, new ListItem("-", "null"));

        ddlDay.SelectedValue = DateTime.Now.Day.ToString();
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
        ddlMinute.SelectedValue = DateTime.Now.Minute.ToString();

        chkEnable.Attributes.Add("onclick", "UserControlChecker_" + this.ClientID+"()");
    }
}