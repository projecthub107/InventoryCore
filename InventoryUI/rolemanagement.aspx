<%@ Page Title="Role Management" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="rolemanagement.aspx.cs" Inherits="rolemanagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">
                <asp:Label ID="lblTitle" runat="server" Text="Role Management"></asp:Label></h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="form-horizontal">
                <div class="well well-sm">
                    <div class="form-group">                       
                        <div class="col-lg-4">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                     <label>Role</label>
                                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="well well-sm">
                    <div class="form-group">
                        <div class="col-lg-12">
                            <label>Select User Interfaces</label>                          
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Height="570px" ScrollBars="Vertical"
                                        Width="100%">
                                        <asp:TreeView ID="trvMenu" runat="server" ForeColor="Black"
                                            OnTreeNodeCheckChanged="trvMenu_TreeNodeCheckChanged" ShowCheckBoxes="All"
                                            ShowLines="True">
                                            <ParentNodeStyle Font-Bold="False" />
                                            <RootNodeStyle Font-Bold="True" />
                                            <NodeStyle Font-Bold="False" />
                                        </asp:TreeView>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlRoles"
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Save changes" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
        </div>
    </div>
    <br />

    <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="False">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <p>
                    Please wait while your data is being processed
                </p>
                <img src="images/ajax_loader.gif" alt="Loading" border="1" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
