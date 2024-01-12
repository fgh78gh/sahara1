<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SaharaLabel.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta http-equiv="Content-Language" content="en">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="main.css" rel="stylesheet">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="app-container app-theme-white body-tabs-shadow fixed-sidebar fixed-header">
            <div class="app-header header-shadow">
                <div class="app-header__logo">
                    <div class="logo-src" runat="server" ></div>
                    <div class="header__pane ml-auto">
                        <div>
                            <button type="button" class="hamburger close-sidebar-btn hamburger--elastic" data-class="closed-sidebar">
                                <span class="hamburger-box">
                                    <span class="hamburger-inner"></span>
                                </span>
                            </button>
                        </div>
                    </div>
                </div>
                <div class="app-header__mobile-menu">
                    <div>
                        <button type="button" class="hamburger hamburger--elastic mobile-toggle-nav">
                            <span class="hamburger-box">
                                <span class="hamburger-inner"></span>
                            </span>
                        </button>
                    </div>
                </div>
                <div class="app-header__menu">
                    <span>
                        <button type="button" class="btn-icon btn-icon-only btn btn-primary btn-sm mobile-toggle-header-nav">
                            <span class="btn-icon-wrapper">
                                <i class="fa fa-ellipsis-v fa-w-6"></i>
                            </span>
                        </button>
                    </span>
                </div>
                  <div class="app-header__content"> 
                      <div class="logo-src1" runat="server"  id="DisplayName"></div>
                    <div class="app-header-right">
                        <div class="header-btn-lg pr-0">
                            <div class="widget-content p-0">
                                <div class="widget-content-wrapper">
                                    <div class="widget-content-left">
                                        <div class="btn-group">
                                            <a data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="p-0 btn">
                                                <img width="42" height="48" runat="server" id="UserImage" class="rounded-circle" alt="">
                                            </a>

                                        </div>
                                    </div>
                                    <div class="widget-content-left  ml-3 header-user-info">
                                        <div class="widget-heading" style="text-align: center;" runat="server" id="UserName">
                                        </div>
                                        <div class="widget-subheading" style="opacity: 0.4; font-weight: bold;" runat="server" id="UserDesignation">
                                        </div>
                                    </div>
                                    <div class="widget-content-right header-user-info ml-3">
                                        <asp:ImageButton ImageUrl="~/logout.png" runat="server" Height="25" Width="25" ID="btnLogout" OnClick="btnLogout_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ui-theme-settings">
                <%--  <button type="button" id="TooltipDemo" class="btn-open-options btn btn-warning">
                    <i class="fa fa-cog fa-w-16 fa-spin fa-2x"></i>
                </button>--%>
                <div class="theme-settings__inner">
                    <div class="scrollbar-container">
                    </div>
                </div>
            </div>
            <div class="app-main">
                <div class="app-sidebar sidebar-shadow">
                    <div class="app-header__logo">
                        <div class="logo-src"></div>
                        <div class="header__pane ml-auto">
                            <div>
                                <button type="button" class="hamburger close-sidebar-btn hamburger--elastic" data-class="closed-sidebar">
                                    <span class="hamburger-box">
                                        <span class="hamburger-inner"></span>
                                    </span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="app-header__mobile-menu">
                        <div>
                            <button type="button" class="hamburger hamburger--elastic mobile-toggle-nav">
                                <span class="hamburger-box">
                                    <span class="hamburger-inner"></span>
                                </span>
                            </button>
                        </div>
                    </div>
                    <div class="app-header__menu">
                        <span>
                            <button type="button" class="btn-icon btn-icon-only btn btn-primary btn-sm mobile-toggle-header-nav">
                                <span class="btn-icon-wrapper">
                                    <i class="fa fa-ellipsis-v fa-w-6"></i>
                                </span>
                            </button>
                        </span>
                    </div>
                    <div class="scrollbar-sidebar">
                        <div class="app-sidebar__inner" id="dvMenu" runat="server">
                        </div>
                    </div>
                </div>
                <div class="app-main__outer">
                    <div class="app-main__inner">

                        <div id="row1" runat="server" class="row">
                            <div class="col-md-6 col-xl-3">

                                <div class="card mb-3 widget-content bg-arielle-smile">
                                    <div class="widget-content-wrapper text-white">
                                        <div class="widget-content-left">
                                            <asp:LinkButton ID="lnkOpenJobSheet" ForeColor="White" Font-Size="16px" runat="server" OnClick="lnkOpenJobSheet_Click">Open Job Sheet</asp:LinkButton>
                                        </div>
                                        <div class="widget-content-right">
                                            <div class="widget-numbers text-white"><span runat="server" id="spnOpenJobSheet"></span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="card mb-3 widget-content bg-arielle-smile">
                                    <div class="widget-content-wrapper text-white">
                                        <div class="widget-content-left">
                                            <asp:LinkButton ID="lnkOpenSapmleChart" ForeColor="White" Font-Size="16px" runat="server" OnClick="lnkOpenSapmleChart_Click">Open Sample Cart</asp:LinkButton>

                                        </div>
                                        <div class="widget-content-right">
                                            <div class="widget-numbers text-white"><span id="spnOpenSapmleChart" runat="server"></span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 col-xl-3">
                                <div class="card mb-3 widget-content bg-arielle-smile">
                                    <div class="widget-content-wrapper text-white">
                                        <div class="widget-content-left">
                                            <asp:LinkButton ID="lnkButtonTotadyPlan" ForeColor="White" Font-Size="16px" runat="server" OnClick="lnkButtonTotadyPlan_Click">Today Plan</asp:LinkButton>

                                        </div>
                                        <div class="widget-content-right">
                                            <div class="widget-numbers text-white"><span id="spntodayPlan" runat="server"></span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="card mb-3 widget-content bg-arielle-smile">
                                    <div class="widget-content-wrapper text-white">
                                        <div class="widget-content-left">
                                            <asp:LinkButton ID="lnkButtonTotadyCompleted" ForeColor="White" Font-Size="16px" runat="server" OnClick="lnkButtonTotadyCompleted_Click">Today Completed</asp:LinkButton>

                                        </div>
                                        <div class="widget-content-right">
                                            <div class="widget-numbers text-white"><span id="spntodayCompleted" runat="server"></span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                        </div>

                        <div class="row">

                            <asp:GridView ID="gvPendingJobList" class="mb-0 table table-hover table-bordered" runat="server" ShowFooter="false" Width="1000px"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="JobSheetNo" HeaderText="Job Sheet No" SortExpression="JobSheetNo" ItemStyle-Width="150">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>
                                    <asp:BoundField DataField="RequiredDate" HeaderText="Required Date" SortExpression="RequiredDate" ItemStyle-Width="100">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemCode" HeaderText="FG Item Code" SortExpression="FGItemCode" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemName" HeaderText="FG Item Name" SortExpression="FGItemName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DrawingNo" HeaderText="Drawing No" SortExpression="DrawingNo" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductionQty" HeaderText="Production Qty" SortExpression="ProductionQty" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkView" Text="View" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Production/Transaction/JobSheetView.aspx?JobId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkPrint" Text="Print" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Planning/Reports/PlanningReport.aspx?REPORTTYPE=JobSheetPrint&JobId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#CBDBEA" />
                                <FooterStyle BackColor="#CBDBEA" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <PagerStyle BackColor="#CBDBEA" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#CBDBEA" Font-Bold="False" ForeColor="Black" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" Font-Bold="false" ForeColor="Black" />
                                <RowStyle BackColor="White" ForeColor="Black" Font-Bold="false" />
                            </asp:GridView>
                        </div>
                        <div class="row">

                            <asp:GridView ID="gvSampleChartList" class="mb-0 table table-hover table-bordered" runat="server" ShowFooter="false" Width="1000px"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="JobSheetNo" HeaderText="Sample Cart No" SortExpression="JobSheetNo" ItemStyle-Width="150">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>
                                    <asp:BoundField DataField="RequiredDate" HeaderText="SC Date" SortExpression="RequiredDate" ItemStyle-Width="100">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemCode" HeaderText="FG Item Code" SortExpression="FGItemCode" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemName" HeaderText="FG Item Name" SortExpression="FGItemName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DrawingNo" HeaderText="Drawing No" SortExpression="DrawingNo" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductionQty" HeaderText="Production Qty" SortExpression="ProductionQty" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkPrint" Text="Print" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Engineering/Reports/EngineeringReport.aspx?REPORTTYPE=SampleCartPrint&SampleId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#CBDBEA" />
                                <FooterStyle BackColor="#CBDBEA" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <PagerStyle BackColor="#CBDBEA" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#CBDBEA" Font-Bold="False" ForeColor="Black" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" Font-Bold="false" ForeColor="Black" />
                                <RowStyle BackColor="White" ForeColor="Black" Font-Bold="false" />
                            </asp:GridView>
                        </div>

                        <div class="row">

                            <asp:GridView ID="gvTodayPlan" class="mb-0 table table-hover table-bordered" runat="server" ShowFooter="false" Width="1000px"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="JobSheetNo" HeaderText="Job Sheet No" SortExpression="JobSheetNo" ItemStyle-Width="150">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>
                                    <asp:BoundField DataField="RequiredDate" HeaderText="Required Date" SortExpression="RequiredDate" ItemStyle-Width="100">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemCode" HeaderText="FG Item Code" SortExpression="FGItemCode" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemName" HeaderText="FG Item Name" SortExpression="FGItemName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DrawingNo" HeaderText="Drawing No" SortExpression="DrawingNo" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductionQty" HeaderText="Production Qty" SortExpression="ProductionQty" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkPrint" Text="Print" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Planning/Reports/PlanningReport.aspx?REPORTTYPE=JobSheetPrint&JobId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkStatus" Text="Status" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Production/Transaction/JobSheetStatus.aspx?JobId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                     <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkView" Text="View" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Production/Transaction/JobSheetView.aspx?JobId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#CBDBEA" />
                                <FooterStyle BackColor="#CBDBEA" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <PagerStyle BackColor="#CBDBEA" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#CBDBEA" Font-Bold="False" ForeColor="Black" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" Font-Bold="false" ForeColor="Black" />
                                <RowStyle BackColor="White" ForeColor="Black" Font-Bold="false" />
                            </asp:GridView>
                        </div>

                        <div class="row">

                            <asp:GridView ID="gvCompleted" class="mb-0 table table-hover table-bordered" runat="server" ShowFooter="false" Width="1000px"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="JobSheetNo" HeaderText="Job Sheet No" SortExpression="JobSheetNo" ItemStyle-Width="150">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>
                                    <asp:BoundField DataField="RequiredDate" HeaderText="Required Date" SortExpression="RequiredDate" ItemStyle-Width="100">
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemCode" HeaderText="FG Item Code" SortExpression="FGItemCode" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="FGItemName" HeaderText="FG Item Name" SortExpression="FGItemName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DrawingNo" HeaderText="Drawing No" SortExpression="DrawingNo" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductionQty" HeaderText="Production Qty" SortExpression="ProductionQty" ItemStyle-Width="120">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkPrint" Text="Print" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Planning/Reports/PlanningReport.aspx?REPORTTYPE=JobSheetPrint&JobId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                     <asp:TemplateField ShowHeader="true" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplinkView" Text="View" runat="server" Target="_blank" CssClass="gridLink" NavigateUrl='<%# "../Production/Transaction/JobSheetView.aspx?JobId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="centerText" />
                                        <ItemStyle VerticalAlign="Top" Width="50px" CssClass="centerText" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#CBDBEA" />
                                <FooterStyle BackColor="#CBDBEA" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <PagerStyle BackColor="#CBDBEA" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#CBDBEA" Font-Bold="False" ForeColor="Black" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" Font-Bold="false" ForeColor="Black" />
                                <RowStyle BackColor="White" ForeColor="Black" Font-Bold="false" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="app-wrapper-footer">
                    <div class="app-footer">
                    </div>
                </div>
            </div>
            <script src="http://maps.google.com/maps/api/js?sensor=true"></script>
        </div>
        <script type="text/javascript" src="assets/scripts/main.js"></script>
    </form>
</body>
</html>
