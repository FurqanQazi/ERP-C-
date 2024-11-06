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
    public partial class StocksView : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            Det.Open();
            SqlCommand cmd = new SqlCommand("SELECT VendorCode, VendorName, RefNo, UnitPrice, StockQty, BarCode, RetailPrice, SoldQty, Description, Disc, CONVERT(VARCHAR, Date, 103) AS Date from STOCKS", Det);
            cmd.ExecuteNonQuery();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            StkGrid.DataSource = ds;
            StkGrid.DataBind();

            Det.Close();
        }

        private bool VenorcodeExistSupplier(string VendorCode)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE VendorCode = @VendorCode", con);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool VenRefNoexistSupplier(string VendorCode, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE VendorCode = @VendorCode and RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool RefNoexistSupplier(string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool BarCdexistSupplier(string BarCode)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE BarCode = @BarCode", con);
                cmd.Parameters.AddWithValue("@BarCode", BarCode);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        protected void SearchVendorID_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorID.Text))
                {
                    ErrorLabel.Text = "Enter Vendor ID";
                }
                else if (!VenorcodeExistSupplier(VendorID.Text))
                {
                    ErrorLabel.Text = "Incorrect Vendor ID";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT VendorCode, VendorName, RefNo, UnitPrice, StockQty, BarCode, RetailPrice, SoldQty, Description, Disc, CONVERT(VARCHAR, Date, 103) AS Date from STOCKS where VendorCode = @VendorCode", Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorID.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    StkGrid.DataSource = ds;
                    StkGrid.DataBind();
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void SearchRefNo_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Enter Ref ID";
                }
                else if (!RefNoexistSupplier(RefNo.Text))
                {
                    ErrorLabel.Text = "Incorrect Ref No.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT VendorCode, VendorName, RefNo, UnitPrice, StockQty, BarCode, RetailPrice, SoldQty, Description, Disc, CONVERT(VARCHAR, Date, 103) AS Date from STOCKS where RefNo = @RefNo", Det);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    StkGrid.DataSource = ds;
                    StkGrid.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void SeacrhBarCode_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(BarCode.Text))
                {
                    ErrorLabel.Text = "Enter BarCode";
                }
                else if (!BarCdexistSupplier(BarCode.Text))
                {
                    ErrorLabel.Text = "Incorrect BarCode.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT VendorCode, VendorName, RefNo, UnitPrice, StockQty, BarCode, RetailPrice, SoldQty, Description, Disc, CONVERT(VARCHAR, Date, 103) AS Date from STOCKS where BarCode = @BarCode", Det);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    StkGrid.DataSource = ds;
                    StkGrid.DataBind();
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
                if (string.IsNullOrEmpty(VendorID.Text)|| string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Enter Vendor ID and Ref No.";
                }
                else if (!VenRefNoexistSupplier(VendorID.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Incorrect Vendor ID and Ref No.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT VendorCode, VendorName, RefNo, UnitPrice, StockQty, BarCode, RetailPrice, SoldQty, Description, Disc, CONVERT(VARCHAR, Date, 103) AS Date from STOCKS where VendorCode = @VendorCode and RefNo = @RefNo", Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorID.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    StkGrid.DataSource = ds;
                    StkGrid.DataBind();
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