<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SummaryDashboardDetail.ascx.cs" Inherits="SummaryDashboardDetail" %>
<style type="text/css">
    .fontNormal{font-weight:normal;width:100%;}
    .ItemRow
    {
        display:table-row;
        border-bottom:1px dashed #DDD;
    }
    .ItemRow:hover 
    {
        padding:0px;
        background-color:#FAFAFA;
        border:1px solid #DDD;
        cursor:pointer;
    }
</style>
<div style="display:table;width:100%;font-size:9pt;">
    <asp:Label ID="lblDefault" runat="server" />
    <asp:GridView ID="gvDefault" runat="server" AutoGenerateColumns="false" 
        ShowHeader="true" ShowFooter="true" CssClass="fontNormal" BorderStyle="None" 
        CellPadding="0" GridLines="None">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="ItemRow">
                        <td style="width:30px;vertical-align:middle;text-align:left;">
                            <img src='<%#pathPhoto+(DataBinder.Eval(Container.DataItem, "Photo") != DBNull.Value ? DataBinder.Eval(Container.DataItem, "Photo").ToString(): "Default.png")%>' style='width:28px;'/>
                        </td>
                        <td style="padding:3px;vertical-align:middle;text-align:left;">
                            <div style="font-size:10pt;font-weight:bold;color:#3D7899;">
                                <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                                <span style="color:#505050;"> (<%#DataBinder.Eval(Container.DataItem, "GroupName")%>)</span>
                            </div>
                            <div style="color:#525252;font-size:8pt;">
                                <b>OnHand</b> : <span style="color:#E6720C;"><%#DataBinder.Eval(Container.DataItem, "CountOnHand")%></span>
                                <%#GetProcessing(DataBinder.Eval(Container.DataItem,"ResolveType").ToString()) %>
                            </div>
                        </td>
                        <td style="width:50px;vertical-align:middle;text-align:right;">
                            <img src='Images/ic<%#(DataBinder.Eval(Container.DataItem,"StatusName")!=DBNull.Value?DataBinder.Eval(Container.DataItem,"StatusName"):"Offline") %>.gif' title='<%#"["+DataBinder.Eval(Container.DataItem,"StatusName").ToString()+"] "+DataBinder.Eval(Container.DataItem, "ProcessingName").ToString() %>'/>
                        </td>
                    </tr>
                    <%--<div class="ItemRow">
                        <div style="display:table-cell;width:30px;vertical-align:middle;">
                            <img src='<%#pathPhoto+(DataBinder.Eval(Container.DataItem, "Photo") != DBNull.Value ? DataBinder.Eval(Container.DataItem, "Photo").ToString(): "Default.png")%>' style='width:30px;'/>
                        </div>
                        <div style="display:table-cell;padding:4px;vertical-align:middle;width:100%;">
                            <div style="font-size:10pt;font-weight:bold;color:#3D7899;">
                                <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                                <span style="color:#505050;"> (<%#DataBinder.Eval(Container.DataItem, "GroupName")%>)</span>
                            </div>
                            <div style="color:#525252;font-size:8pt;">
                                <b>OnHand</b> : <span style="color:#E6720C;"><%#DataBinder.Eval(Container.DataItem, "CountOnHand")%></span>
                                <%#GetProcessing(DataBinder.Eval(Container.DataItem,"ResolveType").ToString()) %>
                            </div>
                        </div>
                        <div style="display:table-cell;width:50px;vertical-align:middle;">
                            <img src='Images/ic<%#(DataBinder.Eval(Container.DataItem,"StatusName")!=DBNull.Value?DataBinder.Eval(Container.DataItem,"StatusName"):"Offline") %>.gif' title='<%#"["+DataBinder.Eval(Container.DataItem,"StatusName").ToString()+"] "+DataBinder.Eval(Container.DataItem, "ProcessingName").ToString() %>'/>
                        </div>
                    </div>--%>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>