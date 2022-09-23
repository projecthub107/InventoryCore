<%@ Page Title="User Details" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="userdetails.aspx.cs" Inherits="userdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">
                <asp:Label ID="lblTitle" runat="server" Text="Add New User"></asp:Label></h3>
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
                            <asp:TextBox ID="txtUser" runat="server" autocomplete="new-User"  CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-12">Email</label>
                        <div class="col-lg-12">                           
                            <asp:TextBox ID="txtEmail" runat="server" autocomplete="new-Email" TextMode="Email" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="well well-sm">
                    <div class="form-group">
                        <div class="col-lg-6">
                            <label>Password</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" autocomplete="new-Password"   runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label>Confirm Password</label>
                            <asp:TextBox ID="txtConfirmPass" TextMode="Password" autocomplete="new-Password"  runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        
                    </div>
                      <div class="form-group">
                        <div class="col-lg-6">
                            <label>Password Verification Question</label>
                          <asp:DropDownList ID="ddlQuestion" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6">
                            <label>Answer</label>
                            <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        
                    </div>
                </div>
                <div class="well well-sm">
                    <div class="form-group">
                        
                        <div class="col-lg-4">
                            <label>Role</label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
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
            <asp:HiddenField ID="hdnUserId" runat="server" Value="0" />

        </div>
    </div>
    <br />
</asp:Content>

