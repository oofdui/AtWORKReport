using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ucDate : System.Web.UI.UserControl
{

    /*################## Example ##################
    DropDownList แสดงวันเดือนปี โดยสามารถ Get , Set ได้ ดังนี้
    
    Get: lblDate.Text = ucDate1.DateTime.ToString("yyyy-MM-dd");
         lblDate.Text = ucDate1.Day;
         lblDate.Text = ucDate1.Month;
         lblDate.Text = ucDate1.Year;
     
    Set: ucDate1.DateTime = DateTime.Now;
         ucDate1.Day = DateTime.Now.Day;
         ucDate1.Month = DateTime.Now.Month;
         ucDate1.Year = DateTime.Now.Year;
    ##############################################*/

    clsDefault clsDefault = new clsDefault();

    private int _day;
    private int _month;
    private int _year;

    public int Day
    {
        get { return int.Parse(ddlDay.SelectedItem.Value); }
        set 
        { 
            _day = value;
            try
            {
                ddlDay.SelectedValue = value.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
    public int Month
    {
        get { return int.Parse(ddlMonth.SelectedItem.Value); }
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
        get { return int.Parse(ddlYear.SelectedItem.Value); }
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

    public DateTime DateTime
    {
        get
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (ddlDay.SelectedItem.Value != "null" && ddlMonth.SelectedItem.Value != "null" && ddlYear.SelectedItem.Value != "null")
            {
                return DateTime.Parse(ddlYear.SelectedItem.Value + "-" + ddlMonth.SelectedItem.Value + "-" + ddlDay.SelectedItem.Value);
            }
            else
            {
                return DateTime;
            }
        }
        set
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            ddlDay.SelectedValue = value.Day.ToString();
            ddlMonth.SelectedValue = value.Month.ToString();
            ddlYear.SelectedValue = value.Year.ToString();
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
        ddlDay.Items.Insert(0, new ListItem("-", "null"));
        ddlMonth.Items.Insert(0, new ListItem("-", "null"));
        ddlYear.Items.Insert(0, new ListItem("-", "null"));

        ddlDay.SelectedValue = DateTime.Now.Day.ToString();
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
}