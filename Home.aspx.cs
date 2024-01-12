using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;


namespace SaharaLabel
{
    public partial class Home : System.Web.UI.Page
    {
        DBOperation dbop = new DBOperation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../Index.aspx");
            }

            DisplayName.InnerText = Session["CompanyName"].ToString();
            UserName.InnerText = Session["UserName"].ToString();
            UserDesignation.InnerText = "FY(" + Session["Years"].ToString() + ")"; UserImage.Src = Session["UserImage"].ToString();

            Session["strSQLConnection"] = System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString;
            Menu();
            lnkOpenJobSheet.Visible = false;
            spnOpenJobSheet.Visible = false;
            lnkOpenSapmleChart.Visible = false;
            spnOpenSapmleChart.Visible = false;
            lnkButtonTotadyPlan.Visible = false;
            spntodayPlan.Visible = false;
            lnkButtonTotadyCompleted.Visible = false;
            spntodayCompleted.Visible = false;

            row1.Visible = false;

            if (Session["UserDepartment"].ToString() == "PRODUCTION")
            {
                row1.Visible = true;
                lnkOpenJobSheet.Visible = true;
                spnOpenJobSheet.Visible = true;
                lnkOpenSapmleChart.Visible = true;
                spnOpenSapmleChart.Visible = true;
                lnkButtonTotadyPlan.Visible = true;
                spntodayPlan.Visible = true;
                lnkButtonTotadyCompleted.Visible = true;
                spntodayCompleted.Visible = true;

                String OpenJobList = dbop.GiveField1("select count(*) FROM tblJobSheet where deleted=0 and AuthRise=1 and JobSheetEnd=0 and Cancel=0 and id in (select jobsheetid from tblJobSheetProcessUpdate where jobsheetid=tblJobSheet.id and ProcessEndStatus=0 and ProcessStartStatus=0 and ProcessName='" + Session["STREMPName"].ToString() + "')");
                String OpenSampleCartList = dbop.GiveField1("select count(*) FROM tblSampleChart where deleted=0 and AuthRise=1 and id in (select SampleChartId from tblSampleChartProcessUpdate where SampleChartId=tblSampleChart.id and ProcessEndStatus=0 and ProcessStartStatus=0 and ProcessName='" + Session["STREMPName"].ToString() + "')");
                String TodauPlan = dbop.GiveField1("set dateformat dmy;select count(*) FROM tblJobSheet where deleted=0 and AuthRise=1 and id in (Select JobSheetId FROM tblJobSheetPlan where deleted=0 and SectionType='" + Session["STREMPName"].ToString() + "' and PlanDate='" + Session["DateNow"].ToString() + "')");
                String TodauCompleted = dbop.GiveField1("set dateformat dmy;select count(*) FROM tblJobSheet where deleted=0 and AuthRise=1 and id in (select jobsheetid from tblJobSheetProcessUpdate where jobsheetid=tblJobSheet.id and ProcessEndStatus=1 and ProcessStartStatus=1 and ProcessName='" + Session["STREMPName"].ToString() + "') and id in (Select JobSheetId FROM tblJobSheetPlan where deleted=0 and SectionType='" + Session["STREMPName"].ToString() + "' and PlanDate='" + Session["DateNow"].ToString() + "')");
                spnOpenJobSheet.InnerText = OpenJobList.ToString();
                spnOpenSapmleChart.InnerText = OpenSampleCartList.ToString();
                spntodayPlan.InnerText = TodauPlan.ToString();
                spntodayCompleted.InnerText = TodauCompleted.ToString();
            }
        }
        private void Menu()
        {
            StringBuilder objstr = new StringBuilder();
            objstr.Append("<ul class='vertical-nav-menu'>");
            DataTable dtSubMenu = this.GetData(dbop.SubMenuQry1(Session["strUserType"].ToString(), Session["LoginEMPName"].ToString()));
            DataTable dtDeptMenu1 = this.GetData(dbop.SubMenuQry2(Session["strUserType"].ToString(), Session["LoginEMPName"].ToString()));
            Session["dtDeptMenu1"] = dtDeptMenu1;
            DataTable dtDept = this.GetData("uds_SelMainDepartment '" + Session["strUserType"].ToString() + "','" + Session["UserCode"].ToString() + "'");
            foreach (DataRow Department in dtDept.Rows)
            {
                objstr.Append("<li><a href='#'><i class='app-sidebar__heading'></i>" + Department["Menu"].ToString() + "<i class='metismenu-state-icon pe-7s-angle-down caret-left'></i></a>");
                objstr.Append("<ul>");

                DataView dataView1 = dtDeptMenu1.DefaultView;
                if (!string.IsNullOrEmpty(Department["Menu"].ToString()))
                {
                    dataView1.RowFilter = "Menu = '" + Department["Menu"].ToString() + "'";
                }
                DataTable dtDeptMenu = dataView1.ToTable();

                foreach (DataRow DepartmentMenu in dtDeptMenu.Rows)
                {
                    DataView dataView = dtSubMenu.DefaultView;
                    if (!string.IsNullOrEmpty(DepartmentMenu["id"].ToString()))
                    {
                        dataView.RowFilter = "Menuid = '" + DepartmentMenu["id"].ToString() + "'";
                    }
                    DataTable dtDeptSubMenu = dataView.ToTable();

                    objstr.Append("<li><a href='#'><i class='metismenu-icon'></i>" + DepartmentMenu["MenuType"].ToString() + "<i class='metismenu-state-icon pe-7s-angle-down caret-left'></i></a>");

                    objstr.Append("<ul>");
                    //DataTable dtDeptSubMenu = this.GetData(dbop.SubMenuQry(Session["strUserType"].ToString(), DepartmentMenu["id"].ToString(), Session["LoginEMPName"].ToString()));

                    foreach (DataRow DepartmentSubMenu in dtDeptSubMenu.Rows)
                    {
                        String strMenu = dbop.MenuName(DepartmentSubMenu["SubMenu"].ToString(), DepartmentSubMenu);

                        String strPath = string.Format(DepartmentSubMenu["Path"].ToString());

                        objstr.Append("<li style='border-bottom-style: groove;border-width: thin;'><a href=../../" + strPath + " ><i class='metismenu-icon'></i>" + strMenu + "</a></li>");
                    }
                    objstr.Append("</ul>");
                    objstr.Append("</li>");
                }

                objstr.Append("</ul>");
                objstr.Append("</li>");

            }
            objstr.Append("</ul>");
            dvMenu.InnerHtml = objstr.ToString();
        }

        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        protected void lnkOpenJobSheet_Click(object sender, EventArgs e)
        {
            gvTodayPlan.Visible = false;
            gvSampleChartList.Visible = false;
            gvPendingJobList.Visible = true;
            gvCompleted.Visible = false;
            DataSet dsFetchData = new DataSet();
            dbop.GetDataSet("set dateformat dmy;SELECT id,JobSheetNo,CONVERT(VARCHAR(10),PlanDeliveryDate,103)RequiredDate,FGItemCode,FGItemName,DrawingNo,ProductionQty  FROM tblJobSheet where deleted=0 and AuthRise=1 and JobSheetEnd=0 and Cancel=0 and id in (select jobsheetid from tblJobSheetProcessUpdate where jobsheetid=tblJobSheet.id and ProcessEndStatus=0 and ProcessStartStatus=0 and ProcessName='" + Session["STREMPName"].ToString() + "') ", "POLIST", ref dsFetchData);
            DataTable dttable = dsFetchData.Tables["POLIST"];
            gvPendingJobList.DataSource = dttable;
            gvPendingJobList.DataBind();
        }
        protected void lnkOpenSapmleChart_Click(object sender, EventArgs e)
        {
            gvTodayPlan.Visible = false;
            gvSampleChartList.Visible = true;
            gvPendingJobList.Visible = false;
            gvCompleted.Visible = false;
            DataSet dsFetchData = new DataSet();
            dbop.GetDataSet("set dateformat dmy;SELECT id,SampleCartNo JobSheetNo,CONVERT(VARCHAR(10),SampleCartDate,103)RequiredDate,ItemCode FGItemCode,ItemName FGItemName,DrawingNo,ProductionQty  FROM tblSampleChart where deleted=0 and AuthRise=1 and id in (select SampleChartId from tblSampleChartProcessUpdate where SampleChartId=tblSampleChart.id and ProcessEndStatus=0 and ProcessStartStatus=0 and ProcessName='" + Session["STREMPName"].ToString() + "') ", "POLIST", ref dsFetchData);
            DataTable dttable = dsFetchData.Tables["POLIST"];
            gvSampleChartList.DataSource = dttable;
            gvSampleChartList.DataBind();
        }
        protected void lnkButtonTotadyPlan_Click(object sender, EventArgs e)
        {
            gvTodayPlan.Visible = true;
            gvSampleChartList.Visible = false;
            gvPendingJobList.Visible = false;
            gvCompleted.Visible = false;
            DataSet dsFetchData = new DataSet();
            dbop.GetDataSet("set dateformat dmy;SELECT id,JobSheetNo,CONVERT(VARCHAR(10),PlanDeliveryDate,103)RequiredDate,FGItemCode,FGItemName,DrawingNo,ProductionQty  FROM tblJobSheet where deleted=0 and AuthRise=1 and Cancel=0 and id in (Select JobSheetId FROM tblJobSheetPlan where deleted=0 and SectionType='" + Session["STREMPName"].ToString() + "' and PlanDate='" + Session["DateNow"].ToString() + "') ", "POLIST", ref dsFetchData);
            DataTable dttable = dsFetchData.Tables["POLIST"];
            gvTodayPlan.DataSource = dttable;
            gvTodayPlan.DataBind();
        }

        protected void lnkButtonTotadyCompleted_Click(object sender, EventArgs e)
        {
            gvTodayPlan.Visible = false;
            gvSampleChartList.Visible = false;
            gvPendingJobList.Visible = false;
            gvCompleted.Visible = true;
            DataSet dsFetchData = new DataSet();
            dbop.GetDataSet("set dateformat dmy;SELECT id,JobSheetNo,CONVERT(VARCHAR(10),PlanDeliveryDate,103)RequiredDate,FGItemCode,FGItemName,DrawingNo,ProductionQty  FROM tblJobSheet where deleted=0 and AuthRise=1 and Cancel=0 and id in (select jobsheetid from tblJobSheetProcessUpdate where jobsheetid=tblJobSheet.id and ProcessEndStatus=1 and ProcessStartStatus=1 and ProcessName='" + Session["STREMPName"].ToString() + "')  and id in (Select JobSheetId FROM tblJobSheetPlan where deleted=0 and SectionType='" + Session["STREMPName"].ToString() + "' and PlanDate='" + Session["DateNow"].ToString() + "') ", "POLIST", ref dsFetchData);
            DataTable dttable = dsFetchData.Tables["POLIST"];
            gvCompleted.DataSource = dttable;
            gvCompleted.DataBind();
        }
        protected void btnLogout_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

    }
}