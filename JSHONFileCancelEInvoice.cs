
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
    public class JSHONFileCancelEInvoice
    {
        DBOperation dbop = new DBOperation();

        public class IRNReturn
        {
            public long Irn { get; set; }
            public string CancelDate { get; set; }
        }


        public class EwbDtls
        {
            public string Irn { get; set; }
            public string CnlRsn { get; set; }
            public string CnlRem { get; set; }
        }


        public class Root
        {
            public EwbDtls EwbDtls { get; set; }
        }


        public string VersionValue(String strInvoiceNo)
        {
            DataSet dsData = new DataSet();
            String StrQry = "select IrnNo from tblInvoice where tblInvoice.deleted=0 and invoiceno='" + strInvoiceNo + "'";
            dbop.GetDataSet(StrQry, "tblInvoice", ref dsData);

            EwbDtls root = new EwbDtls()
            {
                Irn = dsData.Tables["tblInvoice"].Rows[0]["IrnNo"].ToString(),
                CnlRsn = "2",
                CnlRem = "Wrong Entry",
            };
          
            string objjsonData = JsonConvert.SerializeObject(root);
            return objjsonData;
        }



    }
}


