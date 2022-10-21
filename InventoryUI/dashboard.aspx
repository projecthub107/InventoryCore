<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">Dashboard</h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-3 col-md-6">
            <a href="productlist.aspx">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-product-hunt fa-5x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="huge">
                                    <asp:Label ID="lblTotalProductsCount" runat="server"></asp:Label>
                                </div>
                                <div>Products</div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <span class="pull-left">View List</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-6">
            <a href="receiveinventory.aspx">
                <div class="panel panel-green">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-get-pocket fa-5x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="huge">
                                    &nbsp;
                                    <asp:Label ID="lblTotalReceiveInventoryCount" Style="display: none;" runat="server"></asp:Label>
                                </div>
                                <div>Receive Inventory</div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <span class="pull-left">Receive</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-6">
            <a href="removeinventory.aspx">
                <div class="panel panel-red">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-sign-out fa-5x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="huge">
                                    &nbsp;
                                    <asp:Label ID="lblTotalRemoveInventoryCount" Style="display: none;" runat="server"></asp:Label>
                                </div>
                                <div>Remove Inventory</div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <span class="pull-left">Remove</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-6">
            <a href="transferinventory.aspx">
                <div class="panel panel-yellow">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-truck fa-5x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="huge">
                                    &nbsp;
                                    <asp:Label ID="lblTransferInventoryCount" Style="display: none;" runat="server"></asp:Label>
                                </div>
                                <div>Transfer Inventory</div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <span class="pull-left">Transfer</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </a>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">Product List</h3>
        </div>
        <div class="col-lg-12">
            <div class="form-inline">
                <div class="form-group">
                    <label>Product:</label>
                    <asp:TextBox ID="txtProductSearch" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />    
                <asp:Button ID="btn" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" />

            </div>
        </div>
        <br />
        <div class="col-lg-12">
            <asp:GridView ID="grdProductList" runat="server" AutoGenerateColumns="True" DataKeyNames="ProductId"
                Width="80%" CssClass="table table-striped table-bordered table-hover mGrid">
                <PagerStyle CssClass="pgr" />


            </asp:GridView>
        </div>
    </div>
     <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">User List</h3>
        </div>
        <div class="col-lg-12">
            <asp:GridView ID="grdUserList" runat="server" AutoGenerateColumns="True" DataKeyNames="UserId"
                Width="80%" CssClass="table table-striped table-bordered table-hover mGrid">
                <PagerStyle CssClass="pgr" />


            </asp:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div id="calendar">
            </div>

            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"><span id="eventTitle"></span></h4>
                        </div>
                        <div class="modal-body">
                            <p id="pDetails"></p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>




        </div>
    </div>

</asp:Content>

