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
    public partial class SalesView : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            Det.Open();
            SqlCommand cmd = new SqlCommand("Select * from Sales", Det);
            cmd.ExecuteNonQuery();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            Salesgrd.DataSource = ds;
            Salesgrd.DataBind();
            Det.Close();

        }
        private bool invoiceexists(string InvoiceNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Sales WHERE InvoiceNo = @InvoiceNo", con);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            Det.Open();
            if (string.IsNullOrEmpty(InvoiceNo.Text))
            {
                ErrorLabel.Text = "Enter Invoice No.";
            }
            else if(!invoiceexists(InvoiceNo.Text))
            {
                ErrorLabel.Text = "Invalid Invoice No.";
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Select InvoiceNo, BarCode, Description, RetailPrice, Qty, Price, Disc, CONVERT(VARCHAR, Date, 103) AS Date from Sales where InvoiceNo = @InvoiceNo", Det);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                cmd.ExecuteNonQuery();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Salesgrd.DataSource = ds;
                Salesgrd.DataBind();
            }
            Det.Close();
        }
    }
}