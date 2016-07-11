<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="BycycleAvailability.aspx.cs" Inherits="LondonTransport.BycyleAvailability" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="Content/GmapControl.css" rel="stylesheet" />
    <link href="Content/BycycleAvailability.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1 class="page-header">Bycycle availability</h1>
    <div class="row form-filter">
        <div class="form-inline">
            <div class="btn-group" role="group">
                <asp:Button ID="BycycleAvailible" runat="server" CssClass="btn btn-success" Text="Availible bike" OnClick="BycycleAvailible_OnClick"/>

                <asp:Button ID="EmptyDocks" runat="server" CssClass="btn btn-default" Text="Empty docks" OnClick="EmptyDocks_OnClick"/>

                <asp:Button ID="LockedPoints" runat="server" CssClass="btn btn-warning" Text="Locked points" OnClick="LockedPoints_OnClick"/>

            </div>
            <asp:Button ID="ShowAllPoints" runat="server" CssClass="btn btn-info" Text="Show all points" OnClick="ShowAllPoints_OnClick"/>
        </div>
        <pre class="labelInfo"><%=LabelInfo %></pre>
    </div>
    <div class="row gmap">
        <p ID="WarningJS" class="bg-danger">For correct display of results, including JavaScript!</p>
        <gmaps:GMap ID="GMap1" runat="server" mapType="Normal" Height="100%" Width="100%" />
    </div>
    <script src="Scripts/WarningJSRemove.js"></script>
     <script >
         $("#BycycleAvailability").parent("li").addClass("active");
    </script>
</asp:Content>
