<%@ Page Title="Product List" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="productlist.aspx.cs" Inherits="productlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <div class="row">

        <div class="col-lg-12">
            <div class="page-header">
                <h3 style="display: inline-block;">
                    <asp:Label ID="lblTitle" runat="server" Text="Product List"></asp:Label></h3>
                <div style="display: inline-block; float: right;">
                    <div class=" form-inline">
                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New Product" OnClick="btnAdd_Click" />
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
                    <label>Location</label>
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
                 &nbsp;
                 <div class="form-group">
                    <label>Page</label> &nbsp;
                    <asp:Label ID="lblCurrentPageNo" runat="server" Font-Bold="true" ForeColor="#000000"></asp:Label>
                </div>
                 &nbsp; &nbsp;
                <div class="form-group">
                    <label>Item per page</label>
                    <asp:DropDownList ID="ddlItemPerPage" runat="server" AutoPostBack="True" CssClass="form-control"
                        OnSelectedIndexChanged="ddlItemPerPage_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem Value="4">All</asp:ListItem>
                    </asp:DropDownList>
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
            <asp:GridView ID="grdProductList" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ProductId" 
                OnRowDataBound="grdProductList_RowDataBound"  OnPageIndexChanging="grdProductList_PageIndexChanging"
                Width="80%" CssClass="table table-striped table-bordered table-hover mGrid">
                <PagerStyle CssClass="pgr" />
                <PagerSettings Position="TopAndBottom" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="productdetails.aspx?pid={0}" DataTextField="ProductCode" HeaderText="Product Code">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>

                    <asp:HyperLinkField DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="productdetails.aspx?pid={0}" DataTextField="ProductName" HeaderText="Product Name">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                   
                    <asp:BoundField HeaderText="Area" DataField="AreaName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Manufacturer" DataField="ManufacturerName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>


                    <asp:TemplateField HeaderText="Stock Status">
                        <ItemTemplate>
                            <asp:GridView ID="grdStockList" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ProductId" ShowHeader="false" CssClass="noCSS">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="ProductId,LocationId" DataNavigateUrlFormatString="inventorydetails.aspx?pid={0}&&lid={1}" DataTextField="StockStatus" ShowHeader="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:HyperLinkField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:HiddenField ID="hdnProductId" runat="server" Value="0" />
</asp:Content>

