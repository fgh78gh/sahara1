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
using System.Text;

public class DBOperation
{
    private SqlConnection SQLConn;
    private SqlCommand SQLCommand;
    private SqlDataAdapter SQLDataAdap;

    public DBOperation()
    {

    }
    public string GiveField1(string qry)
    {

        string str;
        DataSet ds = new DataSet();
        GetDataSet(qry, "ds", ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            str = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            str = "";
        }
        return str;
    }

    public string GiveFieldQty1(string qry)
    {
        string str = "0.00";
        try
        {
            DataSet ds = new DataSet();
            GetDataSet(qry, "ds", ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception)
        {
        }
        return str;
    }
    public void ExecuteNonQuery(string strSQLCmd)
    {
        //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
        //rkey.SetValue("sShortDate", "dd/MM/yyyy");
        //rkey.SetValue("sLongDate", "dd/MM/yyyy");

        SQLConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString);
        try
        {
            SQLConn.Open();
            SQLCommand = new SqlCommand(strSQLCmd, SQLConn);
            SQLCommand.CommandTimeout = 0;
            SQLCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            throw ex;
        }
        finally
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
        }
    }

    public bool GetDataSet(string strSQLString, string strTableName, ref DataSet dsResults)
    {
        ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        SQLConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString);
        bool functionReturnValue = false;
        try
        {
            SQLConn.Open();
            SQLCommand = new SqlCommand(strSQLString, SQLConn);
            SQLCommand.CommandTimeout = 0;
            SQLDataAdap = new SqlDataAdapter(SQLCommand);
            SQLDataAdap.Fill(dsResults, strTableName);
            functionReturnValue = true;
        }
        catch (Exception ex)
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
            throw ex;
        }
        finally
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
        }
        return functionReturnValue;
    }

    public bool GetDataTable(string strSQLString, ref DataTable dtable)
    {
        ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        SQLConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["strSQLConnection"].ConnectionString);
        bool functionReturnValue = false;

        try
        {
            SQLConn.Open();
            SQLCommand = new SqlCommand(strSQLString, SQLConn);
            SQLDataAdap = new SqlDataAdapter(SQLCommand);
            SQLDataAdap.Fill(dtable);
            functionReturnValue = true;
        }
        catch (Exception ex)
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
            throw ex;
        }
        finally
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
        }
        return functionReturnValue;
    }

    public string NxtKeyCode(string KeyCode)
    {
        byte[] ASCIIValues = ASCIIEncoding.ASCII.GetBytes(KeyCode);
        int StringLength = ASCIIValues.Length;
        bool isAllZed = true;
        bool isAllNine = true;
        //Check if all has ZZZ.... then do nothing just return empty string.

        for (int i = 0; i < StringLength - 1; i++)
        {
            if (ASCIIValues[i] != 90)
            {
                isAllZed = false;
                break;
            }
        }
        if (isAllZed && ASCIIValues[StringLength - 1] == 57)
        {
            ASCIIValues[StringLength - 1] = 64;
        }

        // Check if all has 999... then make it A0
        for (int i = 0; i < StringLength; i++)
        {
            if (ASCIIValues[i] != 57)
            {
                isAllNine = false;
                break;
            }
        }
        if (isAllNine)
        {
            ASCIIValues[StringLength - 1] = 47;
            ASCIIValues[0] = 65;
            for (int i = 1; i < StringLength - 1; i++)
            {
                ASCIIValues[i] = 48;
            }
        }


        for (int i = StringLength; i > 0; i--)
        {
            if (i - StringLength == 0)
            {
                ASCIIValues[i - 1] += 1;
            }
            if (ASCIIValues[i - 1] == 58)
            {
                ASCIIValues[i - 1] = 48;
                if (i - 2 == -1)
                {
                    break;
                }
                ASCIIValues[i - 2] += 1;
            }
            else if (ASCIIValues[i - 1] == 91)
            {
                ASCIIValues[i - 1] = 65;
                if (i - 2 == -1)
                {
                    break;
                }
                ASCIIValues[i - 2] += 1;

            }
            else
            {
                break;
            }

        }
        KeyCode = ASCIIEncoding.ASCII.GetString(ASCIIValues);
        return KeyCode;
    }

    public Double StockMinus(Double dbDelqty, string strItemId, SqlConnection db, SqlTransaction transaction, String strFor, String TransNo, String strinvid, String InvoiceItemId)
    {
        Double dbAMountValue = 0.00;
        if (dbDelqty > 0)
        {
            DataSet dsStockdata = new DataSet();
            String StrSelStokItem = "select id,saleqty,basicprice from tblstocklist where itemcode='" + strItemId + "' and saleqty>0 order by id";
            Double dbadstock = dbDelqty;
            GetDataSet(StrSelStokItem, "data", ref dsStockdata);

            for (int j = 0; j < dsStockdata.Tables[0].Rows.Count; j++)
            {
                Double dbstkqty = Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["saleqty"].ToString());
                if (dbadstock > 0)
                {
                    if (dbstkqty < dbadstock)
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbstkqty + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();

                        new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbstkqty + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();


                        dbAMountValue = dbAMountValue + (dbstkqty * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));

                        dbadstock = dbadstock - dbstkqty;
                    }
                    else
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbadstock + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();
                        new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbadstock + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();
                        dbAMountValue = dbAMountValue + (dbadstock * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));
                        dbadstock = 0.00;
                    }
                }
            }
            Double dbRate = Convert.ToDouble(dsStockdata.Tables[0].Rows[0]["basicprice"].ToString());

            String dbnewbalqty = GiveFieldQty1("SELECT sum(saleqty) FROM tblStockList where saleqty>0 and itemcode='" + strItemId + "' ");
            String dbnewbalVal = GiveFieldQty1("SELECT sum(saleqty*BasicPrice) FROM tblStockList where saleqty>0 and itemcode='" + strItemId + "' ");

            string strInsTransQry = "";
            strInsTransQry = "INSERT INTO tbltransaction(itemid,DocNo,rate,grnqty,grnvalue,saleqty,salevalue,balqty,balvalue,purchaseprice,ReciptDetails) values (";
            strInsTransQry = strInsTransQry + "'" + strItemId + "','" + TransNo + "','" + dbRate + "','0.00','0.00','" + dbDelqty + "','" + dbRate * dbDelqty + "','" + onlyDecimal(dbnewbalqty) + "','" + onlyDecimal(dbnewbalVal) + "','" + dbRate + "','" + strFor + "')";
            new SqlCommand(strInsTransQry, db, transaction).ExecuteNonQuery();

        }
        return dbAMountValue;
    }

    public void StockMinusINVEdit(Double dbDelqty, string strItemId, SqlConnection db, SqlTransaction transaction, String strFor, String TransNo, String strinvid, String BranchCode, String InvoiceItemId)
    {
        Double dbAMountValue = 0.00;
        if (dbDelqty > 0)
        {

            DataSet dsStockdata = new DataSet();
            String StrSelStokItem = "select id,saleqty,basicprice from tblstocklist where itemcode='" + strItemId + "' and saleqty>0  and BranchCode='" + BranchCode + "' order by id";
            Double dbadstock = dbDelqty;
            GetDataSet(StrSelStokItem, "data", ref dsStockdata);

            for (int j = 0; j < dsStockdata.Tables[0].Rows.Count; j++)
            {
                Double dbstkqty = Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["saleqty"].ToString());
                if (dbadstock > 0)
                {
                    if (dbstkqty < dbadstock)
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbstkqty + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();

                        if (strFor.ToLower() == "Invoice".ToLower())
                        {
                            new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbstkqty + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();
                        }

                        dbAMountValue = dbAMountValue + (dbstkqty * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));

                        dbadstock = dbadstock - dbstkqty;
                    }
                    else
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbadstock + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();

                        if (strFor.ToLower() == "Invoice".ToLower())
                        {
                            new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbadstock + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();
                        }

                        dbAMountValue = dbAMountValue + (dbadstock * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));
                        dbadstock = 0.00;
                    }
                }
            }

            Double balqty = 0.00; Double Strbalqty = 0.00;
            int transid = 0;

            DataSet dstoptransection = new DataSet();

            GetDataSet("Select top 1 id,(balqty + saleqty)balqty from tbltransaction where itemid= '" + strItemId + "' and DocNo='" + TransNo + "' and BranchCode='" + BranchCode + "' order by id desc ", "data", ref dstoptransection);
            if (dstoptransection.Tables[0].Rows.Count > 0)
            {
                transid = Convert.ToInt32(dstoptransection.Tables[0].Rows[0]["id"].ToString());
                balqty = Convert.ToDouble(dstoptransection.Tables[0].Rows[0]["balqty"].ToString());
            }

            balqty = balqty - dbDelqty;

            new SqlCommand("Update tbltransaction Set saleqty='" + dbDelqty + "',balqty='" + balqty + "', balvalue=purchaseprice*'" + balqty + "' where id='" + transid + "'", db, transaction).ExecuteNonQuery();

            string strSelTrans = "select top 1 id,rate,balqty,balvalue,purchaseprice,saleqty from tbltransaction where itemid='" + strItemId + "' and DocNo='" + TransNo + "' order by id desc ";
            DataSet dstransection = new DataSet();
            GetDataSet("Select * from tbltransaction where itemid= '" + strItemId + "' and id > '" + transid + "' and BranchCode='" + BranchCode + "' order by id desc ", "data", ref dstransection);
            for (int j = 0; j < dstransection.Tables[0].Rows.Count; j++)
            {
                if (Convert.ToDouble(dstransection.Tables[0].Rows[j]["grnqty"].ToString()) == 0.00)
                {
                    Double StrSaleqty = Convert.ToDouble(dstransection.Tables[0].Rows[j]["saleqty"].ToString());
                    Double StrPurchsePrice = Convert.ToDouble(dstransection.Tables[0].Rows[j]["purchaseprice"].ToString());

                    Strbalqty = balqty - StrSaleqty;
                    Double Strbalvalue = Strbalqty * StrPurchsePrice;
                    new SqlCommand("Update tbltransaction Set balqty='" + Strbalqty + "', balvalue='" + Strbalvalue + "' where id='" + dstransection.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();
                }
                else
                {
                    Double StrGrnqty = Convert.ToDouble(dstransection.Tables[0].Rows[j]["grnqty"].ToString());
                    Double StrPurchsePrice = Convert.ToDouble(dstransection.Tables[0].Rows[j]["purchaseprice"].ToString());

                    Strbalqty = balqty + StrGrnqty;
                    Double Strbalvalue = Strbalqty * StrPurchsePrice;
                    new SqlCommand("Update tbltransaction Set balqty='" + Strbalqty + "', balvalue='" + Strbalvalue + "' where id='" + dstransection.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();
                }
            }
        }
    }


    public Double onlyDecimal(String strval)
    {
        Double val = 0.00;
        if (strval != null)
        {
            if (strval != "")
            {
                val = Convert.ToDouble(strval);
            }
        }
        return val;
    }
    public String SubMenuQry2(String strUserType, String strUsername)
    {
        String strQry = "";
        if (strUserType.ToUpper() == "Super Admin".ToUpper() || strUserType.ToUpper() == "SuperAdmin".ToUpper())
        {
            strQry = "SELECT id,MenuType,Menu FROM tblMenu Order by id";
        }
        else
        {
            strQry = "SELECT id,MenuType,Menu FROM tblUserMainMenu where UserCode='" + strUsername + "' order by MENUNO";
        }

        return strQry;

    }

    public String SubMenuQry1(String strUserType, String strUsername)
    {
        String strQry = "";

        if (strUserType.ToUpper() == "Super Admin".ToUpper() || strUserType.ToUpper() == "SuperAdmin".ToUpper())
        {
            strQry = " SELECT id,Menuid,SubMenu,Path,(select count(PONO) from tblPurchaseOrder where deleted=0 and authorize='' and Cancel=0 and Status='Send for Approval')POAUTH, "
                     + " (Select count(ItemCode) FROM tblItemMaster where deleted=0 and status=0)ItemApprovel, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=1 and PurchaseAccepted=1 and ProductionAccepted=1 and Quality=1 and Accounts=1 and MDApproved=0)MDContractReview, "
                     + " (Select count(MasterCardNo) FROM tblmastercard where Active=1 and deleted=0 and AuthRise=0)MCApprovel, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=0)DesignContranctReview, "
                     + " (Select count(SampleCartNo) FROM tblSampleChart where deleted=0 and AuthRise=0 )SampleCartAuth, "
                     + " (Select count(FeasibilityNo) FROM tblFeasibilityEntry where deleted=0 and DesignRequired='YES' and DesignDone=0)DesignFeasibility, "
                     + " (Select count(SONo) FROM tblSalesOrder where deleted=0 and authorize=0 )SOAuth, "
                     + " (Select count(SONO) FROM tblContranctReview where deleted=0 and SalesAccepted=0 )SalesContranctReview, "
                     + " (Select count(QuotationNo) FROM tblQuotation where deleted=0 and AuthRise=0 )QtnAuth, "
                     + " (Select count(SONO) FROM tblContranctReview where deleted=0 and SalesAccepted=1 and Accounts=0 )AccountContranctReview, "
                     + " (Select count(FeasibilityNo) FROM tblFeasibilityEntry where deleted=0 and PurchaseRequired='YES' and PurchaseDone=0 )PurchaseFeasibility, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=1 and PurchaseAccepted=0)PurchaseContranctReview, "
                     + " (Select count(FeasibilityNo) FROM tblFeasibilityEntry where deleted=0 and QualityRequired='YES' and QualityDone=0)QualityFeasibility, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and Quality=0)QualityContranctReview, "
                     + " (Select count(JobSheetNo) FROM tblJobSheet where deleted=0 and AuthRise=0)JobSheetAuth, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=1 and PurchaseAccepted=1 and ProductionAccepted=0)ProductionContranctReview, "
                     + " (Select count(RequestNo) FROM tblPurchaseRequest where deleted=0 and authorize=0 and Cancel=0 and Status='Send for Approval')PRAuth, "
                     + " (Select count(SONO) FROM tblContranctReview where deleted=0 and SalesAccepted=1 and PurchaseAccepted=1 and DesignAccepted=1 and ProductionAccepted=1 and "
                     + " sono not in (Select tbljobSheet.SoNo from tbljobSheet where tbljobSheet.deleted=0) and SONO not in (select tblInvoiceItem.SONO from tblInvoiceItem where tblInvoiceItem.SONO=tblContranctReview.SONO))PendingJobContract, "
                     + " (Select count(id) from tblStockAdjustment where Authorize='')StockAdjestment, "
                     + "(Select Count(Id) from tblOpeningBalance where Status='')OpeningAuth, "
                     + "(Select Count(Id) from tblinvoice where deleted=0 and cancel=0 and dpdone=0)DispatchPlan, "
                     + "(select Count(Id) from tblMaterialRequest where deleted=0 and id in (select requestid from tblMaterialRequestItem where (Qty-IssueQty-SCQty)>0))MaterialIssuense"
                    
                     +" FROM tblMenuList  order by id";
        }
        else
        {
            strQry = " SELECT id,Menuid,SubMenu,Path,(select count(PONO) from tblPurchaseOrder where deleted=0 and authorize='' and Cancel=0 and Status='Send for Approval')POAUTH, "
                     + " (Select count(ItemCode) FROM tblItemMaster where deleted=0 and status=0)ItemApprovel, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=1 and PurchaseAccepted=1 and ProductionAccepted=1 and Quality=1 and Accounts=1 and MDApproved=0)MDContractReview, "
                     + " (Select count(MasterCardNo) FROM tblmastercard where Active=1 and deleted=0 and AuthRise=0)MCApprovel, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=0)DesignContranctReview, "
                     + " (Select count(SampleCartNo) FROM tblSampleChart where deleted=0 and AuthRise=0 )SampleCartAuth, "
                     + " (Select count(FeasibilityNo) FROM tblFeasibilityEntry where deleted=0 and DesignRequired='YES' and DesignDone=0)DesignFeasibility, "
                     + " (Select count(SONo) FROM tblSalesOrder where deleted=0 and authorize=0 )SOAuth, "
                     + " (Select count(SONO) FROM tblContranctReview where deleted=0 and SalesAccepted=0 )SalesContranctReview, "
                     + " (Select count(QuotationNo) FROM tblQuotation where deleted=0 and AuthRise=0 )QtnAuth, "
                     + " (Select count(SONO) FROM tblContranctReview where deleted=0 and SalesAccepted=1 and Accounts=0 )AccountContranctReview, "
                     + " (Select count(FeasibilityNo) FROM tblFeasibilityEntry where deleted=0 and PurchaseRequired='YES' and PurchaseDone=0 )PurchaseFeasibility, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=1 and PurchaseAccepted=0)PurchaseContranctReview, "
                     + " (Select count(FeasibilityNo) FROM tblFeasibilityEntry where deleted=0 and QualityRequired='YES' and QualityDone=0)QualityFeasibility, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and Quality=0)QualityContranctReview, "
                     + " (Select count(JobSheetNo) FROM tblJobSheet where deleted=0 and AuthRise=0)JobSheetAuth, "
                     + " (Select count(SONO) FROM tblContranctReview where  deleted=0 and SalesAccepted=1 and DesignAccepted=1 and PurchaseAccepted=1 and ProductionAccepted=0)ProductionContranctReview, "
                     + " (Select count(RequestNo) FROM tblPurchaseRequest where deleted=0 and authorize=0 and Cancel=0 and Status='Send for Approval')PRAuth, "
                     + "(Select count(SONO) FROM tblContranctReview where deleted=0 and SalesAccepted=1 and PurchaseAccepted=1 and DesignAccepted=1 and ProductionAccepted=1 and "
                     + " sono not in (Select tbljobSheet.SoNo from tbljobSheet where tbljobSheet.deleted=0) and SONO not in (select tblInvoiceItem.SONO from tblInvoiceItem where tblInvoiceItem.SONO=tblContranctReview.SONO))PendingJobContract, "
                     + "(Select count(id) from tblStockAdjustment where Authorize='')StockAdjestment, "
                     + "(Select Count(Id) from tblOpeningBalance where Status='')OpeningAuth, "
                     + "(Select Count(Id) from tblinvoice where deleted=0 and cancel=0 and dpdone=0)DispatchPlan, "
                     + "(select Count(Id) from tblMaterialRequest where deleted=0 and id in (select requestid from tblMaterialRequestItem where (Qty-IssueQty-SCQty)>0))MaterialIssuense "
                     + " FROM tblUserMenuList where (New =1 or Edit =1 or Remove =1 or Inquiry=1) Order By id";
        }

        return strQry;
    }

    public String MenuName(String strMenu, DataRow DepartmentSubMenu)
    {
        if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Purchase Order Authorization".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["POAUTH"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Item Master Approval".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["ItemApprovel"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Contract Review Accept".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["MDContractReview"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Master Card Authroise".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["MCApprovel"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Design Contract Review".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["DesignContranctReview"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Sample Chart Authroise".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["SampleCartAuth"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Design Feasibility Update".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["DesignFeasibility"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Sales Order Authorisation".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["SOAuth"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Sales Contract Review".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["SalesContranctReview"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Quotation Authorization".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["QtnAuth"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Account Contract Review".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["AccountContranctReview"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Purchase Feasibility Update".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["PurchaseFeasibility"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Purchase Contract Review".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["PurchaseContranctReview"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Quality Feasibility Update".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["QualityFeasibility"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Quality Contract Review".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["QualityContranctReview"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Job Sheet Authroise".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["JobSheetAuth"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Production Contract Review".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["ProductionContranctReview"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Purchase Request Authorization".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["PRAuth"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Contract Review Jobsheet Pending".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["PendingJobContract"].ToString().ToUpper() + ")";
        }
         else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Stock Adjustment Approval".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["StockAdjestment"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Opening Balance Approval".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["OpeningAuth"].ToString().ToUpper() + ")";
        }
       
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Material Request to Issuense".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["MaterialIssuense"].ToString().ToUpper() + ")";
        }
        else if (DepartmentSubMenu["SubMenu"].ToString().ToUpper() == "Dispatch Plan".ToUpper())
        {
            strMenu = strMenu + " (" + DepartmentSubMenu["DispatchPlan"].ToString().ToUpper() + ")";
        }
        return strMenu;
    }
}