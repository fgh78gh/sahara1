
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
    public class JSHONFileCancelEwaybill
    {
        DBOperation dbop = new DBOperation();

        public class IRNReturn
        {
            public long ewayBillNo { get; set; }
            public string cancelDate { get; set; }
        }


        public class EwbDtls
        {
            public string ewbNo { get; set; }
            public string cancelRsnCode { get; set; }
            public string cancelRmrk { get; set; }
        }


        public class Root
        {
            public EwbDtls EwbDtls { get; set; }
        }


        public string VersionValue(String strInvoiceNo)
        {
            DataSet dsData = new DataSet();
            String StrQry = "select EwayBillNo from tblInvoice where tblInvoice.deleted=0 and invoiceno='" + strInvoiceNo + "'";
            dbop.GetDataSet(StrQry, "tblInvoice", ref dsData);

            EwbDtls root = new EwbDtls()
            {
                ewbNo = dsData.Tables["tblInvoice"].Rows[0]["EwayBillNo"].ToString(),
                cancelRsnCode = "2",
                cancelRmrk = "Data Entry Mistake",
            };
          
            string objjsonData = JsonConvert.SerializeObject(root);
            return objjsonData;
        }



    }
}


