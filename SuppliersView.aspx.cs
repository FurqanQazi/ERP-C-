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
    public partial class SuppliersView : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            Det.Open();
            SqlCommand cmd = new SqlCommand("SELECT VendorCode, VendorName, PhoneNumber, Email, Address, PostCode, Country, CONVERT(VARCHAR, Date, 103) AS Date from Suppliers", Det);
            cmd.ExecuteNonQuery();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            StkGrid.DataSource = ds;
            StkGrid.DataBind();

            Det.Close();
        }
        private bool VenorcodeExist(string VendorCode)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Suppliers WHERE VendorCode = @VendorCode", con);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {            
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorID.Text))
                {
                    ErrorLabel.Text = "Please Enter Vendor Code.";
                }
                else if (!VenorcodeExist(VendorID.Text))
                {
                    ErrorLabel.Text = "Vendor Code does not exist.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT VendorCode, VendorName, PhoneNumber, Email, Address, PostCode, Country, CONVERT(VARCHAR, Date, 103) AS Date from Suppliers where VendorCode = @VendorCode", Det);
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
    }
}