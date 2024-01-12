
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
    public class JSHONFileEwaybill
    {
        DBOperation dbop = new DBOperation();

        public class IRNReturn
        {
            public long EwbNo { get; set; }
            public string EwbDt { get; set; }
            public string EwbValidTill { get; set; }
        }


        public class EwbDtls
        {
            public string Irn { get; set; }
            public int Distance { get; set; }
            public string TransMode { get; set; }
            public string TransId { get; set; }
            public string TransName { get; set; }
            public string TransDocDt { get; set; }
            public string TransDocNo { get; set; }
            public string VehNo { get; set; }
            public string VehType { get; set; }
        }


        public class Root
        {
            public EwbDtls EwbDtls { get; set; }
        }


        public string VersionValue(String strInvoiceNo)
        {
            DataSet dsData = new DataSet();
            String StrQry = "select IrnNo,Distance,DocumentNo,DocumentDate,VechileNo,Mode,TransporterId,TransporterName from tblInvoice where tblInvoice.deleted=0 and invoiceno='" + strInvoiceNo + "'";
            dbop.GetDataSet(StrQry, "tblInvoice", ref dsData);

            EwbDtls root = new EwbDtls()
            {               
                Irn = dsData.Tables["tblInvoice"].Rows[0]["IrnNo"].ToString(),
                Distance = Convert.ToInt32(dsData.Tables["tblInvoice"].Rows[0]["Distance"].ToString()),
                TransMode = dsData.Tables["tblInvoice"].Rows[0]["Mode"].ToString(),
                TransId = dsData.Tables["tblInvoice"].Rows[0]["TransporterId"].ToString(),
                TransName = dsData.Tables["tblInvoice"].Rows[0]["TransporterName"].ToString(),
                TransDocDt = dsData.Tables["tblInvoice"].Rows[0]["DocumentDate"].ToString(),
                TransDocNo = dsData.Tables["tblInvoice"].Rows[0]["DocumentNo"].ToString(),
                VehNo = dsData.Tables["tblInvoice"].Rows[0]["VechileNo"].ToString(),
                VehType = "R",
            };
            if (root.TransId == "")
                root.TransId = null;
            if (root.TransName == "")
                root.TransName = null;
            if (root.TransDocDt == "")
                root.TransDocDt = null;
            if (root.TransDocNo == "")
                root.TransDocNo = null;
            string objjsonData = JsonConvert.SerializeObject(root);
            return objjsonData;
        }



    }
}


