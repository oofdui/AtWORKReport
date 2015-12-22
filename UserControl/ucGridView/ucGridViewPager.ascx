<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGridViewPager.ascx.cs" Inherits="UserControl_ucGridViewPager"%>

<style type="text/css">
    body
    {
        background-color:#ffffff;
        color:#303030;
        font-family:Tahoma;
        font-size:10pt;
    }
    .gvPager
    {
	    background: #f0efef;
	    border-left:1px solid #CFCFCF;border-right:1px solid #CFCFCF;border-bottom:1px solid #CFCFCF;
	    padding: 5px 5px;
    }
</style>

<div class="gvPager">
    <div style="float:left;">
        <asp:ImageButton AlternateText="First page" ToolTip="First page" ID="ImageButtonFirst"
            runat="server" ImageUrl="/UserControl/ucGridView/Images/PgFirst.gif" Width="8" Height="9" CommandName="First"
            CommandArgument="First" OnCommand="NavigateClick"/>
        &nbsp;
        <asp:ImageButton AlternateText="Previous page" ToolTip="Previous page" ID="ImageButtonPrev"
            runat="server" ImageUrl="/UserControl/ucGridView/Images/PgPrev.gif" Width="5" Height="9" CommandName="Prev"
            CommandArgument="Prev" OnCommand="NavigateClick"/>
        &nbsp;
        <asp:Label ID="LabelPage" runat="server" Text="Page " AssociatedControlID="ddlPage" />
        <asp:DropDownList ID="ddlPage" runat="server" AutoPostBack="true" CssClass="DDControl"
            OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
        </asp:DropDownList>
        of
        <asp:Label ID="LabelNumberOfPages" runat="server" />
        &nbsp;
        <asp:ImageButton AlternateText="Next page" ToolTip="Next page" ID="ImageButtonNext"
            runat="server" ImageUrl="/UserControl/ucGridView/Images/PgNext.gif" Width="5" Height="9" CommandName="Next"
            CommandArgument="Next" OnCommand="NavigateClick" />
        &nbsp;
        <asp:ImageButton AlternateText="Last page" ToolTip="Last page" ID="ImageButtonLast"
            runat="server" ImageUrl="/UserControl/ucGridView/Images/PgLast.gif" Width="8" Height="9" CommandName="Last"
            CommandArgument="Last" OnCommand="NavigateClick"/>
        &nbsp;
        <asp:Label ID="Label1" Text="Total :" AssociatedControlID="lbTotal" runat="server" />
        <asp:Label ID="lbTotal" CssClass="DDControl" runat="server" />
    </div>
    <div style="float:right;">
        <asp:Label ID="LabelRows" runat="server" Text="Results per page:" AssociatedControlID="DropDownListPageSize" />
        <asp:DropDownList ID="DropDownListPageSize" runat="server" AutoPostBack="true" CssClass="DDControl"
            OnSelectedIndexChanged="DropDownListPageSize_SelectedIndexChanged">
            <asp:ListItem Value="20" />
            <asp:ListItem Value="50" />
            <asp:ListItem Value="100" />
        </asp:DropDownList>
    </div>
    <div style="clear:both;"></div>
</div>
<asp:Label ID="lblMessage" runat="server" />


