
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
    public class JSHONFileReGenrateEInvoice
    {

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

    }
}


