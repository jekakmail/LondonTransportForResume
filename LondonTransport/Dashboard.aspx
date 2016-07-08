<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="LondonTransport.Dashboard" %>
<%@ Import Namespace="LondonTransport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="Content/dashboard.css" rel="stylesheet" />
    <script src="Scripts/bs.pagination.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1 class="page-header">Dashboard</h1>
    <div class="row placeholders">
        <div class="col-xs-6 col-sm-3 placeholder">
            <span class="fa-stack fa-5x icon-park">
                <i class="fa fa-circle fa-stack-2x"></i>
                <i class="fa fa-map-marker fa-stack-1x fa-inverse" aria-hidden="true"></i>
            </span>
            <h4><%=CountPoints %></h4>
            <span class="text-muted">Bycycle points</span>
        </div>
        <div class="col-xs-6 col-sm-3 placeholder">
            <span class="fa-stack fa-5x icon-check">
                <i class="fa fa-circle fa-stack-2x"></i>
                <i class="fa fa-check fa-stack-1x fa-inverse"></i>
            </span>
            <h4><%=AvailibleBycycles%></h4>
            <span class="text-muted">Availible bycycles</span>
        </div>
        <div class="col-xs-6 col-sm-3 placeholder">
            <span class="fa-stack fa-5x icon-lowbattery">
                <i class="fa fa-circle fa-stack-2x"></i>
                <i class="fa fa-battery-empty fa-stack-1x fa-inverse"></i>
            </span>
            <h4><%=EmptyDocks%></h4>
            <span class="text-muted">Empty docks</span>
        </div>
        <div class="col-xs-6 col-sm-3 placeholder">
            <span class="fa-stack fa-5x icon-cycle">
                <i class="fa fa-circle fa-stack-2x"></i>
                <i class="fa fa-bicycle fa-flip-horizontal fa-stack-1x fa-inverse"></i>
            </span>
            <h4><%=Docks%></h4>
            <span class="text-muted">Count docks</span>
        </div>
    </div>

    <h2 class="sub-header">Bycyle points</h2>
    <div class="table-responsive">
        <asp:GridView ID="cyclePointsInfo" runat="server" 
            CssClass="table table-striped" AutoGenerateColumns="False" GridLines="None" ShowHeader="True" 
            PageSize="10"
            AllowPaging="True" PagerStyle-CssClass="bs-pagination" OnPageIndexChanging ="cyclePointsInfo_OnPageIndexChanging">
            <Columns>
                <asp:BoundField DataField="CommonName" HeaderText="Name"/>
                <asp:BoundField DataField="AvailibleBike" HeaderText="Availible Bike" SortExpression="AvailibleBike"/>
                <asp:BoundField DataField="EmptyDocks" HeaderText="Empty Docks"/>
                <asp:BoundField DataField="Docks" HeaderText="Docks"/>
            </Columns>
        </asp:GridView>
    </div>
    <script >
        $("#Dashboard").parent("li").addClass("active");
    </script>
</asp:Content>
