<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JobReport.aspx.cs" Inherits="JobReport" %>

<%@ Register src="UserControl/ucDateTime/ucDateJS.ascx" tagname="ucDateJS" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href="UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div style="border-bottom:1px solid #ddd;padding-bottom:10px;margin-bottom:10px;">
        <h1>Job Report</h1>
    </div>
    <asp:Label ID="lblDefaultWarn" runat="server" />
    <asp:Panel ID="pnDefault" runat="server" Visible="false">
        <div id="dvSearch" style="margin-bottom:10px;padding-bottom:10px;border-bottom:1px dashed #ddd;">
            <div>
                <b>Modify Date From</b> 
                <uc1:ucDateJS ID="ucDateJSFrom" runat="server" /> | 
                <b>To</b> 
                <uc1:ucDateJS ID="ucDateJSTo" runat="server" />
            </div>
            <div>
                <b>Site</b> <asp:DropDownList ID="ddlSite" runat="server" /> | 
                <b>Group</b> <asp:DropDownList ID="ddlGroup" runat="server" /> | 
                <b>Category</b> <asp:DropDownList ID="ddlCategory" runat="server" /> | 
                <b>User</b> <asp:DropDownList ID="ddlUser" runat="server" /> | 
                <b>JobStatus</b> <asp:DropDownList ID="ddlJobStatus" runat="server" >
                    <asp:ListItem>Complete</asp:ListItem>
                    <asp:ListItem>OnProcess</asp:ListItem>
                    <asp:ListItem>OnHold</asp:ListItem>
                    <asp:ListItem Value="All">All</asp:ListItem>
                </asp:DropDownList>
                 | 
                <b>ResolveType</b> <asp:DropDownList ID="ddlResolveType" runat="server" ></asp:DropDownList>
            </div>
            <div>
                <asp:Button ID="btSearch" runat="server" CssClass="Button SearchTH" 
                    onclick="btSearch_Click" />
            </div>
        </div>
        <div style="text-align:center;">
            <div style="padding:10px;">
                <asp:Panel ID="pnGVHeader" runat="server">
                    <table class="tbGridViewHeader" cellpadding="0" cellspacing="0">
                        <tr><td class="tbHD11"></td><td class="tbHD12"></td><td class="tbHD13"></td></tr>
                        <tr><td class="tbHD21"></td><td class="tbHD22">
                        <h4><span class="Icon32 User Normal"></span>
                        Job List<asp:Label ID="lblJobCount" runat="server" />
                        </h4></td><td class="tbHD23"></td></tr>
                        <tr><td></td><td></td><td></td></tr>
                    </table>
                </asp:Panel>
                <asp:Label ID="lblDefault" runat="server" />
                <asp:GridView ID="gvDefault" runat="server" AutoGenerateColumns="false" 
                    ShowHeader="true" ShowFooter="true" BorderStyle="None" CellPadding="0" 
                    Font-Bold="False" GridLines="None" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div style="width:100%;font-weight:normal;background-color:#fff;">
                                <table class="tbGridView" cellpadding="0" cellspacing="0">
                                    <tr class="trGridViewHeader">
                                        <td style="width:50px;">Site<span class="icBullet" /></td>
                                        <td style="width:100px;">User<span class="icBullet" /></td>
                                        <td style="width:auto;">Job Detail<span class="icBullet" /></td>
                                        <td style="width:200px;">Job Category<span class="icBullet" /></td>
                                        <td style="width:200px;">Department<span class="icBullet" /></td>
                                        <td style="width:150px;"><span style="color:#F58053;">Create</span>/<span style="color:#62B854;">Modify</span><span class="icBullet" /></td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <tr class="trGridView">
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem,"Site") %>
                                        </td>
                                        <td>
                                            <div>
                                                <img src='<%#pathPhoto+DataBinder.Eval(Container.DataItem,"Photo").ToString() %>' width="50px"/>
                                            </div>
                                            <div>
                                                <b style="font-size:12pt;color:#0F7EB2;"><%#DataBinder.Eval(Container.DataItem,"Username") %></b><br />
                                                <%#DataBinder.Eval(Container.DataItem,"GroupName") %>
                                            </div>
                                        </td>
                                        <td style="text-align:left;">
                                            <%#
                                                (DataBinder.Eval(Container.DataItem,"JobReferenceID").ToString()!=""?
                                                "<b>JobID</b>: "+DataBinder.Eval(Container.DataItem,"JobReferenceID").ToString()+"<br/>":
                                                "")
                                            %>
                                            <%#
                                                (DataBinder.Eval(Container.DataItem,"JobName").ToString()!=""?
                                                "<b>Name</b>: "+DataBinder.Eval(Container.DataItem,"JobName").ToString()+"<br />":
                                                "")
                                            %>
                                            <b>Detail</b>: <%#DataBinder.Eval(Container.DataItem,"JobDetail") %>
                                            <div style="margin:5px;padding:5px;background-color:#fafafa;border:1px solid #ddd;">
											    <b>Job UID</b>: <%#DataBinder.Eval(Container.DataItem,"UID") %><br/>
                                                <b>Request</b>: <%#DataBinder.Eval(Container.DataItem,"RequestType") %>
                                                <span style="font-size:8pt;color:#E78800;padding-left:5px;">
                                                    (<%#DataBinder.Eval(Container.DataItem,"RequestTypeDetail") %>)
                                                </span><br />
                                                <b>Resolve</b>: <%#DataBinder.Eval(Container.DataItem,"ResolveType") %>
                                                <span style="font-size:8pt;color:#E78800;padding-left:5px;">
                                                    (<%#DataBinder.Eval(Container.DataItem,"ResolveTypeDetail") %>)
                                                </span>
                                                <br />
                                                <b>JobStatus</b>: 
                                                <%#(DataBinder.Eval(Container.DataItem,"Processing").ToString()=="1"?"OnProcess":
                                                                                                    (DataBinder.Eval(Container.DataItem, "Complete").ToString() == "1" ? "Complete" : "OnHold"))%>
                                            </div>
                                        </td>
                                        <td>
                                            <%#(DataBinder.Eval(Container.DataItem,"JobCategory")!=DBNull.Value?DataBinder.Eval(Container.DataItem,"JobCategory").ToString():"-") %>
                                        </td>
                                        <td>
                                            <%#GetDepartment(
                                                DataBinder.Eval(Container.DataItem,"Site").ToString(),
                                                DataBinder.Eval(Container.DataItem,"DepartmentID").ToString()
                                            ) %>
                                            <br />
                                            <span style="font-size:8pt;color:#E78800;" title="รหัสแผนก (dept_id)">
                                                <%#DataBinder.Eval(Container.DataItem, "DepartmentID").ToString()%>
                                            </span>
                                        </td>
                                        <td>
                                            <span title="Create Date" style="color:#F58053;"><%#DateTime.Parse(DataBinder.Eval(Container.DataItem,"CreateDate").ToString()).ToString("dd/MM/yyyy HH:mm") %></span><br />
                                            <span title="Modify Date" style="color:#62B854;"><%#DateTime.Parse(DataBinder.Eval(Container.DataItem,"CloseDate").ToString()).ToString("dd/MM/yyyy HH:mm") %></span>
                                        </td>
                                    </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </table>
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

