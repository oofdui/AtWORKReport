<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDateJS.ascx.cs" Inherits="UserControl_ucDateTime_ucDateJS" %>

<script type="text/javascript">
    if (typeof jQuery.ui == 'undefined') {
        document.write("<script type='text/javascript' src='UserControl/ucDateTime/Plugin/jQuery/jQueryUI/jquery-ui.min.js'><" + "/script>");
    }
</script>

<link href="UserControl/ucDateTime/Plugin/jQuery/jQueryUI/Theme/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(function () {
        $('#<%=txtDateTime.ClientID %>').datepicker({
            changeMonth: <%=MonthChange.ToString().ToLower() %>,
            changeYear: <%=YearChange.ToString().ToLower() %>,
            yearRange: '<%=YearRange %>'
        //dateFormat: 'yy-mm-dd',
        //timeFormat: 'HH:mm'
    });

    $.datepicker.regional['MyCalendar'] = {
        closeText: 'Close',
        prevText: 'ก่อนหน้า',
        nextText: 'ถัดไป',
        currentText: 'Now',
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน',
                'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.',
	            'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesShort: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        weekHeader: 'สัปดาห์',
        dateFormat: 'yy-mm-dd',
        timeFormat: 'HH:mm',
        firstDay: 0,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['MyCalendar']);
});
</script>

<style type="text/css">
    .vldDefault
    {
        text-decoration:blink;
        color:#FF0000;
    }
    .ui-widget 
    {
	    font-family: tahoma;
	    font-size: 9pt;
    }
    .ui-datepicker table
    {
        font-size:9pt;
    }
    .ucDateTime
    {
        background-image:url('UserControl/ucDateTime/Images/icCalendar.png');
        background-repeat: no-repeat;
        background-position:left center;
        background-color:#fff;
        border:1px solid #ddd;
        padding:4px 4px 4px 22px;
        width:110px;
    }
</style>

<asp:TextBox ID="txtDateTime" runat="server" CssClass="ucDateTime"/>
<asp:RequiredFieldValidator
    ID="vldDateTime" runat="server" ErrorMessage="โปรดกรอก" Display="Dynamic" CssClass="vldDefault" ControlToValidate="txtDateTime" Enabled="false"></asp:RequiredFieldValidator>