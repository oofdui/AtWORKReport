using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for clsChart
/// </summary>
public class clsChart
{
	public clsChart()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected void Import(string strPath)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        //## Import JS ##
        HtmlGenericControl js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = strPath;
        currentPage.Header.Controls.Add(js);
    }

    public void ImportChart()
    {
        Import("Plugin/JQUERY/1.4.4/jquery.min.js");
        Import("Plugin/Highcharts/js/highcharts.js");
        Import("Plugin/Highcharts/js/modules/exporting.js");

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
    }

    public string LineBasic(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue)
    {
        /*################# Example #################
         * lblChart.Text = clsChart.LineBasic("ID", 
         *  "หัวข้อหลัก", 
         *  "หัวข้อย่อย", 
         *  "ข้อความแกน y", 
         *  "หน่วยแกน y", 
         *  new string[] { "Series 1", "Series 2" }, 
         *  new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
         *  new string[] { "10,5,2", "1,7,12" });
         */

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
        strScript.Append("plotLines: [{value: 0,width: 1,color: '#808080'}]},");
        strScript.Append("tooltip: {");
        strScript.Append("formatter: function() {");
        strScript.Append("return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + ' " + valueUnit + "';}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -10,y: 100,borderWidth: 0},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            strScript.Append("data: [" + seriesValue[i] + "]");
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

    public string LineBasic_ShowLabel(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue)
    {
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

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        string chartName = "chart" + chart_id;

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {renderTo: '" + chartName + "',type: 'line'},");
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

        strScript.Append("]},");
        strScript.Append("yAxis: {title: {text: '" + yTitle + "'}},");
        strScript.Append("tooltip: {enabled: false,formatter: function() {return '<b>'+ this.series.name +'</b><br/>'+this.x +': '+ this.y +' " + valueUnit + "';}},");
        strScript.Append("plotOptions: {line: {dataLabels: {enabled: true},enableMouseTracking: false}},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            strScript.Append("data: [" + seriesValue[i] + "]");
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

    public string LineBasic_ShowAll(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue)
    {
        /*################# Example #################
         * lblChart.Text = clsChart.LineBasic("ID", 
         *  "หัวข้อหลัก", 
         *  "หัวข้อย่อย", 
         *  "ข้อความแกน y", 
         *  "หน่วยแกน y", 
         *  new string[] { "Series 1", "Series 2" }, 
         *  new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
         *  new string[] { "10,5,2", "1,7,12" });
         */

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
        strScript.Append("plotLines: [{value: 0,width: 1,color: '#808080'}]},");
        strScript.Append("tooltip: {formatter: function() {return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + ' " + valueUnit + "';}},");
        strScript.Append("plotOptions: {line: {dataLabels: {enabled: true},enableMouseTracking: true}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -10,y: 100,borderWidth: 0},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            strScript.Append("data: [" + seriesValue[i] + "]");
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

    public string BarHorizontal(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue)
    {
        /*################# Example #################
         * lblChart.Text = clsChart.BarHorizontal("ID", 
         *  "หัวข้อหลัก", 
         *  "หัวข้อย่อย", 
         *  "ข้อความแกน y", 
         *  "หน่วยแกน y", 
         *  new string[] { "Series 1", "Series 2" }, 
         *  new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
         *  new string[] { "10,5,2", "1,7,12" });
         */

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        string chartName = "chart" + chart_id;

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {renderTo: '" + chartName + "',type: 'bar',height: $(document).height()-100},");
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
        strScript.Append("yAxis: {min: 0,title: {text: '" + yTitle + "',align: 'high'},labels: {overflow: 'justify'}},");
        strScript.Append("tooltip: {formatter: function() {return ''+ this.series.name +': '+ this.y +' " + valueUnit + "';}},");
        strScript.Append("plotOptions: {bar: {dataLabels: {enabled: true}}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -100,y: 100,floating: true,borderWidth: 1,backgroundColor: '#FFFFFF',shadow: true},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            strScript.Append("data: [" + seriesValue[i] + "]");
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

    public string BarVertical(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue)
    {
        /*################# Example #################
         * lblChart.Text = clsChart.BarVertical("ID", 
         *  "หัวข้อหลัก", 
         *  "หัวข้อย่อย", 
         *  "ข้อความแกน y", 
         *  "หน่วยแกน y", 
         *  new string[] { "Series 1", "Series 2" }, 
         *  new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
         *  new string[] { "10,5,2", "1,7,12" });
         */

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
        strScript.Append("yAxis: {min: 0,title: {text: '" + yTitle + "'},labels: {overflow: 'justify'}},");
        strScript.Append("tooltip: {formatter: function() {return ''+ this.series.name +': '+ this.y +' " + valueUnit + "';}},");
        strScript.Append("plotOptions: {bar: {dataLabels: {enabled: true}}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -100,y: 100,floating: true,borderWidth: 1,backgroundColor: '#FFFFFF',shadow: true},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [");

        //########### Series ###########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            strScript.Append("data: [" + seriesValue[i] + "]");
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

    public string BarHorizontalStack(string chart_id, string title, string subtitle, string yTitle, string valueUnit, string[] seriesName, string[] xCategory, string[] seriesValue)
    {
        /*################# Example #################
         * lblChart.Text = clsChart.BarHorizontalStack("ID", 
         *  "หัวข้อหลัก", 
         *  "หัวข้อย่อย", 
         *  "ข้อความแกน y", 
         *  "หน่วยแกน y", 
         *  new string[] { "Series 1", "Series 2" }, 
         *  new string[] { "ข้อความแกน x:1", "ข้อความแกน x:2", "ข้อความแกน x:3" }, 
         *  new string[] { "10,5,2", "1,7,12" });
         */

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        string chartName = "chart" + chart_id;

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {renderTo: '" + chartName + "',type: 'bar',height: $(document).height()-100},");
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
        strScript.Append("yAxis: {min: 0,title: {text: '" + yTitle + "',align: 'high'},labels: {overflow: 'justify'}},");
        strScript.Append("tooltip: {formatter: function() {return ''+ this.series.name +': '+ this.y +' " + valueUnit + "';}},");
        strScript.Append("plotOptions: {bar: {dataLabels: {enabled: true}},series: {stacking: 'normal'}},");
        //strScript.Append("legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -100,y: 100,floating: true,borderWidth: 1,backgroundColor: '#FFFFFF',shadow: true},");
        strScript.Append("credits: {enabled: false},");
        strScript.Append("series: [");

        //########### Series ##########
        for (int i = 0; i < seriesName.Length; i++)
        {
            strScript.Append("{");
            strScript.Append("name: '" + seriesName[i] + "',");
            strScript.Append("data: [" + seriesValue[i] + "]");
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

    public string ChartBarStack(string title, string subtitle, string xAxis, string yAxis, string toolTip, string name, string value)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {");
        strScript.Append("renderTo: 'container',");
        strScript.Append("type: 'bar'");
        strScript.Append("},");
        strScript.Append("title: {");
        strScript.Append("text: '" + title + "'");
        strScript.Append("},");
        strScript.Append("subtitle: {");
        strScript.Append("text: '" + subtitle + "'");
        strScript.Append("},");
        strScript.Append("xAxis: {");
        strScript.Append("categories: [" + xAxis + "]");
        strScript.Append("},");
        strScript.Append("yAxis: {");
        strScript.Append("min: 0,");
        strScript.Append("title: {");
        strScript.Append("text: '" + yAxis + "'");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("legend: {");
        strScript.Append("backgroundColor: '#FFFFFF',");
        strScript.Append("reversed: true");
        strScript.Append("},");
        strScript.Append("tooltip: {");
        strScript.Append("formatter: function() {");
        strScript.Append("return ''+");
        strScript.Append("this.series.name +': '+ this.y +' " + toolTip + "';");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("plotOptions: {");
        strScript.Append("series: {");
        strScript.Append("stacking: 'normal'");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("series: [");
        //## Start Point Value ##
        string[] strxAxis = xAxis.Split(',');
        string[] strName = name.Split(',');
        string[] strValue = value.Split(';');
        if (strxAxis.Length != strValue[0].Split(',').Length || strName.Length != strValue.Length)
        {
            //return "ตัวแปรที่ส่งเข้ามาไม่เท่ากัน";
            return "ตัวแปรที่ส่งเข้ามาไม่เท่ากัน<br/>xAxis : " + strxAxis.Length.ToString() + " Name : " + strName.Length.ToString() + " Value : " + strValue.Length.ToString() + " Sub Value : " + strValue[0].Split(',').Length;
        }
        int i = 0;
        for (i = 0; i <= strName.Length - 1; i++)
        {
            strScript.Append("{");
            strScript.Append("name: " + strName[i].ToString() + ",");
            strScript.Append("data: [" + strValue[i].ToString() + "]");
            strScript.Append("}");
            if (i < strName.Length - 1)
            {
                strScript.Append(",");
            }
        }
        //## End Point Value ##
        strScript.Append("]");
        strScript.Append("});");
        strScript.Append("});");
        strScript.Append("});");

        //lblSQL.Text = strScript.ToString();
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ChartBarStack"))
        {
            //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ChartLine", strScript.ToString(), true);
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "ChartBarStack", strScript.ToString(), true);
        }
        strScript = null;
        return "<div id='container' style='min-width: 400px; height: 400px; margin: 0 auto'></div>";
    }

    public string ChartBar(string title, string subtitle, string xAxis, string yAxis, string toolTip, string name, string value)
    {
        /* Example for Use
        lblChart.Text = clsChart.ChartBar("Title", 
            "SubTitle", 
            "'Africa', 'America', 'Asia', 'Europe', 'Oceania'", 
            "ข้อความแกน x", 
            "ToolTip", 
            "'Year 1800','Year 1900','Year 2008'", 
            "107, 31, 635, 203, 2;133, 156, 947, 408, 6;973, 914, 4054, 732, 34");
         */

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();

        strScript.Append("$(function () {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {");
        strScript.Append("renderTo: 'container',");
        strScript.Append("type: 'bar'");
        strScript.Append("},");
        strScript.Append("title: {");
        strScript.Append("text: '" + title + "'");
        strScript.Append("},");
        strScript.Append("subtitle: {");
        strScript.Append("text: '" + subtitle + "'");
        strScript.Append("},");
        strScript.Append("xAxis: {");
        strScript.Append("categories: [" + xAxis + "],");
        strScript.Append("title: {");
        strScript.Append("text: null");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("yAxis: {");
        strScript.Append("min: 0,");
        strScript.Append("title: {");
        strScript.Append("text: '" + yAxis + "',");
        strScript.Append("align: 'high'");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("tooltip: {");
        strScript.Append("formatter: function() {");
        strScript.Append("return ''+");
        strScript.Append("this.series.name +': '+ this.y +' " + toolTip + "';");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("plotOptions: {");
        strScript.Append("bar: {");
        strScript.Append("dataLabels: {");
        strScript.Append("enabled: true");
        strScript.Append("}");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("legend: {");
        strScript.Append("layout: 'vertical',");
        strScript.Append("align: 'right',");
        strScript.Append("verticalAlign: 'top',");
        strScript.Append("x: -100,");
        strScript.Append("y: 100,");
        strScript.Append("floating: true,");
        strScript.Append("borderWidth: 1,");
        strScript.Append("backgroundColor: '#FFFFFF',");
        strScript.Append("shadow: true");
        strScript.Append("},");
        strScript.Append("credits: {");
        strScript.Append("enabled: false");
        strScript.Append("},");
        strScript.Append("series: [");
        //## Start Point Value ##
        string[] strxAxis = xAxis.Split(',');
        string[] strName = name.Split(',');
        string[] strValue = value.Split(';');
        if (strxAxis.Length != strValue[0].Split(',').Length || strName.Length != strValue.Length)
        {
            //return "ตัวแปรที่ส่งเข้ามาไม่เท่ากัน";
            return "ตัวแปรที่ส่งเข้ามาไม่เท่ากัน<br/>xAxis : " + strxAxis.Length.ToString() + " Name : " + strName.Length.ToString() + " Value : " + strValue.Length.ToString() + " Sub Value : " + strValue[0].Split(',').Length;
        }
        int i = 0;
        for (i = 0; i <= strName.Length - 1; i++)
        {
            strScript.Append("{");
            strScript.Append("name: " + strName[i].ToString() + ",");
            strScript.Append("data: [" + strValue[i].ToString() + "]");
            strScript.Append("}");
            if (i < strName.Length - 1)
            {
                strScript.Append(",");
            }
        }
        //## End Point Value ##
        strScript.Append("]");
        strScript.Append("});");
        strScript.Append("});");
        strScript.Append("});");

        //lblSQL.Text = strScript.ToString();
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ChartBar"))
        {
            //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ChartLine", strScript.ToString(), true);
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "ChartBar", strScript.ToString(), true);
        }
        strScript = null;
        return "<div id='container' style='min-width: 400px; height: 400px; margin: 0 auto'></div>";
    }

    public string ChartLine(string title, string subtitle, string xAxis, string yAxis, string toolTip, string name, string value)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();

        strScript.Append("$(function() {");
        strScript.Append("var chart;");
        strScript.Append("$(document).ready(function() {");
        strScript.Append("chart = new Highcharts.Chart({");
        strScript.Append("chart: {");
        strScript.Append("renderTo: 'container',");
        strScript.Append("type: 'line',");
        strScript.Append("marginRight: 130,");
        strScript.Append("marginBottom: 25");
        strScript.Append("},");
        strScript.Append("title: {");
        strScript.Append("text: '" + title + "',");
        strScript.Append("x: -20");
        strScript.Append("},");
        strScript.Append("subtitle: {");
        strScript.Append("text: '" + subtitle + "',");
        strScript.Append("x: -20");
        strScript.Append("},");
        strScript.Append("xAxis: {");
        strScript.Append("categories: [" + xAxis + "]");
        strScript.Append("},");
        strScript.Append("yAxis: {");
        strScript.Append("title: {");
        strScript.Append("text: '" + yAxis + "'");
        strScript.Append("},");
        strScript.Append("plotLines: [");
        strScript.Append("{");
        strScript.Append("value: 0, width: 1, color: '#808080'");
        strScript.Append("}");
        strScript.Append("]");
        strScript.Append("},");
        strScript.Append("tooltip: {");
        strScript.Append("formatter: function() {");
        strScript.Append("return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + ' " + toolTip + "';");
        strScript.Append("}");
        strScript.Append("},");
        strScript.Append("legend: {");
        strScript.Append("layout: 'vertical',");
        strScript.Append("align: 'right',");
        strScript.Append("verticalAlign: 'top',");
        strScript.Append("x: -10,");
        strScript.Append("y: 100,");
        strScript.Append("borderWidth: 0");
        strScript.Append("},");
        strScript.Append("series: [");
        //## Start Point Value ##
        string[] strxAxis = xAxis.Split(',');
        string[] strName = name.Split(',');
        string[] strValue = value.Split(';');
        if (strxAxis.Length != strValue[0].Split(',').Length || strName.Length != strValue.Length)
        {
            //return "ตัวแปรที่ส่งเข้ามาไม่เท่ากัน";
            return "ตัวแปรที่ส่งเข้ามาไม่เท่ากัน<br/>xAxis : " + strxAxis.Length.ToString() + " Name : " + strName.Length.ToString() + " Value : " + strValue.Length.ToString() + " Sub Value : " + strValue[0].Split(',').Length;
        }
        int i = 0;
        for (i = 0; i <= strName.Length - 1; i++)
        {
            strScript.Append("{");
            strScript.Append("name: " + strName[i].ToString() + ",");
            strScript.Append("data: [" + strValue[i].ToString() + "]");
            strScript.Append("}");
            if (i < strName.Length-1)
            {
                strScript.Append(",");
            }
        }
        //## End Point Value ##
        strScript.Append("]");

        strScript.Append("});");
        strScript.Append("});");
        strScript.Append("});");
        //lblSQL.Text = strScript.ToString();
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ChartLine"))
        {
            //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ChartLine", strScript.ToString(), true);
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "ChartLine", strScript.ToString(), true);
        }
        strScript = null;
        return "<div id='container' style='min-width: 400px; height: 400px; margin: 0 auto'></div>";
    }
}
