<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDateTime.ascx.cs" Inherits="UserControl_ucDateTime" %>

<script type="text/javascript">
    window.onload = function () {
        UserControlChecker_<%=this.ClientID %>();
    };
    function UserControlChecker_<%=this.ClientID %>() {
        var chkEnable = document.getElementById('<%=chkEnable.ClientID %>');
        if (chkEnable.checked) {
            document.getElementById("<%=ddlDay.ClientID %>").disabled = false;
            document.getElementById("<%=ddlMonth.ClientID %>").disabled = false;
            document.getElementById("<%=ddlYear.ClientID %>").disabled = false;
            document.getElementById("<%=ddlHour.ClientID %>").disabled = false;
            document.getElementById("<%=ddlMinute.ClientID %>").disabled = false;
        }
        else {
            document.getElementById("<%=ddlDay.ClientID %>").disabled = true;
            document.getElementById("<%=ddlMonth.ClientID %>").disabled = true;
            document.getElementById("<%=ddlYear.ClientID %>").disabled = true;
            document.getElementById("<%=ddlHour.ClientID %>").disabled = true;
            document.getElementById("<%=ddlMinute.ClientID %>").disabled = true;
        }
    }
</script>

<span style="font-family:Tahoma;font-size:10pt;">
    <span>
        <asp:CheckBox ID="chkEnable" runat="server" Checked="false"/>
        <asp:DropDownList ID="ddlDay" runat="server"/>
        <asp:DropDownList ID="ddlMonth" runat="server"/>
        <asp:DropDownList ID="ddlYear" runat="server"/>
    </span>
    <span style="padding-left:5px;">
        <asp:DropDownList ID="ddlHour" runat="server" />:
        <asp:DropDownList ID="ddlMinute" runat="server" />
    </span>
</span>