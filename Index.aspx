<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SaharaLabel.Index" %>

<!DOCTYPE html>
<style>
    <!--
    body {
        /*background-image: url(assets/Images/avatars/Home-banner4.jpg) !important;
        -webkit-background-size: cover;
        -moz-background-size: cover;
        -o-background-size: cover;
        background-size: 122%;
        background-repeat: no-repeat;
        opacity: 0.60;
        z-index: -1;
        filter: alpha(opacity=90);*/
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="main.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="app-container">
                <div class="app-container img">

                    <div class="h-100 bg-deep-white bg-animation">
                        <div class="d-flex h-120 justify-content-center  align-items-center">

                            <div class="mx-auto app-login-box col-md-5">
                                <div class="app-logo-inverse mx-auto mb-5"></div>
                                <div class="h5 modal-title text-center" style="font-style: ">
                                    <img src="assets/images/SaharaLogo1.png" alt="Sample Photo" />
                                </div>
                                <div class="h5 modal-title text-center" style="font-style: ">
                                    <h2 class="mt-2">SAHARA LABELS PVT. LTD.
                                    </h2>
                                </div>
                                <div class="modal-dialog w-100 mx-auto">

                                    <div class="modal-content ">

                                        <div class="modal-body">
                                            <div class="h5 modal-title text-center" style="font-style: ">
                                                <h4 class="mt-2">Login
                                                </h4>
                                            </div>

                                            <div class="form-row">

                                                <div class="col-md-12">
                                                    <div class="position-relative form-group" style="margin: 6px;">
                                                        <asp:DropDownList ID="CboYear" runat="server" Font-Bold="true" class="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="position-relative form-group" style="margin: 6px;">
                                                        <asp:TextBox ID="txtUserName" runat="server" Font-Bold="true" placeholder="User Name here..." class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="position-relative form-group" style="margin: 6px;">
                                                        <asp:TextBox ID="txtPassword" runat="server" Font-Bold="true" placeholder="Password here..." type="password" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                        <div class="modal-footer clearfix">
                                            <div class="float-right">
                                                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-lg" Text="Login" OnClick="Button1_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                </br> </br> </br> </br> </br>
                                 <div class="h5 modal-title text-center" style="font-style: ">
                                    <p style="font-size:12px;font-family:Verdana, Arial, Helvetica, sans-serif">Copyright  2022. <!-- Do not remove -->Designed and developed by <a href="http://www.3sps.in" target="_blank" title="Quadrant 3S ERP Solutions Pvt. Ltd"><span style="color:red">3S</span></a> <a href="http://www.3sps.in" target="_blank" title="Quadrant 3S ERP Solutions Pvt. Ltd">Quadrant 3S ERP Solutions Pvt. Ltd</a><!-- end --></p>
		       				
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
