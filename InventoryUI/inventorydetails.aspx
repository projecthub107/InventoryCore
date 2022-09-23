<%@ Page Title="Inventory Details" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="inventorydetails.aspx.cs" Inherits="inventorydetails" %>

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
            <div class="page-header">
                <h3 style="display: inline-block;">Inventory Details</h3>
                <div style="display: inline-block; float: right;">
                    <div class=" form-inline">
                        <div class="form-group">
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Back to Product list" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit Product" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add to Inventory" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnReomove" runat="server" CssClass="btn btn-warning" Text="Remove From Inventory" OnClick="btnReomove_Click" />
                            <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-success" Text="Transfer Product" OnClick="btnTransfer_Click" Visible="false" />
                        </div>
                    </div>
                </div>

            </div>
        </div>


    </div>
    <div class="row">
        <div class="col-lg-5">
            <div class="row form-inline">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Location:</label>
                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-6">
                </div>
            </div>
            <div class="row form-inline">
                <div class="col-lg-6">
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
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Area:</label>
                        <asp:Label ID="lblArea" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Status:</label>
                        <asp:Label ID="lblStock" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <br />
    <div class="well" style="width: 80%;" id="divTransfer" runat="server" visible="false">
        <div class="row">
            <div class="col-lg-12">
                <h4>Transfer                    
                    from
                    <asp:Label ID="lblTransferFrom" runat="server" Font-Bold="true"></asp:Label></h4>
            </div>
            <div class="col-lg-5  ">
                <div role="form">
                    <div class="form-group">
                        <label>To</label>
                        <asp:DropDownList ID="ddlTransferToLocation" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

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
                        <label>Quantity</label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>


                    <div class="form-group">
                        <label>Transaction #:</label>
                        <asp:Label ID="lblTransactionNumber" runat="server" Font-Bold="true" Text="TRNS-"></asp:Label><asp:TextBox ID="txtTransactionNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <asp:Label ID="lblResult" runat="server"></asp:Label>

                    <asp:Button ID="btnSaveTransfer" runat="server" CssClass="btn btn-success" Text="Save changes" OnClick="btnSaveTransfer_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="btnClose_Click" />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-12">
        </div>
        <div class="col-lg-12">
            <asp:GridView ID="grdInventoryList" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductId" Width="80%" CssClass="table table-striped table-bordered table-hover">
                <Columns>
                    <asp:BoundField HeaderText="LocationName" DataField="LocationName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Transaction Date" DataField="TransactionDate" DataFormatString="{0:d}">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Quantity In" DataField="QuantityIn">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Quantity Out" DataField="QuantityOut">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Transaction Type" DataField="TransactionName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:HiddenField ID="hdnProductId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnLocationId" runat="server" Value="0" />
</asp:Content>

