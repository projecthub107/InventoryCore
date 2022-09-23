<%@ Page Title="Receive Inventory" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="receiveinventory_.aspx.cs" Inherits="receiveinventory_" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <div class="row">

        <div class="col-lg-12">
            <div class="page-header">
                <h3 style="display: inline-block;">
                    <asp:Label ID="lblTitle" runat="server" Text="Receive Inventory"></asp:Label></h3>

            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3">
            <div class="input-group custom-search-form">
                <input type="text" class="form-control" placeholder="Search...">
                <span class="input-group-btn">
                    <button class="btn btn-default" type="button">
                        <i class="fa fa-search"></i>
                    </button>
                </span>
            </div>
        </div>
        <div class="col-lg-3 col-lg-offset-5">
             <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Save"  />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Cancel"  />
        </div>
    </div>
    <br />
    <div class="row">

        <div class="col-lg-12">
            <asp:GridView ID="grdProductList" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ProductId" Width="80%" CssClass="table table-striped table-bordered table-hover">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="productdetails.aspx?pid={0}" DataTextField="ProductCode" HeaderText="Product Code">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>

                    <asp:BoundField HeaderText="Product Name" DataField="ProductName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="inventorydetails.aspx?pid={0}" DataTextField="StockStatus" HeaderText="Stock Status">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Area">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manufacturer">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

