<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SaharaLabel.ChangePassword" %>

<!DOCTYPE html>
<style>
    <!--
    body {
        background-image: url(assets/Images/avatars/og-etap-generic.jpg) !important;
        -webkit-background-size: cover;
        -moz-background-size: cover;
        -o-background-size: cover;
        background-size: cover;
        opacity: 0.70;
        z-index: -1;
        filter: alpha(opacity=90);
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="main.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="app-container app-theme-white body-tabs-shadow" id="app-container">
                <div class="app-container img">
                    <div class="h-100 bg-deep-blue bg-animation">
                        <div class="d-flex h-100 justify-content-center  align-items-center">
                            <div class="mx-auto app-login-box col-md-6">
                            </div>
                            <div class="mx-auto app-login-box col-md-6">
                                <div class="app-logo-inverse mx-auto mb-3"></div>
                                <div class="modal-dialog w-100 mx-auto">
                                    <div class="modal-content ">
                                        <div class="modal-body">
                                            <div class="h5 modal-title text-center">
                                                <h4 class="mt-2">UPDATE PASSWORD
                                                </h4>
                                            </div>

                                            <div class="form-row">                                              

                                                <div class="col-md-12">
                                                    <div class="position-relative form-group" style="margin: 6px;">
                                                        <asp:TextBox ID="txtUserName" runat="server" placeholder="User Name here..." class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="position-relative form-group" style="margin: 6px;">
                                                        <asp:TextBox ID="txtOldPassword" runat="server" placeholder="Old Password here..." type="password" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="position-relative form-group" style="margin: 6px;">
                                                        <asp:TextBox ID="txtNewPassword" runat="server" placeholder="New Password here..." type="password" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="position-relative form-group" style="margin: 6px;">
                                                        <asp:TextBox ID="txtConfirmPassword" runat="server" placeholder="Confirm Password here..." type="password" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                        <div class="modal-footer clearfix">
                                             
                                            <div class="float-right">
                                                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-lg" Text="Save" OnClick="Button1_Click" />
                                                <%--<asp:Button ID="Button2" runat="server" class="btn btn-primary btn-lg" Text="Cancel" OnClick="Button2_Click" />--%>
                                            </div>
                                        </div>
                                    </div>
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
