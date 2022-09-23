<%@ Page Title="Manufacturer List" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="manufacturerlist.aspx.cs" Inherits="manufacturerlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <div class="row">

        <div class="col-lg-12">
            <div class="page-header">
                <h3 style="display: inline-block;">
                    <asp:Label ID="lblTitle" runat="server" Text="Manufacturer List"></asp:Label></h3>
                <div style="display: inline-block; float: right;">
                    <div class=" form-inline">
                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New Manufacturer" OnClick="btnAdd_Click" />
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
            <asp:GridView ID="grdManufacturerList" runat="server" AllowPaging="True" OnPageIndexChanging="grdManufacturerList_PageIndexChanging"
                AutoGenerateColumns="False" DataKeyNames="ManufacturerId" Width="80%" CssClass="table table-striped table-bordered table-hover mGrid">
                <PagerStyle CssClass="pgr" />
                <PagerSettings Position="TopAndBottom" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="ManufacturerId" DataNavigateUrlFormatString="manufacturerdetails.aspx?mid={0}" DataTextField="ManufacturerName" HeaderText="Manufacturer Name">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>

                    <asp:BoundField HeaderText="City" DataField="City">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="State" DataField="State">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Zip" DataField="Zip">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Active" DataField="Active">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:HiddenField ID="hdnManufacturerId" runat="server" Value="0" />
</asp:Content>

