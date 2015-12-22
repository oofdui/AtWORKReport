<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Timeline.aspx.cs" Inherits="Timeline" %>
<%@ Register src="UserControl/ucDateTime/ucDateJS.ascx" tagname="ucDateJS" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <script src="Plugin/Schedule/hogan.min.js" type="text/javascript"></script>
    <script src="Plugin/Schedule/scheduler.js" type="text/javascript"></script>
    <link href="Plugin/Schedule/scheduler.css" rel="stylesheet" />
    <style>
        .schedule1{
            background-color: #FF6F6F;
            border-right:1px solid #7F7F7F;
        }
        .schedule2{
            background-color: #FFB66F;
            border-right:1px solid #7F7F7F;
        }
        .schedule3{
            background-color: #FFE26F;
            border-right:1px solid #7F7F7F;
        }
        .schedule4{
            background-color: #DFFF6F;
            border-right:1px solid #7F7F7F;
        }
        .schedule5{
            background-color: #79EA4D;
            border-right:1px solid #7F7F7F;
        }
        .schedule6{
            background-color: #60E4AA;
            border-right:1px solid #7F7F7F;
        }
        .schedule7{
            background-color: #60C3E4;
            border-right:1px solid #7F7F7F;
        }
        .schedule8{
            background-color: #D18EFB;
            border-right:1px solid #7F7F7F;
        }
        .schedule9{
            background-color: #FF7AFA;
            border-right:1px solid #7F7F7F;
        }
        .schedule10{
            background-color: #FF7A93;
            border-right:1px solid #7F7F7F;
        }
        #scheduler{
            border: 1px solid #e7e7e7;
            border-right:1px solid #7F7F7F;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div style="border-bottom:1px solid #ddd;padding-bottom:10px;margin-bottom:10px;">
        <h1 style="float:left;"><img src="Images/icTimeline.png" style="width:64px;"/> Timeline</h1>
        <div style="float:right;background-color:#FAFAFA;border:1px solid #DDD;padding:10px;">Next <a href="javascript:window.location.reload()">refresh</a> in <b id="countdown"></b> seconds | LastUpdate : <%=DateTime.Now.ToString("HH:mm") %></div>
        <div style="clear:both;"></div>
    </div>
    <div id="dvSearch" style="margin-bottom:10px;padding-bottom:10px;border-bottom:1px dashed #ddd;">
        <div>
            <div style="float:left;">
                <b>Date</b><uc1:ucDateJS ID="ucDateJSDate" runat="server" /> | 
                <b>Site</b> <asp:DropDownList ID="ddlSite" runat="server" /> | 
                <b>Group</b> <asp:DropDownList ID="ddlGroup" runat="server" /> | 
                <b>User</b> <asp:DropDownList ID="ddlUser" runat="server" /> 
            </div>
            <div style="float:right;">
                <asp:Button ID="btSearch" runat="server" CssClass="Button SearchTH" OnClick="btSearch_Click" />
            </div>
            <div style="clear:both;"></div>
        </div>
    </div>
    <div id="scheduler"></div>
    <script type="text/javascript">
        $(document).ready(function(){
            var list = [
                <%=strTimeline.ToString()%>
            ];
            var steps = [
                '07:00',
                '08:00',
                '09:00',
                '10:00',
                '11:30',
                '12:05',
                '13:00',
                '14:00',
                '15:00',
                '16:00',
                '17:00',
                '18:00',
                '19:00',
                '20:00',
                '21:00',
                '22:00'
            ];

            var $scheduler = $("#scheduler").schedulerjs({
                'list': list,
                'steps': steps,
                'start': '<%=DateTime.Now.Hour+":00"%>',
                'end': '<%=DateTime.Now.AddHours(1).Hour+":00"%>',
                'headName':'เจ้าหน้าที่'
            });
            $scheduler.schedulerjs('showSelector');
        });
    </script>
    <script type="text/javascript">
        function refreshpage(interval, countdownel, totalel){
	        var countdownel = document.getElementById(countdownel)
	        var totalel = document.getElementById(totalel)
	        var timeleft = interval+1
	        var countdowntimer

	        function countdown(){
		        timeleft--
		        countdownel.innerHTML = timeleft
		        if (timeleft == 0){
			        clearTimeout(countdowntimer)
			        window.location.reload()
		        }
		        countdowntimer = setTimeout(function(){
			        countdown()
		        }, 1000)
	        }
	        countdown()
        }
        window.onload = function(){
            //refreshpage(60, "countdown")
        }
    </script>
</asp:Content>