using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ucChart : System.Web.UI.UserControl
{
    #region Example
    /*
        ##### Normal Chart #####
        ucChart1.ChartType = ucChart.Type.BarVertical;
        ucChart1.Title = "ทดสอบ";
        ucChart1.SubTitle = "ซัปไตเติ้ล";
        ucChart1.YName = "จำนวนงาน";
        ucChart1.YUnit = "ชิ้น";
        ucChart1.XNames = new string[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน" };
        ucChart1.SeriesName = new string[] { "อ๊อฟ", "พี่พงษ์", "พี่ซื่อ", "พี่อาร์ม" };
        ucChart1.SeriesValues = new string[] { "10,5,20,30", "1,8,2,0", "4,4,4,4", "10,9,20,1" };
        ucChart1.SeriesValueColor = new string[] { "", "", "", "#FF525E"};
        ucChart1.SeriesValueText = new string[]{"ชื่อ 1","ชื่อ 2","",""};
        ucChart1.TargetValue = "18";
        ucChart1.TargetName = "ห้ามเลย";
        ucChart1.TargetColor = "#FFB10B";
        ucChart1.ShowTooltip = false;
        ucChart1.ShowLabel = false;
        ucChart1.ShowLegend = false;
    */
    /*
        ##### Pie Chart #####
        ucChart1.Title = "ทดสอบ";
        ucChart1.ChartType = UserControl_ucChart_ucChart.Type.PieChartSemiCircle;
        ucChart1.Parameters = new string[,] { { "Google", "35" }, { "GooDesign", "55" }, { "GooGig", "10" } }; 
    */
    #endregion
    public enum Type
    {
        LineBasic, BarHorizontal, BarVertical, PieChartSemiCircle,BarHorizontalStack
    }

    #region Property
    private Type _chartType=Type.LineBasic;
    public Type ChartType
    {
        get { return _chartType; }
        set { _chartType = value; }
    }

    private string _chartUID = "UID";
    public string ChartUID
    {
        get { return _chartUID; }
        set { _chartUID = value; }
    }

    private string _title = "TITLE";
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    private string _subTitle = "";
    public string SubTitle
    {
        get { return _subTitle; }
        set { _subTitle = value; }
    }

    private string _yName = "ชื่อแกน Y";
    public string YName
    {
        get { return _yName; }
        set { _yName = value; }
    }

    private string _yUnit = "หน่วยแกน Y";
    public string YUnit
    {
        get { return _yUnit; }
        set { _yUnit = value; }
    }

    private string[] _seriesName;
    public string[] SeriesName
    {
        get { return _seriesName; }
        set { _seriesName = value; }
    }

    private string[] _xNames;
    public string[] XNames
    {
        get { return _xNames; }
        set { _xNames = value; }
    }

    private string[] _seriesValues;
    public string[] SeriesValues
    {
        get { return _seriesValues; }
        set { _seriesValues = value; }
    }

    private string[] _seriesValueColor;
    public string[] SeriesValueColor
    {
        get { return _seriesValueColor; }
        set { _seriesValueColor = value; }
    }

    private string[] _seriesValueText;
    public string[] SeriesValueText
    {
        get { return _seriesValueText; }
        set { _seriesValueText = value; }
    }

    private bool _showLabel = true;
    public bool ShowLabel
    {
        get { return _showLabel; }
        set { _showLabel = value; }
    }

    private bool _showTooltip = true;
    public bool ShowTooltip
    {
        get { return _showTooltip; }
        set { _showTooltip = value; }
    }

    private bool _showLegend=true;
    public bool ShowLegend
    {
        get { return _showLegend; }
        set { _showLegend = value; }
    }
    
    private string[,] _parameters;
    public string[,] Parameters
    {
        get { return _parameters; }
        set { _parameters = value; }
    }

    private string _targetValue;
    public string TargetValue
    {
        get { return _targetValue; }
        set { _targetValue = value; }
    }

    private string _targetName="Target";
    public string TargetName
    {
        get { return _targetName; }
        set { _targetName = value; }
    }

    private string _targetColor="#FF3B3B";
    public string TargetColor
    {
        get { return _targetColor; }
        set { _targetColor = value; }
    }

    private bool _visible=true;
    public bool Visible
    {
        get { return _visible; }
        set { _visible = value; }
    }

    private bool _enable=true;
    public bool Enable
    {
        get { return _enable; }
        set { _enable = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Variable
        string strChart = "";
        #endregion

        if (_visible && _enable)
        {
            #region Switch Chart Type
            switch (_chartType)
            {
                case Type.LineBasic:
                    if (!DataChecker(out strChart))
                    {
                        goto Output;
                    }
                    strChart = LineBasic(_chartUID,
                        _title,
                        _subTitle,
                        _yName,
                        _yUnit,
                        _seriesName,
                        _xNames,
                        _seriesValues,
                        _targetValue,
                        _targetName,
                        _targetColor);
                    break;
                case Type.BarHorizontal:
                    if (!DataChecker(out strChart))
                    {
                        goto Output;
                    }
                    strChart = BarHorizontal(_chartUID,
                        _title,
                        _subTitle,
                        _yName,
                        _yUnit,
                        _seriesName,
                        _xNames,
                        _seriesValues,
                        _targetValue,
                        _targetName,
                        _targetColor);
                    break;
                case Type.BarVertical:
                    if (!DataChecker(out strChart))
                    {
                        goto Output;
                    }
                    strChart = BarVertical(_chartUID,
                        _title,
                        _subTitle,
                        _yName,
                        _yUnit,
                        _seriesName,
                        _xNames,
                        _seriesValues,
                        _targetValue,
                        _targetName,
                        _targetColor);
                    break;
                case Type.PieChartSemiCircle:
                    if (!DataAreaChecker(out strChart))
                    {
                        goto Output;
                    }
                    strChart = PieChartSemiCircle(_chartUID,
                        _title,
                        _parameters);
                    break;
                case Type.BarHorizontalStack:
                    if (!DataChecker(out strChart))
                    {
                        goto Output;
                    }
                    strChart = BarHorizontalStack(_chartUID,
                        _title,
                        _subTitle,
                        _yName,
                        _yUnit,
                        _seriesName,
                        _xNames,
                        _seriesValues,
                        _targetValue,
                        _targetName,
                        _targetColor);
                    break;
                default:
                    break;
            }
            #endregion
        }

        Output:
        lblChart.Text = strChart;
    }

    /// <summary>
    /// ตรวจสอบข้อมูล Input สำหรับสร้าง Line Chart , Bar Chart
    /// </summary>
    /// <param name="outMessage"></param>
    /// <returns></returns>
    private bool DataChecker(out string outMessage)
    {
        #region Variable
        clsDefault clsDefault = new clsDefault();
        bool rtnValue = true;
        outMessage = "";
        #endregion

        if (_xNames == null || _xNames.Length == 0)
        {
            //outMessage = clsDefault.AlertMessageColor("โปรดระบุค่า XNames", clsDefault.AlertType.Fail);
            return false;
        }
        if (_seriesName == null || _seriesName.Length == 0)
        {
            //outMessage = clsDefault.AlertMessageColor("โปรดระบุค่า SeriesName", clsDefault.AlertType.Fail);
            return false;
        }
        if (_seriesValues == null || _seriesValues.Length == 0)
        {
            //outMessage = clsDefault.AlertMessageColor("โปรดระบุค่า SeriesValues", clsDefault.AlertType.Fail);
            return false;
        }
        if (_seriesName.Length != _seriesValues.Length)
        {
            //outMessage = clsDefault.AlertMessageColor("จำนวนตัวแปร SeriesName (" + _seriesName.Length.ToString() + ") กับ SeriesValues (" + _seriesValues.Length.ToString() + ") ต้องเท่ากัน", clsDefault.AlertType.Fail);
            return false;
        }

        if (_xNames.Length != _seriesValues[0].Split(',').Length)
        {
            //outMessage = clsDefault.AlertMessageColor("จำนวนตัวแปร XName (" + _xNames.Length.ToString() + ") กับจำนวนค่าย่อยใน SeriesValues (" + _seriesValues[0].Split(',').Length + ") ต้องเท่ากัน", clsDefault.AlertType.Fail);
            return false;
        }

        return rtnValue;
    }

    /// <summary>
    /// ตรวจสอบข้อมูล Input สำหรับสร้าง Pie Chart
    /// </summary>
    /// <param name="outMessage"></param>
    /// <returns></returns>
    private bool DataAreaChecker(out string outMessage)
    {
        #region Variable
        clsDefault clsDefault = new clsDefault();
        bool rtnValue = true;
        outMessage = "";
        #endregion

        if (_parameters == null || _parameters.Length == 0)
        {
            //outMessage = clsDefault.AlertMessageColor("โปรดระบุค่า Parameter", clsDefault.AlertType.Fail);
            return false;
        }

        return rtnValue;
    }

    private string LineBasic(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue, string targetValue, string targetName, string targetColor)
    {
        #region Example
        /*################# Example #################
        lblChart.Text = clsChart.LineBasic("ID", 
            "หัวข้อหลัก", 
            "หัวข้อย่อย", 
            "ข้อความแกน y", 
            "หน่วยแกน y", 
            new string[] { "Series 1", "Series 2" }, 
            new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
            new string[] { "10,5,2", "1,7,12" });
        */
        #endregion

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        string chartName = "chart" + chart_id;

        strScript.Append("$(function() {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {renderTo: '" + chartName + "',");
        strScript.Append("type: 'line'},");
        strScript.Append("title: {text: '" + title + "'},");
        strScript.Append("subtitle: {text: '" + subtitle + "'},");
        strScript.Append("xAxis: {");
        strScript.Append("categories: [");

        //########### xCategory ##########
        for (int i = 0; i < xCategory.Length; i++)
        {
            strScript.Append("'" + xCategory[i] + "'");
            if (i < xCategory.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("]},yAxis: {title: {text: '" + yTitle + "'},");
        if (!string.IsNullOrEmpty(targetValue))
        {
            strScript.Append("plotLines: [{value: " + targetValue + ",width: 1,color: '" + targetColor + "',zIndex:4,label:{text:'" + targetName + "'}}]},");
            //strScript.Append(",plotLines:[{value:" + targetValue + ",color: '" + targetColor + "',width:1,zIndex:4,label:{text:'" + targetName + "'}}]");
        }
        //strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + ",formatter: function() {return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + ' " + valueUnit + "';}},");
        strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + "},");
        strScript.Append("plotOptions: {series: {dataLabels: {enabled: " + _showLabel.ToString().ToLower() + ",formatter:function(){return this.point.name;}}}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -10,y: 100,borderWidth: 0},");
        strScript.Append("legend: {enabled: " + _showLegend.ToString().ToLower() + "},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            #region Data
            if (_seriesValueColor != null || _seriesValueText != null)
            {
                string[] seriesValues = seriesValue[i].Split(',');
                if (seriesValues.Length == _seriesValueColor.Length || seriesValues.Length == _seriesValueText.Length)
                {
                    strScript.Append("data: [");

                    for (int d = 0; d < seriesValues.Length; d++)
                    {
                        if (d > 0) strScript.Append(",");

                        strScript.Append("{");
                        if (_seriesValueColor != null)
                        {
                            if (_seriesValueColor[d] != "")
                            {
                                strScript.Append("color:'" + _seriesValueColor[d] + "',");
                            }
                        }
                        if (_seriesValueText != null)
                        {
                            if (_seriesValueText[d] != "")
                            {
                                strScript.Append("name:'" + _seriesValueText[d] + "',");
                            }
                        }
                        strScript.Append("y:" + seriesValues[d] + "");
                        strScript.Append("}");
                    }

                    strScript.Append("]");
                }
                else
                {
                    strScript.Append("data: [" + seriesValue[i] + "]");
                }
            }
            else
            {
                strScript.Append("data: [" + seriesValue[i] + "]");
            }
            #endregion
            strScript.Append("}");
            if (i < seriesName.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("]});});});");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), chartName))
        {
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), chartName, strScript.ToString(), true);
        }
        strScript = null;

        return "<div id='" + chartName + "' style='min-width: 400px; min-height: 400px; margin: 0 auto'></div>";
    }

    private string BarHorizontal(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue, string targetValue, string targetName, string targetColor)
    {
        #region Example
        /*################# Example #################
        lblChart.Text = clsChart.BarHorizontal("ID", 
            "หัวข้อหลัก", 
            "หัวข้อย่อย", 
            "ข้อความแกน y", 
            "หน่วยแกน y", 
            new string[] { "Series 1", "Series 2" }, 
            new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
            new string[] { "10,5,2", "1,7,12" });
        */
        #endregion

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        string chartName = "chart" + chart_id;

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {renderTo: '" + chartName + "',type: 'bar'},");
        strScript.Append("title: {text: '" + title + "'},");
        strScript.Append("subtitle: {text: '" + subtitle + "'},");
        strScript.Append("xAxis: {categories: [");

        //########### xCategory ##########
        for (int i = 0; i < xCategory.Length; i++)
        {
            strScript.Append("'" + xCategory[i] + "'");
            if (i < xCategory.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("],title: {text: null}},");
        strScript.Append("yAxis: ");
        strScript.Append("{min: 0,title: {text: '" + yTitle + "',align: 'high'},labels: {overflow: 'justify'}");
        if (!string.IsNullOrEmpty(targetValue))
        {
            strScript.Append(",plotLines:[{value:" + targetValue + ",color: '" + targetColor + "',width:1,zIndex:4,label:{text:'" + targetName + "'}}]");
        }
        strScript.Append("},");
        //strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + ",formatter: function() {return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + ' " + valueUnit + "';}},");
        strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + "},");
        //strScript.Append("plotOptions: {series: {dataLabels: {enabled: " + _showLabel.ToString().ToLower() + ",formatter:function(){return this.point.name;}}}},");
        strScript.Append("plotOptions: {bar: {dataLabels: {enabled: " + _showLabel.ToString().ToLower() + "}},series: {stacking: 'normal'}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -100,y: 100,floating: true,borderWidth: 1,backgroundColor: '#FFFFFF',shadow: true},");
        strScript.Append("legend: {enabled: " + _showLegend.ToString().ToLower() + "},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            #region Data
            if (_seriesValueColor != null || _seriesValueText != null)
            {
                string[] seriesValues = seriesValue[i].Split(',');
                if (seriesValues.Length == _seriesValueColor.Length || seriesValues.Length == _seriesValueText.Length)
                {
                    strScript.Append("data: [");

                    for (int d = 0; d < seriesValues.Length; d++)
                    {
                        if (d > 0) strScript.Append(",");

                        strScript.Append("{");
                        if (_seriesValueColor != null)
                        {
                            if (_seriesValueColor[d] != "")
                            {
                                strScript.Append("color:'" + _seriesValueColor[d] + "',");
                            }
                        }
                        if (_seriesValueText != null)
                        {
                            if (_seriesValueText[d] != "")
                            {
                                strScript.Append("name:'" + _seriesValueText[d] + "',");
                            }
                        }
                        strScript.Append("y:" + seriesValues[d] + "");
                        strScript.Append("}");
                    }

                    strScript.Append("]");
                }
                else
                {
                    strScript.Append("data: [" + seriesValue[i] + "]");
                }
            }
            else
            {
                strScript.Append("data: [" + seriesValue[i] + "]");
            }
            #endregion
            strScript.Append("}");
            if (i < seriesName.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("]});});});");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), chartName))
        {
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), chartName, strScript.ToString(), true);
        }
        strScript = null;

        return "<div id='" + chartName + "' style='min-width: 400px; min-height: 400px; margin: 0 auto'></div>";
    }

    private string BarVertical(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue, string targetValue,string targetName, string targetColor)
    {
        #region Example
        /*################# Example #################
        lblChart.Text = clsChart.BarVertical("ID", 
            "หัวข้อหลัก", 
            "หัวข้อย่อย", 
            "ข้อความแกน y", 
            "หน่วยแกน y", 
            new string[] { "Series 1", "Series 2" }, 
            new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
            new string[] { "10,5,2", "1,7,12" });
        */
        #endregion

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        string chartName = "chart" + chart_id;

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {renderTo: '" + chartName + "',type: 'column'},");
        strScript.Append("title: {text: '" + title + "'},");
        strScript.Append("subtitle: {text: '" + subtitle + "'},");
        strScript.Append("xAxis: {categories: [");

        //########### xCategory ##########
        for (int i = 0; i < xCategory.Length; i++)
        {
            strScript.Append("'" + xCategory[i] + "'");
            if (i < xCategory.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("],title: {text: null}},");
        strScript.Append("yAxis: ");
        strScript.Append("{min: 0,title: {text: '" + yTitle + "'},labels: {overflow: 'justify'}");
        if (!string.IsNullOrEmpty(targetValue))
        {
            strScript.Append(",plotLines:[{value:"+targetValue+",color: '"+targetColor+"',width:1,zIndex:4,label:{text:'"+targetName+"'}}]");
        }
        strScript.Append("},");
        //strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + ",formatter: function() {return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + ' " + valueUnit + "';}},");
        strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + "},");
        strScript.Append("plotOptions: {series: {dataLabels: {enabled: " + _showLabel.ToString().ToLower() + ",formatter:function(){return this.point.name;}}}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -100,y: 100,floating: true,borderWidth: 1,backgroundColor: '#FFFFFF',shadow: true},");
        strScript.Append("legend: {enabled: " + _showLegend.ToString().ToLower()+ "},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [");

        //########### Series ###########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            //strScript.Append("color:'#F7E230',");
            strScript.Append("name: '" + seriesName[i] + "',");
            #region Data
            if (_seriesValueColor != null || _seriesValueText!=null)
            {
                string[] seriesValues = seriesValue[i].Split(',');
                if (seriesValues.Length == _seriesValueColor.Length || seriesValues.Length==_seriesValueText.Length)
                {
                    strScript.Append("data: [");

                    for (int d = 0; d < seriesValues.Length; d++)
                    {
                        if (d > 0) strScript.Append(",");

                        strScript.Append("{");
                        if (_seriesValueColor!=null)
                        {
                            if (_seriesValueColor[d] != "")
                            {
                                strScript.Append("color:'" + _seriesValueColor[d] + "',");
                            }
                        }
                        if (_seriesValueText != null)
                        {
                            if (_seriesValueText[d] != "")
                            {
                                strScript.Append("name:'" + _seriesValueText[d] + "',");
                            }
                        }
                        strScript.Append("y:" + seriesValues[d] + "");
                        strScript.Append("}");
                    }

                    strScript.Append("]");
                }
                else
                {
                    strScript.Append("data: [" + seriesValue[i] + "]");
                }
            }
            else
            {
                strScript.Append("data: [" + seriesValue[i] + "]");
            }
            #endregion
            strScript.Append("}");
            if (i < seriesName.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("]});});});");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), chartName))
        {
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), chartName, strScript.ToString(), true);
        }
        strScript = null;

        return "<div id='" + chartName + "' style='min-width: 400px; min-height: 400px; margin: 0 auto'></div>";
    }

    /// <summary>
    /// สร้าง Pie Chart ครึ่งวงกลม
    /// </summary>
    /// <param name="ChartID">UID ของ Chart</param>
    /// <param name="Title">ชื่อ Chart</param>
    /// <param name="Parameters">Array 2 มิติ ส่งชื่อ และ ค่า เช่น new string[,]{{"Google","10"},{"GooDesign","90"}}</param>
    /// <returns></returns>
    private string PieChartSemiCircle(string ChartID, string Title,string[,] Parameters)
    {
        #region Variable
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        StringBuilder strScript = new StringBuilder();
        string chartName = "chart" + ChartID;
        #endregion

        #region Script Builder
        strScript.Append("$(function () {");
        strScript.Append("$('#" + chartName + "').highcharts({");
        strScript.Append("chart: {");
        strScript.Append("plotBackgroundColor: null,");
        strScript.Append("plotBorderWidth: 0,");
        strScript.Append("plotShadow: false");
        strScript.Append("},");
        strScript.Append("title: {");
        strScript.Append("  text: '" + Title + "',align: 'center',verticalAlign: 'middle',y: 50");
        strScript.Append("},");
        strScript.Append("tooltip: {");
        strScript.Append("  pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'");
        strScript.Append("},");
        strScript.Append("plotOptions: {");
        strScript.Append("pie: {");
        strScript.Append("  allowPointSelect:true,");
        //strScript.Append("  events:{click:function(event){alert(event.point.name);}},");
        strScript.Append("  dataLabels: {enabled: true,distance: -50,");
        strScript.Append("  style: {fontWeight: 'bold',color: 'white',textShadow: '0px 1px 2px black'}");
        strScript.Append("},");
        strScript.Append("startAngle: -90,");
        strScript.Append("endAngle: 90,");
        strScript.Append("center: ['50%', '75%']");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [{");
        strScript.Append("type: 'pie',");
        strScript.Append("name: '" + Title + "',");
        strScript.Append("innerSize: '50%',");
        strScript.Append("data: [");
        #region Data Builder
        for (int i = 0; i < Parameters.Length / Parameters.Rank; i++)
        {
            strScript.Append("['"+Parameters[i, 0]+"',"+Parameters[i, 1]+"],");
        }
        #endregion
        strScript.Append("]");
        strScript.Append("}]");
        strScript.Append("});");
        strScript.Append("});");
        #endregion

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), chartName))
        {
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), chartName, strScript.ToString(), true);
        }
        strScript = null;

        return "<div id='" + chartName + "' style='min-width: 400px; min-height: 400px; margin: 0 auto'></div>";
    }

    private string BarHorizontalStack(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue, string targetValue, string targetName, string targetColor)
    {
        #region Example
        /*################# Example #################
        lblChart.Text = clsChart.BarHorizontal("ID", 
            "หัวข้อหลัก", 
            "หัวข้อย่อย", 
            "ข้อความแกน y", 
            "หน่วยแกน y", 
            new string[] { "Series 1", "Series 2" }, 
            new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
            new string[] { "10,5,2", "1,7,12" });
        */
        #endregion

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        string chartName = "chart" + chart_id;

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        //strScript.Append("chart: {renderTo: '" + chartName + "',type: 'bar',height:$(document).height()-100},");
        strScript.Append("chart: {renderTo: '" + chartName + "',type: 'bar'},");
        strScript.Append("title: {text: '" + title + "'},");
        strScript.Append("subtitle: {text: '" + subtitle + "'},");
        strScript.Append("xAxis: {categories: [");

        //########### xCategory ##########
        for (int i = 0; i < xCategory.Length; i++)
        {
            strScript.Append("'" + xCategory[i] + "'");
            if (i < xCategory.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("],title: {text: null}},");
        strScript.Append("yAxis: ");
        strScript.Append("{min: 0,title: {text: '" + yTitle + "',align: 'high'},labels: {overflow: 'justify'}");
        if (!string.IsNullOrEmpty(targetValue))
        {
            strScript.Append(",plotLines:[{value:" + targetValue + ",color: '" + targetColor + "',width:1,zIndex:4,label:{text:'" + targetName + "'}}]");
        }
        strScript.Append("},");
        //strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + ",formatter: function() {return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + ' " + valueUnit + "';}},");
        strScript.Append("tooltip: {enabled:" + _showTooltip.ToString().ToLower() + "},");
        //strScript.Append("plotOptions: {series: {dataLabels: {enabled: " + _showLabel.ToString().ToLower() + ",formatter:function(){return this.point.name;}}}},");
        strScript.Append("plotOptions: {bar: {dataLabels: {enabled: " + _showLabel.ToString().ToLower() + "}},series: {stacking: 'normal'}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -100,y: 100,floating: true,borderWidth: 1,backgroundColor: '#FFFFFF',shadow: true},");
        strScript.Append("legend: {enabled: " + _showLegend.ToString().ToLower() + "},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            #region Data
            if (_seriesValueColor != null || _seriesValueText != null)
            {
                string[] seriesValues = seriesValue[i].Split(',');
                if (seriesValues.Length == _seriesValueColor.Length || seriesValues.Length == _seriesValueText.Length)
                {
                    strScript.Append("data: [");

                    for (int d = 0; d < seriesValues.Length; d++)
                    {
                        if (d > 0) strScript.Append(",");

                        strScript.Append("{");
                        if (_seriesValueColor != null)
                        {
                            if (_seriesValueColor[d] != "")
                            {
                                strScript.Append("color:'" + _seriesValueColor[d] + "',");
                            }
                        }
                        if (_seriesValueText != null)
                        {
                            if (_seriesValueText[d] != "")
                            {
                                strScript.Append("name:'" + _seriesValueText[d] + "',");
                            }
                        }
                        strScript.Append("y:" + seriesValues[d] + "");
                        strScript.Append("}");
                    }

                    strScript.Append("]");
                }
                else
                {
                    strScript.Append("data: [" + seriesValue[i] + "]");
                }
            }
            else
            {
                strScript.Append("data: [" + seriesValue[i] + "]");
            }
            #endregion
            strScript.Append("}");
            if (i < seriesName.Length - 1)
            {
                strScript.Append(",");
            }
        }

        strScript.Append("]});});});");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), chartName))
        {
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), chartName, strScript.ToString(), true);
        }
        strScript = null;

        return "<div id='" + chartName + "' style='min-width: 400px; min-height: 400px; margin: 0 auto'></div>";
    }
}