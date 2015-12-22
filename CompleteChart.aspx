<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompleteChart.aspx.cs" Inherits="CompleteChart" %>

<%@ Register src="UserControl/ucChart/ucChart.ascx" tagname="ucChart" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>@WORK Complete Chart</title>
    <link href="CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <script src="Plugin/jQuery/jquery-1.8.3.js" type="text/javascript"></script>
    <meta http-equiv="refresh" content="20">

    <style type="text/css">
        html,body{background-color:#FFF;height:100%;width:100%;overflow:hidden;}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="height:100%;">
        <center>
            <h1 style="padding-top:20px;">
                @WORK Report <span style="color:#5CBACB;">(Daily | Monthly)</span>
            </h1>
            <div style="display:table;margin-top:20px;">
                <div style="display:table-row;">
                    <div style="display:table-cell;width:500px;">
                        <asp:Label ID="chartDialy" runat="server" />
                        <uc1:ucChart ID="ucChart2" runat="server" />
                    </div>
                    <div style="display:table-cell;width:500px;">
                        <asp:Label ID="chartMonthly" runat="server" />
                        <uc1:ucChart ID="ucChart1" runat="server" />
                    </div>
                </div>
            </div>
        </center>
    </form>
</body>
</html>
