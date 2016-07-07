<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AllBycyclesPoints.aspx.cs" Inherits="LondonTransport.AllBycyclesPoints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="Content/GmapControl.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1 class="page-header">All bycyle points</h1>
    <div class="row gmap">
        <gmaps:GMap ID="GMap1" runat="server" mapType="Normal" Height="100%" Width="100%" />
    </div>
     <script >
        $("#AllBycyclePoints").parent("li").addClass("active");
    </script>
</asp:Content>
