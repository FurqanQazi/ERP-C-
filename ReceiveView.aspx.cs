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
    public partial class ReceiveView : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            Det.Open();

            SqlCommand cmd = new SqlCommand("Select PONumber, InvoiceNo, ReceivingNo, VendorCode, RefNo, BarCode, Description, UnitPrice, OrderQty, ReceivingQty, PendingQty, Coeff, RetailPrice, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from ReceiveOrder", Det);
            cmd.ExecuteNonQuery();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            ReceiveGrd.DataSource = ds;
            ReceiveGrd.DataBind();

            Det.Close();
        }

        private bool PoinReceive(string PONumber)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReceiveOrder WHERE PONumber = @PONumber", con);
                cmd.Parameters.AddWithValue("@PONumber", PONumber);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool ReceiveinReceive(string ReceiveNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReceiveOrder WHERE ReceivingNo = @ReceivingNo", con);
                cmd.Parameters.AddWithValue("@ReceivingNo", ReceiveNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        
        private bool InvoiceinReceive(string InvoiceNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReceiveOrder WHERE InvoiceNo = @InvoiceNo", con);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool AllinReceive(string POnumber, string Receiveno, string InvoiceNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReceiveOrder WHERE POnumber = @POnumber and ReceivingNo = @ReceivingNo and InvoiceNo = @InvoiceNo", con);
                cmd.Parameters.AddWithValue("@POnumber", POnumber);
                cmd.Parameters.AddWithValue("@ReceivingNo", Receiveno);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
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
                    ErrorLabel.Text = "Enter PO Number";
                }
                else if (!PoinReceive(PONumber.Text))
                {
                    ErrorLabel.Text = "PO dosen't exist";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select PONumber, InvoiceNo, ReceivingNo, VendorCode, RefNo, BarCode, Description, UnitPrice, OrderQty, ReceivingQty, PendingQty, Coeff, RetailPrice, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from ReceiveOrder where PONumber = @PONumber", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    ReceiveGrd.DataSource = ds;
                    ReceiveGrd.DataBind();
                    ErrorLabel.Text = "Requested data fetched by PO";
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void ReceiveNoSearch_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(ReceiveNo.Text))
                {
                    ErrorLabel.Text = "Enter Receive Number";
                }
                else if (!ReceiveinReceive(ReceiveNo.Text))
                {
                    ErrorLabel.Text = "Receive No. dosen't exist";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select ReceivingNo, PONumber, InvoiceNo, VendorCode, RefNo, BarCode, Description, UnitPrice, OrderQty, ReceivingQty, PendingQty, Coeff, RetailPrice, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from ReceiveOrder where ReceivingNo = @ReceivingNo", Det);
                    cmd.Parameters.AddWithValue("@ReceivingNo", ReceiveNo.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    ReceiveGrd.DataSource = ds;
                    ReceiveGrd.DataBind();
                    ErrorLabel.Text = "Requested data fetched by ReceiveNo.";
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void InvoiceSearch_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Enter Invoice Number";
                }
                else if (!InvoiceinReceive(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Invoice dosen't exist";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select InvoiceNo, PONumber, ReceivingNo, InvoiceNo, VendorCode, RefNo, BarCode, Description, UnitPrice, OrderQty, ReceivingQty, PendingQty, Coeff, RetailPrice, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from ReceiveOrder where InvoiceNo = @InvoiceNo", Det);
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    ReceiveGrd.DataSource = ds;
                    ReceiveGrd.DataBind();
                    ErrorLabel.Text = "Requested data fetched by Invoice No.";
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
                if (string.IsNullOrEmpty(PONumber.Text) || string.IsNullOrEmpty(ReceiveNo.Text) || string.IsNullOrEmpty(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Enter all fields";
                }
                else if (!AllinReceive(PONumber.Text, ReceiveNo.Text, InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Invoice, PO and Receive No. dosen't exist";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select InvoiceNo, PONumber, ReceivingNo, InvoiceNo, VendorCode, RefNo, BarCode, Description, UnitPrice, OrderQty, ReceivingQty, PendingQty, Coeff, RetailPrice, TotalInvoice, CONVERT(VARCHAR, Date, 103) AS Date from ReceiveOrder where InvoiceNo = @InvoiceNo", Det);
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    ReceiveGrd.DataSource = ds;
                    ReceiveGrd.DataBind();

                    ErrorLabel.Text = "Requested data fetched by PO, Invoice and Receive No.";
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