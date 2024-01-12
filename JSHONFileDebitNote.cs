
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace SaharaLabel
{
    public class JSHONFileDebitNote
    {
        DBOperation dbop = new DBOperation();

        public class IRNReturn
        {
            public long AckNo { get; set; }
            public string AckDt { get; set; }
            public string Irn { get; set; }
            public string SignedInvoice { get; set; }
            public string SignedQRCode { get; set; }
            public string Status { get; set; }
            public long EwbNo { get; set; }
            public string EwbDt { get; set; }
            public string EwbValidTill { get; set; }
            public string Remarks { get; set; }
        }

        public class TranDtls
        {
            public string TaxSch { get; set; }
            public string SupTyp { get; set; }
            public string RegRev { get; set; }
            public object EcmGstin { get; set; }
            public string IgstOnIntra { get; set; }
        }
        public class DocDtls
        {
            public string Typ { get; set; }
            public string No { get; set; }
            public string Dt { get; set; }
        }
        public class BuyerDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string TrdNm { get; set; }
            public string Pos { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public int Pin { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            public string Em { get; set; }
        }
        public class SellerDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string TrdNm { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public int Pin { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            public string Em { get; set; }
        }
        public class DispDtls
        {
            public string Nm { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public int Pin { get; set; }
            public string Stcd { get; set; }
        }
        public class ShipDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string TrdNm { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public int Pin { get; set; }
            public string Stcd { get; set; }
        }
        public class EwbDtls
        {
            public string TransId { get; set; }
            public string TransName { get; set; }
            public string TransMode { get; set; }
            public int Distance { get; set; }
            public string TransDocNo { get; set; }
            public string TransDocDt { get; set; }
            public string VehNo { get; set; }
            public string VehType { get; set; }
        }
        public class ExpDtls
        {
            public string ShipBNo { get; set; }
            public string ShipBDt { get; set; }
            public string CntCode { get; set; }
            public string ForCur { get; set; }
            public string Port { get; set; }
            public string RefClm { get; set; }
            public int ExpDuty { get; set; }
        }
        public class AttribDtl
        {
            public string Nm { get; set; }
            public string Val { get; set; }
        }
        public class BchDtls
        {
            public string Nm { get; set; }
            public string ExpDt { get; set; }
            public string WrDt { get; set; }
        }
        public class ItemList
        {
            public List<AttribDtl> AttribDtls { get; set; }
            public string PrdSlNo { get; set; }
            public object OrgCntry { get; set; }
            public object OrdLineRef { get; set; }
            public double TotItemVal { get; set; }
            public int OthChrg { get; set; }
            public int StateCesNonAdvlAmt { get; set; }
            public int StateCesAmt { get; set; }
            public int StateCesRt { get; set; }
            public int CesNonAdvlAmt { get; set; }
            public int CesAmt { get; set; }
            public int CesRt { get; set; }
            public double SgstAmt { get; set; }
            public double CgstAmt { get; set; }
            public double IgstAmt { get; set; }
            public double Qty { get; set; }
            public double AssAmt { get; set; }
            public int PreTaxVal { get; set; }
            public double Discount { get; set; }
            public double TotAmt { get; set; }
            public double UnitPrice { get; set; }
            public string Unit { get; set; }
            public double FreeQty { get; set; }
            public double GstRt { get; set; }
            public string Barcde { get; set; }
            public BchDtls BchDtls { get; set; }
            public string HsnCd { get; set; }
            public string IsServc { get; set; }
            public string PrdDesc { get; set; }
            public string SlNo { get; set; }
        }
        public class ValDtls
        {
            public double AssVal { get; set; }
            public double CgstVal { get; set; }
            public double SgstVal { get; set; }
            public double IgstVal { get; set; }
            public int CesVal { get; set; }
            public int StCesVal { get; set; }
            public double RndOffAmt { get; set; }
            public double TotInvVal { get; set; }
            public int TotInvValFc { get; set; }
            public int Discount { get; set; }
            public double OthChrg { get; set; }
        }
        public class PayDtls
        {
            public string Nm { get; set; }
            public string AccDet { get; set; }
            public string Mode { get; set; }
            public string FinInsBr { get; set; }
            public string CrTrn { get; set; }
            public string PayInstr { get; set; }
            public string PayTerm { get; set; }
            public string DirDr { get; set; }
            public int CrDay { get; set; }
            public int PaidAmt { get; set; }
            public int PaymtDue { get; set; }
        }
        public class PrecDocDtl
        {
            public string InvNo { get; set; }
            public string InvDt { get; set; }
            public string OthRefNo { get; set; }
        }
        public class ContrDtl
        {
            public string RecAdvDt { get; set; }
            public string RecAdvRefr { get; set; }
            public string TendRefr { get; set; }
            public string ContrRefr { get; set; }
            public string ExtRefr { get; set; }
            public string ProjRefr { get; set; }
            public string PORefr { get; set; }
            public string PORefDt { get; set; }
        }
        public class DocPerdDtls
        {
            public string InvStDt { get; set; }
            public string InvEndDt { get; set; }
        }
        public class RefDtls
        {
            public string InvRm { get; set; }
            public List<PrecDocDtl> PrecDocDtls { get; set; }
            public List<ContrDtl> ContrDtls { get; set; }
            public DocPerdDtls DocPerdDtls { get; set; }
        }
        public class AddlDocDtl
        {
            public string Url { get; set; }
            public string Docs { get; set; }
            public string Info { get; set; }
        }

        public class Root
        {
            public string Version { get; set; }
            public TranDtls TranDtls { get; set; }
            public DocDtls DocDtls { get; set; }
            public BuyerDtls BuyerDtls { get; set; }
            public SellerDtls SellerDtls { get; set; }
            public DispDtls DispDtls { get; set; }
            public ShipDtls ShipDtls { get; set; }
            public EwbDtls EwbDtls { get; set; }
            public ExpDtls ExpDtls { get; set; }
            public List<ItemList> ItemList { get; set; }
            public ValDtls ValDtls { get; set; }
            public PayDtls PayDtls { get; set; }
            public RefDtls RefDtls { get; set; }
            public List<AddlDocDtl> AddlDocDtls { get; set; }
        }

       
        public string VersionValue(String strInvoiceNo)
        {
            DataSet dsData = new DataSet();
            String StrQry = "select GSTNO CompanyGSTNo, CompanyName CompanyName,CompanyAddress1 CompanyAdd1, (CompanyAddress2 +' '+  CompanyAddress3)CompanyAdd2,"
                            + " (Select statename from TblStateMaster where id=stateid) CompanyLocation,PINCode CompanyPinCode, (Select stateCode from TblStateMaster where id=stateid) CompanyStateCode,"
                            + " Companyphoneno CompanyPhoneNo,Companyemail CompanyEmailId from Tblcompanymaster where deleted=0";
            dbop.GetDataSet(StrQry, "tblCompanyDetails", ref dsData);

            StrQry = "set dateformat dmy;select 'B2B' InvType ,DebitNoteNo INVNO,CONVERT(VARCHAR(10),DebitNoteDate,103) INVDT,GSTNO Buyergstno,name Buyername,StateCode BuyerPOS,"
                      + " Address1 BuyerAdd1, (Address2 +' '+  Address3) BuyerAdd2,StateName BuyerLocation,PinCode BuyerPinCode,StateCode BuyerStateCode,"
                      + " GSTNO ShipGSTNo,Name ShipName,Address1 ShipAddr1, (Address2 +' '+  Address3) ShipAddr2,StateName ShipLoc,PinCode ShipPinCode,StateCode ShipStateCode, "
                      + " TotalTaxableValue TotalTaxableValue,CGST,SGST,IGST,RoundOff,TCSValue OthChrg,InvoiceValue InvoiceValue,"
                      + " 'N' InvoiceFor from "
                      + " tblDebitNote  where DebitNoteNo='" + strInvoiceNo + "'";
            dbop.GetDataSet(StrQry, "tblInvoice", ref dsData);           

            Root root = new Root()
            {
                Version = "1.1",
                TranDtls = new TranDtls()
                {
                    TaxSch = "GST",
                    SupTyp = dsData.Tables["tblInvoice"].Rows[0]["InvType"].ToString(),
                    RegRev = "N",
                    IgstOnIntra = "N",
                },
                DocDtls = new DocDtls()
                {
                    Typ = "DBN",
                    No = dsData.Tables["tblInvoice"].Rows[0]["INVNO"].ToString(),
                    Dt = dsData.Tables["tblInvoice"].Rows[0]["INVDT"].ToString(),
                },

                BuyerDtls = new BuyerDtls()
                {
                    Gstin = dsData.Tables["tblInvoice"].Rows[0]["Buyergstno"].ToString(),
                    LglNm = dsData.Tables["tblInvoice"].Rows[0]["Buyername"].ToString(),
                    TrdNm = dsData.Tables["tblInvoice"].Rows[0]["Buyername"].ToString(),
                    Pos = dsData.Tables["tblInvoice"].Rows[0]["BuyerPOS"].ToString(),
                    Addr1 = dsData.Tables["tblInvoice"].Rows[0]["BuyerAdd1"].ToString(),
                    Addr2 = dsData.Tables["tblInvoice"].Rows[0]["BuyerAdd2"].ToString(),
                    Loc = dsData.Tables["tblInvoice"].Rows[0]["BuyerLocation"].ToString(),
                    Pin = Convert.ToInt32(dsData.Tables["tblInvoice"].Rows[0]["BuyerPinCode"].ToString()),
                    Stcd = dsData.Tables["tblInvoice"].Rows[0]["BuyerStateCode"].ToString(),
                },

                SellerDtls = new SellerDtls()
                {
                    Gstin = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyGSTNo"].ToString(),
                    LglNm = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyName"].ToString(),
                    TrdNm = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyName"].ToString(),
                    Addr1 = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyAdd1"].ToString(),
                    Addr2 = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyAdd2"].ToString(),
                    Loc = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyLocation"].ToString(),
                    Pin = Convert.ToInt32(dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyPinCode"].ToString()),
                    Stcd = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyStateCode"].ToString(),
                    Ph = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyPhoneNo"].ToString(),
                    Em = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyEmailId"].ToString(),
                },
                DispDtls = new DispDtls()
                {
                    Nm = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyName"].ToString(),
                    Addr1 = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyAdd1"].ToString(),
                    Addr2 = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyAdd2"].ToString(),
                    Loc = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyLocation"].ToString(),
                    Pin = Convert.ToInt32(dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyPinCode"].ToString()),
                    Stcd = dsData.Tables["tblCompanyDetails"].Rows[0]["CompanyStateCode"].ToString(),
                },


                ShipDtls = new ShipDtls()
                {
                    Gstin = dsData.Tables["tblInvoice"].Rows[0]["ShipGSTNo"].ToString(),
                    LglNm = dsData.Tables["tblInvoice"].Rows[0]["ShipName"].ToString(),
                    TrdNm = dsData.Tables["tblInvoice"].Rows[0]["ShipName"].ToString(),
                    Addr1 = dsData.Tables["tblInvoice"].Rows[0]["ShipAddr1"].ToString(),
                    Addr2 = dsData.Tables["tblInvoice"].Rows[0]["ShipAddr2"].ToString(),
                    Loc = dsData.Tables["tblInvoice"].Rows[0]["ShipLoc"].ToString(),
                    Pin = Convert.ToInt32(dsData.Tables["tblInvoice"].Rows[0]["ShipPinCode"].ToString()),
                    Stcd = dsData.Tables["tblInvoice"].Rows[0]["ShipStateCode"].ToString(),
                },


                ItemList = ItemListDataA(dsData.Tables["tblInvoice"].Rows[0]["INVNO"].ToString(), dsData.Tables["tblInvoice"].Rows[0]["InvoiceFor"].ToString()),

                ValDtls = new ValDtls()
                {
                    AssVal = Convert.ToDouble(dsData.Tables["tblInvoice"].Rows[0]["TotalTaxableValue"].ToString()),
                    CgstVal = Convert.ToDouble(dsData.Tables["tblInvoice"].Rows[0]["CGST"].ToString()),
                    SgstVal = Convert.ToDouble(dsData.Tables["tblInvoice"].Rows[0]["SGST"].ToString()),
                    IgstVal = Convert.ToDouble(dsData.Tables["tblInvoice"].Rows[0]["IGST"].ToString()),
                    CesVal = 0,
                    StCesVal = 0,
                    RndOffAmt = Convert.ToDouble(dsData.Tables["tblInvoice"].Rows[0]["RoundOff"].ToString()),
                    TotInvVal = Convert.ToDouble(dsData.Tables["tblInvoice"].Rows[0]["InvoiceValue"].ToString()),
                    TotInvValFc = 0,
                    Discount = 0,
                    OthChrg = Convert.ToDouble(dsData.Tables["tblInvoice"].Rows[0]["OthChrg"].ToString()),
                },
            };
            string objjsonData = JsonConvert.SerializeObject(root);
            return objjsonData;
        }


        public string ReturnIRN(String strData)
        {
            var IIRNReturn = JsonConvert.DeserializeObject<IRNReturn>(strData);
            return IIRNReturn.Irn;
        }

       

        public List<ItemList> ItemListDataA(String InvoiceNo,String InvoiceFor)
        {
            DataSet dsData = new DataSet();
            String StrQry = "select ROW_NUMBER()over(order by ItemCode) PrdSlNo, TotalValue TotItemVal,SGSTValue SgstAmt,CGSTValue CgstAmt,IGSTValue IgstAmt,Delqty Qty,TaxableValue TaxableValue,TaxableValue TotalTaxableValue,UnitPrice UnitPrice,UOM Unit,"
                    + " (CGSTPercent+SGSTPercent+IGSTPercent) GSTPercent,HSNNo HSNCode,ItemName ProductDescription "
                    + " from tblDebitNoteItem where DebitNoteid in (Select tblDebitNote.id from tblDebitNote where DebitNoteNo='" + InvoiceNo + "')";
            dbop.GetDataSet(StrQry, "tblInvoiceItem", ref dsData);

            List<ItemList> details = new List<ItemList>();
            ItemList user = new ItemList();
            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
            {
                details.Add(new ItemList
                {
                    PrdSlNo = dsData.Tables["tblInvoiceItem"].Rows[i]["PrdSlNo"].ToString(),
                    TotItemVal = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["TotItemVal"].ToString()),
                    OthChrg = 0,
                    StateCesNonAdvlAmt = 0,
                    StateCesAmt = 0,
                    StateCesRt = 0,
                    CesNonAdvlAmt = 0,
                    CesAmt = 0,
                    CesRt = 0,
                    SgstAmt = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["SgstAmt"].ToString()),
                    CgstAmt = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["CgstAmt"].ToString()),
                    IgstAmt = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["IgstAmt"].ToString()),
                    Qty = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["Qty"].ToString()),
                    AssAmt = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["TaxableValue"].ToString()),
                    PreTaxVal = 0,
                    Discount = 0,
                    TotAmt = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["TotalTaxableValue"].ToString()),
                    UnitPrice = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["UnitPrice"].ToString()),
                    Unit = dsData.Tables["tblInvoiceItem"].Rows[i]["Unit"].ToString(),
                    FreeQty = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["Qty"].ToString()),
                    GstRt = Convert.ToDouble(dsData.Tables["tblInvoiceItem"].Rows[i]["GSTPercent"].ToString()),
                    HsnCd = dsData.Tables["tblInvoiceItem"].Rows[i]["HSNCode"].ToString(),
                    IsServc = InvoiceFor,
                    PrdDesc = dsData.Tables["tblInvoiceItem"].Rows[i]["ProductDescription"].ToString(),
                    SlNo = (i + 1).ToString(),

                });
            }

            return details;
        }

    }
}


