<%@ Page Title="User List" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="userlist.aspx.cs" Inherits="userlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <div class="row">

        <div class="col-lg-12">
            <div class="page-header">
                <h3 style="display: inline-block;">
                    <asp:Label ID="lblTitle" runat="server" Text="Product List"></asp:Label></h3>
                <div style="display: inline-block; float: right;">
                    <div class=" form-inline">
                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New User" OnClick="btnAdd_Click" />
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
                    <label>User:</label>
                    <asp:TextBox ID="txtUserSearch" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        <asp:ListItem Text="All" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Role</label>
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
                &nbsp;
                <div class="form-group">
                    <label>Page</label>
                    &nbsp;
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
            <asp:GridView ID="grdUserList" OnPageIndexChanging="grdUserList_PageIndexChanging" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="UserId"
                Width="80%" CssClass="table table-striped table-bordered table-hover mGrid">
                <PagerStyle CssClass="pgr" />
                <PagerSettings Position="TopAndBottom" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="userdetails.aspx?uid={0}" DataTextField="UserName" HeaderText="User Name">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>

                    <asp:BoundField HeaderText="Email" DataField="Email">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Active" DataField="Active">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Role" DataField="Role">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:HiddenField ID="hdnUserId" runat="server" Value="0" />
</asp:Content>
