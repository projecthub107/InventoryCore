<%@ Page Title="Remove from Inventory" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="deleteinventory.aspx.cs" Inherits="deleteinventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {

            $('#sandbox-container .input-group.date').datepicker({
                todayBtn: "linked",
                orientation: "bottom left",
                autoclose: true,
                todayHighlight: true
            })
        });
    </script>
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">
                <asp:Label ID="lblTitle" runat="server" Text="Remove from Inventory"></asp:Label></h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-5  col-lg-offset-1">
            <div class="row form-inline">
                <div class="col-lg-12">
                    <div class="form-group">
                        <label>Location:</label>
                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row form-inline">
                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Code:</label>
                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Name:</label>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row form-inline">
                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Area:</label>
                        <asp:Label ID="lblArea" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Status:</label>
                        <asp:Label ID="lblStock" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-5  col-lg-offset-1">
            <div role="form">


                <div class="form-group" id="sandbox-container">
                    <label>Transaction Date</label>
                    <div class="input-group date">
                        <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="input-group-addon">
                            <span class="glyphicon glyphicon-th"></span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>Transaction</label>
                    <asp:DropDownList ID="ddlTransaction" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <label>Quantity</label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Transaction #:</label>
                    <asp:Label ID="lblTransactionNumber" runat="server" Font-Bold="true" Text="RMV-"></asp:Label><asp:TextBox ID="txtTransactionNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Label ID="lblResult" runat="server"></asp:Label>

                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Save changes" OnClick="btnAdd_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
                <asp:HiddenField ID="hdnProductId" runat="server" Value="0" />
            </div>
        </div>
    </div>
</asp:Content>

