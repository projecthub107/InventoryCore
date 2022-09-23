<%@ Page Title="Preferences" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SetPreferences.aspx.cs" Inherits="SetPreferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">
                <asp:Label ID="lblTitle" runat="server" Text="Preferences"></asp:Label></h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>

    <br />
    <div class="row">
        <div class="col-lg-5  col-lg-offset-1">
            <div role="form">


                <div class="form-group" id="sandbox-container">
                    <label>Minimum Stock</label>

                    <asp:TextBox ID="txtMinimumStock" runat="server" CssClass="form-control"></asp:TextBox>


                </div>
            <div class="form-group">
                <label>Currency</label>
                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>


            <div class="form-group">
                <label>Location</label>
                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>


            <asp:Label ID="lblResult" runat="server"></asp:Label>

            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Save changes" OnClick="btnAdd_Click" />
            <asp:Button ID="Button2" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
            <asp:HiddenField ID="hdnPreferencesId" runat="server" Value="0" />
        </div>
    </div>
    </div>
</asp:Content>

