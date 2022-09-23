<%@ Page Title="Category Details" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="categorydetails.aspx.cs" Inherits="categorydetails" %>

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
                <asp:Label ID="lblTitle" runat="server" Text="Add New Category"></asp:Label></h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-6  col-lg-offset-1">

            <div class="form-horizontal">
                <div class="well well-sm">
                   
                    <div class="form-group">
                        <label class="col-lg-12">Name</label>
                        <div class="col-lg-12">
                            <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-12">Serial</label>
                        <div class="col-lg-12">
                            <asp:TextBox ID="txtSerial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
               
                

                <div class="well well-sm">
                    <div class="form-group">
                       
                        <div class="col-lg-4">
                            <label>Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

          
            <asp:Label ID="lblResult" runat="server"></asp:Label>

            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Save changes" OnClick="btnAdd_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
            <asp:HiddenField ID="hdnCategoryId" runat="server" Value="0" />

        </div>
    </div>
    <br />
</asp:Content>

