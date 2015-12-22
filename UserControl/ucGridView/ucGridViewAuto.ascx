<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGridViewAuto.ascx.cs" Inherits="UserControl_ucGridView_ucGridViewAuto" %>

<%@ Register src="ucGridViewPager.ascx" tagname="ucGridViewPager" tagprefix="uc1" %>

<link href="/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
<link href="/UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />

<div>
    <asp:Label ID="lblMessage" runat="server" />
    <asp:Panel ID="pnGVHeader" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" class="tbHeader">
            <tr>
                <td class="tbDefault hdTL">
                </td>
                <td class="tbDefault hdTC">
                </td>
                <td class="tbDefault hdTR">
                </td>
            </tr>
            <tr>
                <td class="tbDefault hdML">
                </td>
                <td class="tbDefault hdMC">
                    <h3><asp:Label ID="lblTitle" runat="server" /></h3>
                </td>
                <td class="tbDefault hdMR">
                </td>
            </tr>
            <tr>
                <td class="tbDefault hdFL">
                </td>
                <td class="tbDefault hdFC">
                </td>
                <td class="tbDefault hdFR">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:GridView ID="gvDefault" runat="server" AutoGenerateColumns="true" 
        AllowPaging="True" GridLines="None" CssClass="gvDefault" PagerStyle-CssClass="Pager" 
        CellPadding="0">
        <PagerTemplate>
            <uc1:ucGridViewPager ID="ucGridViewPager1" runat="server" PageSize="2,10,50,100"/>
        </PagerTemplate>
    </asp:GridView>
</div>