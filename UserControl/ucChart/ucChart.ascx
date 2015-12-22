<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucChart.ascx.cs" Inherits="ucChart" %>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof Highcharts == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/Highcharts/js/highcharts.js") %>'><" + "/script>");
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/Highcharts/js/modules/exporting.js") %>'><" + "/script>");
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/Highcharts/js/themes/Oof.js") %>'><" + "/script>");
    }
</script>

<asp:Label ID="lblChart" runat="server"/>
