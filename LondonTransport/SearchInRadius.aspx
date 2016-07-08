<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SearchInRadius.aspx.cs" Inherits="LondonTransport.SearchInRadius" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="Content/GmapControl.css" rel="stylesheet" />
    <link href="Content/SearchInRadius.css" rel="stylesheet" />
    <script type="text/javascript">
        function getCoordinates() {
            navigator.geolocation.getCurrentPosition(showCoordinates);
        }

        function showCoordinates(position) {
            document.write('Широта: ' + position.coords.latitude + "<br />");
            document.write('Долгота: ' + position.coords.longitude);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1 class="page-header">Search bycycle in radius</h1>
    <div class="row">
        <div class="col-lg-2">
            <button type="button" class="btn btn-success" onclick="getCoordinates()"><i class="fa fa-compass fa-inverse fa-lg" aria-hidden="true"></i> Get my location</button>
        </div>
        <div class="col-lg-4">
            <div class="form-group">
                <div class="input-group">
                    <asp:TextBox TextMode="Number" runat="server" CssClass="form-control" placeholder="Radius in metres" ValidateRequestMode="Enabled"></asp:TextBox>
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button">Go!</button>
                    </span>
                </div><!-- /input-group -->
            </div>
        </div><!-- /.col-lg-4 -->
    </div>
    <div class="row gmap">
        <gmaps:GMap ID="GMap1" runat="server" mapType="Normal" Height="100%" Width="100%"
                    enableServerEvents="true"
                    OnServerEvent="GMap1_ServerEvent"/>
    </div>
    <script >
        $("#SearchInRadius").parent("li").addClass("active");
    </script>
</asp:Content>
