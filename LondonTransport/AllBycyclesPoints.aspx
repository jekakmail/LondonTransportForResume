<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AllBycyclesPoints.aspx.cs" Inherits="LondonTransport.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="Content/GmapControl.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row gmap">
        <gmaps:GMap ID="GMap1" runat="server" mapType="Normal" Height="100%" Width="100%" />
    </div>
</asp:Content>
