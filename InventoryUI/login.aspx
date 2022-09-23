<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Login</title>

    <!-- Bootstrap Core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="vendor/metisMenu/metisMenu.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <link href="css/style.css" rel="stylesheet" />

    <!-- jQuery -->
    <script src="vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="dist/js/sb-admin-2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row login-panel">
                <div class="col-md-4 col-md-offset-4">
                    <img src="images/inventory.png" class="img-responsive login-panel-img"  />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div class=" panel panel-primary">
                        <div class="panel-heading text-center">

                            <h2 class="panel-title">Login</h2>
                        </div>
                        <div class="panel-body">
                            <div role="form">
                                <fieldset>

                                    <div class="form-group input-group">
                                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="E-mail" name="email" type="email" autofocus></asp:TextBox>
                                    </div>
                                    <div class="form-group input-group">
                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" name="password" type="password" Text=""></asp:TextBox>
                                    </div>

                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-lg btn-success btn-block" Text="Login" OnClick="btnLogIn_Click" />
                                    <asp:Label ID="lblResult" runat="server"></asp:Label>
                                    <div class="clearfix">

                                        <a href="#" class="pull-right">Forgot Password?</a>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
