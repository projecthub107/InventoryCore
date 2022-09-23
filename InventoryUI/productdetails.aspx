<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="productdetails.aspx.cs" Inherits="productdetails" %>

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
                <asp:Label ID="lblTitle" runat="server" Text="Add New Product"></asp:Label></h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-6  col-lg-offset-1">

            <div class="form-horizontal">
                <div class="well well-sm">
                    <div class="form-group">
                        <div class="col-lg-3">
                            <label>Code</label>
                            <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label>Name</label>
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 text-center">
                            <asp:ImageButton ID="imgBarCode" ToolTip="Barcode Scanner" runat="server" ImageUrl="~/images/barcode.png" Width="60" Height="60" OnClientClick="alert('Barcode Scanner not found'); return false;" />
                        </div>
                    </div>
                </div>
                <div class="well well-sm">
                    <div class="form-group">
                        <div class="col-lg-12">
                            <label>Description</label>
                            <asp:TextBox ID="txtProductDescription" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <%--<div class="well well-sm">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <label>Size</label>
                            <asp:TextBox ID="txtProductSize" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <label>Wight</label>
                            <asp:TextBox ID="txtProductWight" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <label>Color</label>
                            <asp:TextBox ID="txtProductColor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>--%>
                <div class="well well-sm">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <label>Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <label>Manufacturer</label>
                            <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <label>Area</label>
                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>




                    </div>
                </div>

                <div class="well well-sm">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <label>Minimum Quantity Stock</label>
                            <asp:TextBox ID="txtMinimumQuantityStock" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <label>Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            &nbsp;
                           <%-- <label>Serial</label>
                            <asp:TextBox ID="txtProductSerial" runat="server" CssClass="form-control"></asp:TextBox>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="well well-sm" id="divTransaction" runat="server" visible="true">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-lg-3">
                            <div id="sandbox-container">
                                <label>Transaction Date</label>
                                <div class="input-group date">
                                    <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-th"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <label>Transaction</label>
                            <asp:DropDownList ID="ddlTransaction" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Quantity</label>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            <label>Unit</label>
                            <asp:TextBox ID="txtProductSize" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-2" style="padding-left: 5px;padding-right: 5px;">
                            <label>Transaction#:</label><br />
                            <asp:Label ID="lblTransactionNumber" style="display:inline;" runat="server" Font-Bold="true" Text="RCV-"></asp:Label><asp:TextBox ID="txtTransactionNumber" runat="server" style="width:75px; display:inline;" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>


            </div>
            <asp:Label ID="lblResult" runat="server"></asp:Label>

            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Save changes" OnClick="btnAdd_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
            <asp:HiddenField ID="hdnProductId" runat="server" Value="0" />

        </div>
    </div>
    <br />
</asp:Content>

