<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummaryDashboard.aspx.cs" Inherits="SummaryDashboard" %>

<%@ Register src="SummaryDashboardDetail.ascx" tagname="SummaryDashboardDetail" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>@WORK Summary Dashboard</title>
    <link href="CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="CSS/cssControl.css" rel="stylesheet" type="text/css" />
    <link href="CSS/cssCustom.css" rel="stylesheet" type="text/css" />

    <script src="Plugin/jQuery/jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
        setTimeout(function() {
            $('body,html').animate({ scrollTop: $(document).height() - $(window).height() }, 800);
        }, 15000);
        setTimeout(function () {
            $('body,html').animate({ scrollTop: 0 }, 800);
        }, 30000);
        });
    </script>
    <meta http-equiv="refresh" content="40"/>

    <style type="text/css">
        body{background-color:#FFF;}
        .Width100{width:100%;}
        .Width33{width:33%;}
        .Main
        {
            margin:4px;
            -moz-border-radius:5px;
            -webkit-border-radius:5px;
            border-radius:5px;
            background-color:#FFF;
            border:1px solid #DDD;
            -moz-box-shadow:2px 2px 3px #DDD;
            -webkit-box-shadow:2px 2px 3px #DDD;
            box-shadow:2px 2px 3px #DDD;
        }
        .Header
        {
            padding:5px;
            font-size:11pt;
            text-align:center;
            background-color:#FAFAFA;
            border-bottom:1px solid #DDD;
            font-weight:bold;
        }
        .Content
        {
            padding:4px;
            vertical-align:top;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:4px">
            <asp:Label ID="lblDefault" runat="server" />
            <asp:DataList ID="dlDefault" runat="server" BorderStyle="None" CellPadding="0" 
                RepeatColumns="3" RepeatDirection="Horizontal" ShowFooter="False" 
                ShowHeader="False" CssClass="Width100" HorizontalAlign="Justify">
                <ItemStyle VerticalAlign="Top" Width="33%" />
                <ItemTemplate>
                    <div class="Main">
                        <div class="Header" style="color:#79D5EF;text-align:left;">
                            <div style="float:left;padding-left:5px;">
                                <%#DataBinder.Eval(Container.DataItem,"Name") %>
                            </div>
                            <div style="float:right;font-size:8pt;font-weight:normal;color:#6c6c6c;padding-right:5px;">
                                <%#GetCountSummary(DataBinder.Eval(Container.DataItem,"SiteUID").ToString(),
                                    DataBinder.Eval(Container.DataItem,"GroupsUID").ToString(),
                                    DataBinder.Eval(Container.DataItem,"GroupsUIDException").ToString())%>
                            </div>
                            <div style="clear:both;height:1px;"></div>
                        </div>
                        <div class="Content">
                            <uc1:SummaryDashboardDetail ID="SummaryDashboardDetail1" runat="server" 
                                SiteUID='<%#DataBinder.Eval(Container.DataItem,"SiteUID") %>' 
                                GroupUID='<%#DataBinder.Eval(Container.DataItem,"GroupsUID") %>' 
                                GroupUIDException='<%#DataBinder.Eval(Container.DataItem,"GroupsUIDException") %>'/>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
