<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SearchInRadius.aspx.cs" Inherits="LondonTransport.SearchInRadius" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="Content/GmapControl.css" rel="stylesheet" />
    <link href="Content/SearchInRadius.css" rel="stylesheet" />
    <script src="Scripts/SearchInRadius.js"></script>
    <script>
        window.onload = function ()
        {
            $("#ContentPlaceHolder_numMetres").bind("click keyup", function() {
                metres = $("#ContentPlaceHolder_numMetres").val();
            });

            $("#getCoordinate").removeClass("disabled");
            $("#SearchInRadius").parent("li").addClass("active");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
    <h1 class="page-header">Search bycycle in radius</h1>
    <div class="row">
        <div class="col-lg-2">
            <button ID="getCoordinate" type="button" class="btn btn-success disabled" onclick="getCoordinates()"><i class="fa fa-compass fa-inverse fa-lg" aria-hidden="true"></i> Get my location</button>
        </div>
        <div class="col-lg-4">
            <div class="form-group">
                <div class="input-group">
                    <asp:TextBox ID="numMetres" TextMode="Number" runat="server" CssClass="form-control" placeholder="Radius in metres" ValidateRequestMode="Enabled" Text="500" ></asp:TextBox>
                    <span class="input-group-addon" id="basic-addon2"> metres</span>
                </div><!-- /input-group -->
            </div>
        </div><!-- /.col-lg-4 -->
    </div>
    <div class="row gmap">
        <p ID="WarningJS" class="bg-danger">For correct display of results, including JavaScript!</p>
        <gmaps:GMap ID="GMap1" runat="server" mapType="Normal" Height="100%" Width="100%"
                    enableServerEvents="true"
                    OnClick="GMap1_Click" />
    </div>
    <script src="Scripts/WarningJSRemove.js"></script>
    <script>var metres = $("#ContentPlaceHolder_numMetres").val();</script>
    
</asp:Content>
