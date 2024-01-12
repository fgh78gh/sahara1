using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SaharaLabel
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        SqlConnection db;
        SqlTransaction transaction;
        DBOperation dbop = new DBOperation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["strSQLConnection"] = System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                Response.Write("<script>alert('Please Enter the Correct User Name.')</script>");
                txtUserName.Focus();
            }
            else if (txtOldPassword.Text == "")
            {
                Response.Write("<script>alert('Please Enter the Old Password.')</script>");
                txtOldPassword.Focus();
            }
            else if (txtNewPassword.Text == "")
            {
                Response.Write("<script>alert('Please Enter New Password.')</script>");
                txtNewPassword.Focus();
            }
            else if (txtConfirmPassword.Text == "")
            {
                Response.Write("<script>alert('Please Enter Confirm Password.')</script>");
                txtConfirmPassword.Focus();
            }
            else if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                Response.Write("<script>alert('New Password and Confirm Password Not Matching.')</script>");
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtNewPassword.Focus();
            }
            else
            {
                DataSet dsLogin = new DataSet();
                dbop.GetDataSet("uds_UserLogin '" + txtUserName.Text.Replace("'", "''") + "','" + txtOldPassword.Text.Replace("'", "''") + "'", "Login", ref dsLogin);

                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    if (dsLogin.Tables[0].Rows[0]["UserName"].ToString().ToLower() == txtUserName.Text.ToLower())
                    {
                        if (dsLogin.Tables[0].Rows[0]["password"].ToString() == txtOldPassword.Text)
                        {

                            SqlConnection db;
                            SqlTransaction transaction;
                            db = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString);

                            db.Open();
                            transaction = db.BeginTransaction();
                            try
                            {
                                new SqlCommand("set dateformat dmy;Update tbllogin set password='" + txtNewPassword.Text + "' where deleted=0 and username='" + txtUserName.Text + "'", db, transaction).ExecuteNonQuery();
                                //  dbop.ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                                Response.Write("<script>alert('Password Updated Successfully.')</script>");
                               // Response.Redirect("Index.aspx", false);
                                transaction.Commit();
                                db.Close();
                                txtUserName.Text = "";
                                txtOldPassword.Text = "";
                                txtNewPassword.Text = "";
                                txtConfirmPassword.Text = "";
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
                            txtOldPassword.Text = "";
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

    }
}