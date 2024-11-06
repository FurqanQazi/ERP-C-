using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace ERPproject
{

    public partial class PurchaseReport : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private bool POinPO(string POnumber, string VendorCode, string Ref)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PurchaseOrder WHERE PONumber = @PONumber AND VendorCode = @VendorCode And RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@PONumber", POnumber);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", Ref);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }


        protected void Generate_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if(string.IsNullOrEmpty(PONo.Text)|| string.IsNullOrEmpty(VendorID.Text) || string.IsNullOrEmpty(Ref.Text))
                {
                    ErrorLabel.Text = "Enter PO, Vendor and Ref No.";
                }
                else if (!POinPO(PONo.Text, VendorID.Text, Ref.Text))
                {
                    ErrorLabel.Text = "Invalid PO, Vendor or Ref No.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT PONumber, VendorCode, VendorName, RefNo, Barcode, Description, UnitPrice, OrderQty, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date FROM PurchaseOrder WHERE PONumber = @PONumber and VendorCode = @VendorCode AND RefNo = @RefNo", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONo.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorID.Text);
                    cmd.Parameters.AddWithValue("@RefNo", Ref.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PONumber.Text = reader["PONumber"].ToString();
                            VendorCode.Text = reader["VendorCode"].ToString();
                            VendorName.Text = reader["VendorName"].ToString();
                            Refno.Text = reader["Refno"].ToString();
                            BarCode.Text = reader["BarCode"].ToString();
                            Description.Text = reader["Description"].ToString();
                            UnitPrice.Text = reader["UnitPrice"].ToString();
                            OrderQty.Text = reader["OrderQty"].ToString();
                            Total.Text = reader["TotalInvoice"].ToString();
                            Date.Text = reader["Date"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "No data found for the given VendorCode and RefNo.";
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            
            Det.Close();
        }
    }
}