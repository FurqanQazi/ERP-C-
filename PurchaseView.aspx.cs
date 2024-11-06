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
    public partial class PurchaseView : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            Det.Open();

            SqlCommand cmd = new SqlCommand("Select PONumber, VendorCode, VendorName, RefNo, BarCode, Description, UnitPrice, OrderQty, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from PurchaseOrder", Det);
            cmd.ExecuteNonQuery();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            PurchaseGrd.DataSource = ds;
            PurchaseGrd.DataBind();

            Det.Close();
        }

        private bool POinPO(string PONumber)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PurchaseOrder WHERE PONumber = @PONumber", con);
                cmd.Parameters.AddWithValue("@PONumber", PONumber);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool VendorinPO(string VendorCode)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PurchaseOrder WHERE VendorCode = @VendorCode", con);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        
        private bool POandvendorinPO(string PONumber, string VendorCode)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PurchaseOrder WHERE PONumber = @PONumber and VendorCode = @VendorCode", con);
                cmd.Parameters.AddWithValue("@PONumber", PONumber);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        protected void SearchPoNumber_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(PONumber.Text))
                {
                    ErrorLabel.Text = "Please Enter PO Number";
                }
                else if (!POinPO(PONumber.Text))
                {
                    ErrorLabel.Text = "Invalid PO Number";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select PONumber, VendorCode, VendorName, RefNo, BarCode, Description, UnitPrice, OrderQty, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from PurchaseOrder where PONumber= @PONumber", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    PurchaseGrd.DataSource = ds;
                    PurchaseGrd.DataBind();

                    ErrorLabel.Text = "Requested data fetched by PO";
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void SearchVendorCode_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorCode.Text))
                {
                    ErrorLabel.Text = "Please Enter Vendor Code";
                }
                else if (!VendorinPO(VendorCode.Text))
                {
                    ErrorLabel.Text = "Invalid Vendor Code";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select VendorCode, PONumber, VendorName, RefNo, BarCode, Description, UnitPrice, OrderQty, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from PurchaseOrder where VendorCode= @VendorCode", Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    PurchaseGrd.DataSource = ds;
                    PurchaseGrd.DataBind();

                    ErrorLabel.Text = "Requested data fetched by Vendor";
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(PONumber.Text) || string.IsNullOrEmpty(VendorCode.Text))
                {
                    ErrorLabel.Text = "Please Enter PO Number and Vendor Code";
                }
                else if (!POandvendorinPO(PONumber.Text, VendorCode.Text))
                {
                    ErrorLabel.Text = "Invalid PO Number and Vendor Code";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select PONumber, VendorCode, VendorName, RefNo, BarCode, Description, UnitPrice, OrderQty, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from PurchaseOrder where PONumber= @PONumber and VendorCode= @VendorCode", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    PurchaseGrd.DataSource = ds;
                    PurchaseGrd.DataBind();
                    ErrorLabel.Text = "Requested data fetched by PO and Vendor";
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }
    }
}