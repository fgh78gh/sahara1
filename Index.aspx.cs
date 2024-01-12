using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Net.Mime;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SaharaLabel
{
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection db;
        SqlTransaction transaction;
        DBOperation dbop = new DBOperation();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DatabaseBackup();
            }
            catch { }
            if (!IsPostBack)
            {
                Session["Years"] = "";
                Session["UserCode"] = "";
                Session["UserName"] = "";
                Session["strUserType"] = "";
                Session["DateNow"] = "";
                Session["CompanyCode"] = "";
                Session["UserImage"] = "";
                Session["CurrentYear"] = "YES";
                Session["strSQLConnection"] = System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString;
                fillyear();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
                txtUserName.Text = "sa";
                txtPassword.Text = "sa12345";
            
            if (CboYear.SelectedItem.Text.ToUpper() == "SELECT")
            {
                Response.Write("<script>alert('Please Select Correct FY Year.')</script>");
            }
            else
            {
                Session["CurrentYear"] = "YES";
                if (CboYear.SelectedIndex != 0)
                {
                    Session["CurrentYear"] = "NO";
                }

                Session["Years"] = CboYear.SelectedItem.Text;
                int ct = CboYear.SelectedIndex;
                try
                {
                    int num = 0;
                    ct = ct + 1;
                    foreach (ListItem li in CboYear.Items)
                    {
                        if (ct == num)
                        {
                            Session["PYear"] = li.Text;
                            break;
                        }
                        num++;
                    }
                }
                catch
                {

                }
                DataSet dsLogin = new DataSet();

                dbop.GetDataSet("uds_UserLogin '" + txtUserName.Text.Replace("'", "''") + "','" + txtPassword.Text.Replace("'", "''") + "'", "Login", ref dsLogin);
              
                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    String ecname1 = "";
                    try
                    {
                        string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
                        ecname1 = computer_name[0].ToString();
                    }
                    catch { }

                    if (ecname1 == "")
                    {
                        try
                        {
                            string[] computer_name1 = System.Net.Dns.GetHostEntry(Request.ServerVariables["LocalIPAddress"]).HostName.Split(new Char[] { '.' });
                            ecname1 = computer_name1[0].ToString();
                        }
                        catch { }
                    }

                    Session["HostName"] = ecname1;
                    if (dsLogin.Tables[0].Rows[0]["UserName"].ToString().ToLower() == txtUserName.Text.ToLower())
                    {
                        if (dsLogin.Tables[0].Rows[0]["password"].ToString() == txtPassword.Text)
                        {
                            try
                            {
                                dbop.ExecuteNonQuery("set dateformat dmy;update tblloggedIn set logouttime=getdate() where username='" + txtUserName.Text + "' and MachineName='" + Session["HostName"].ToString() + "' and logouttime is NULL");
                            }
                            catch { }

                            SqlConnection db;
                            SqlTransaction transaction;
                            db = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString);

                            db.Open();
                            transaction = db.BeginTransaction();
                            try
                            {
                                Session["UserCode"] = txtUserName.Text;
                                Session["LoginEMPCode"] = dsLogin.Tables[0].Rows[0]["Employeecode"].ToString();
                                Session["STREMPName"] = dsLogin.Tables[0].Rows[0]["EmployeeName"].ToString();
                                Session["LoginEMPName"] = dsLogin.Tables[0].Rows[0]["UserName"].ToString();
                                Session["UserName"] = dsLogin.Tables[0].Rows[0]["UserName"].ToString();
                                Session["strUserType"] = dsLogin.Tables[0].Rows[0]["UserType"].ToString();
                                Session["DateNow"] = dbop.GiveField1("select CONVERT(VARCHAR(24),GETDATE(),103)");
                                Session["UserDesignation"] = dsLogin.Tables[0].Rows[0]["Designation"].ToString();
                                Session["UserDepartment"] = dsLogin.Tables[0].Rows[0]["Department"].ToString();
                                String imgFile = dbop.GiveField1("select ImageFile from TblEmployeemaster where Deleted=0 and EmployeeCode='" + Session["LoginEMPCode"].ToString() + "'");
                                Session["UserImage"] = "../../Employee Document/" + Session["LoginEMPCode"].ToString() + "/" + imgFile;
                                DataSet dsData = new DataSet();
                                dbop.GetDataSet("uds_SelCompanyDetails", "data", ref dsData);
                                if (dsData.Tables[0].Rows.Count > 0)
                                {
                                    Session["Companyid"] = dsData.Tables[0].Rows[0]["id"].ToString();
                                    Session["CompanyCode"] = dsData.Tables[0].Rows[0]["CompanyCode"].ToString();
                                    Session["CompanyName"] = dsData.Tables[0].Rows[0]["CompanyName"].ToString();
                                    Session["Address"] = dsData.Tables[0].Rows[0]["Address"].ToString();
                                    Session["StateCode"] = dsData.Tables[0].Rows[0]["StateCode"].ToString();
                                }

                                dbop.ExecuteNonQuery("delete from tblOpenForm where CName='" + Session["HostName"].ToString() + "' and Username='" + Session["UserCode"].ToString() + "'");

                                if (dsLogin.Tables[0].Rows[0]["UserType"].ToString() != "Super Admin")
                                {
                                    new SqlCommand("set dateformat dmy;insert into tblloggedIn (username,MachineName) values ('" + txtUserName.Text + "','" + Session["HostName"].ToString() + "')", db, transaction).ExecuteNonQuery();
                                    dbop.ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                                }
                                Session["logedinid"] = dbop.GiveField1("select id from tblloggedIn where username='" + txtUserName.Text + "' and logouttime is NULL");

                                if (dbop.GiveField1("Set dateformat dmy;select * from tblEmailSend where EmailDate=convert(date,GETDATE())") == "")
                                {
                                    try
                                    {
                                        WebClient web = new WebClient();
                                        const string urlPattern = "https://api.exchangerate.host/convert?from=USD&to=INR";
                                        string url = String.Format(urlPattern);
                                        string response = new WebClient().DownloadString(url);
                                        var userObj = JObject.Parse(response);
                                        var Status = Convert.ToString(userObj["result"]);
                                        if (onlyDecimal(Status) > 0)
                                        {
                                            dbop.ExecuteNonQuery("update TblCurrencymaster set CurrencyValue='" + Status + "'where deleted=0 and Currencycode='USD'");
                                        }
                                        new SqlCommand("set dateformat dmy;insert into tblEmailSend (EmailDate,MacineName) values ('" + DateTime.Now.ToShortDateString() + "','" + Environment.MachineName + "')", db, transaction).ExecuteNonQuery();
                                        dbop.ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                                    }
                                    catch
                                    { }
                                }
                                
                                Response.Redirect("Home.aspx", false);
                                transaction.Commit();
                                db.Close();

                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                db.Close();
                            }


                        }
                        else
                        {
                            Response.Write("<script>alert('Please Enter the Correct Password.')</script>");
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtUserName.Focus();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Please Enter the Correct User Name.')</script>");
                        txtUserName.Text = "";
                        txtUserName.Focus();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please Enter the Correct User and Password.')</script>");
                    txtUserName.Focus();
                    txtUserName.Text = "";
                }
            }
        }
        protected void BtnChangePassward_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx", false);
        }
        private void fillyear()
        {
            DataSet dsFetchData = new DataSet();
            dbop.GetDataSet("uds_SelFYear", "FYear", ref dsFetchData);

            CboYear.DataSource = dsFetchData.Tables[0];
            CboYear.DataTextField = "FYear";
            CboYear.DataValueField = "FYear";
            CboYear.DataBind();
        }

        private void DatabaseBackup()
        {
            DataSet dsdata = new DataSet();
            dbop.GetDataSet("uds_databasebackup", "data", ref dsdata);
            if (dsdata.Tables[0].Rows.Count > 0)
            {
                if (dsdata.Tables[0].Rows[0]["BkpDate"].ToString() != dsdata.Tables[0].Rows[0]["CurrentDate"].ToString())
                {
                    SqlConnection ObjConn = new SqlConnection(Session["strSQLConnection"].ToString());
                    SqlCommand ObjCmd = new SqlCommand("Sp_DatabaseBackup", ObjConn);
                    ObjCmd.CommandTimeout = 0;
                    ObjConn.Open();
                    ObjCmd.ExecuteNonQuery();
                    ObjConn.Close();
                    ObjConn.Dispose();
                    dbop.ExecuteNonQuery("set dateformat dmy;insert into tblBackup values(getdate())");
                }
            }
            else
            {
                SqlConnection ObjConn = new SqlConnection(Session["strSQLConnection"].ToString());
                SqlCommand ObjCmd = new SqlCommand("Sp_DatabaseBackup", ObjConn);
                ObjCmd.CommandTimeout = 0;
                ObjConn.Open();
                ObjCmd.ExecuteNonQuery();
                ObjConn.Close();
                ObjConn.Dispose();
                dbop.ExecuteNonQuery("set dateformat dmy;insert into tblBackup values(getdate())");
            }
        }
        private static Double onlyDecimal(String strval)
        {
            Double val = 0.00;
            if (strval != null)
            {
                if (strval != "")
                {
                    try
                    {
                        val = Convert.ToDouble(strval.Replace(",", "").Trim());
                    }
                    catch { }
                }
            }
            return val;
        }
    }
}