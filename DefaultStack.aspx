<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultStack.aspx.cs" Inherits="DefaultStack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>@WORK Reporting</title>
    <link href="CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <script src="Plugin/jQuery/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="Plugin/Highcharts-2.3.5/js/highcharts.js" type="text/javascript"></script>
    <script src="Plugin/Highcharts-2.3.5/js/modules/exporting.js" type="text/javascript"></script>
    <script src="Plugin/Highcharts-2.3.5/js/Oof.js" type="text/javascript"></script>

    <meta http-equiv="refresh" content="20">
</head>
<body>
    <form id="form1" runat="server">
        <center style="padding:10px;">
            <div style="/*background-color:#fafafa;padding:10px;border:1px solid #ddd;*/">
                <h1>
                    <asp:Label ID="chartCount" runat="server" />
                </h1>
                <h3 style="margin-top:10px;">
                    <asp:Label ID="chartCountDetail" runat="server" />
                </h3>
            </div>
            <table cellpadding="0" cellspacing="0" width="100%" style="margin:10px 0px 0px 0px;border-top:1px dashed #ddd;padding-top:10px;">
                <tr>
                    <td width="50%">
                        <asp:Label ID="chartDepartment" runat="server" />
                    </td>
                    <td width="50%">
                        <asp:Label ID="chartCategory" runat="server" />
                    </td>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
