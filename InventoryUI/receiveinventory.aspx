<%@ Page Title="Receive Inventory" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="receiveinventory.aspx.cs" Inherits="receiveinventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <div class="row">

        <div class="col-lg-12">
            <div class="page-header">
                <h3 style="display: inline-block;">
                    <asp:Label ID="lblTitle" runat="server" Text="Receive Inventory"></asp:Label></h3>
                <div style="display: inline-block; float: right;">
                    <div class=" form-inline">
                        <div class="form-group">
                            <label>Transaction #:</label>
                            <asp:Label ID="lblTransactionNumber" runat="server" Font-Bold="true" Text="RCV-"></asp:Label><asp:TextBox ID="txtTransactionNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="form-inline">
                <div class="form-group">
                    <label>Product:</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Quantity:</label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Style="width: 100px;"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:DropDownList ID="ddlTransaction" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
                 &nbsp;
                <div class="form-group">                   
                    <asp:ImageButton ID="imgBarCode" ToolTip="Barcode Scanner" CssClass="img-responsive" runat="server" ImageUrl="~/images/barcode.png" Width="40" Height="40" OnClientClick="alert('Barcode Scanner not found'); return false;" />
                </div>
            </div>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblResult" runat="server"></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">

        <div class="col-lg-12">
            <asp:GridView ID="grdProductList" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="grdProductList_RowDataBound"
                DataKeyNames="ProductId" Width="80%"
                CssClass="table table-striped table-bordered table-hover">
                <Columns>
                    <asp:TemplateField HeaderText="Product Code">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkgrdProductDetails" runat="server" OnClick="lnkgrdProductDetails_Click" Text='<%#Eval("ProductCode") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Product Name" DataField="ProductName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Area" DataField="AreaName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Manufacturer" DataField="ManufacturerName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Transaction">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlgrdTransaction" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlgrdTransaction_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="inventorydetails.aspx?pid={0}" DataTextField="StockStatus" HeaderText="Stock Status">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:HyperLinkField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgrdQuantity" runat="server" CssClass="form-control" Text='<%#Eval("NewQuantity") %>' OnTextChanged="txtgrdQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="btngrdRemove" runat="server" OnClick="btngrdRemove_Click" ForeColor="Red" ImageUrl="~/images/remove.png"></asp:ImageButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-lg-12">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" Visible="false" />
        </div>
    </div>
</asp:Content>

