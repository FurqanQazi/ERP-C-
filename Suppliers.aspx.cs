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
    public partial class Suppliers : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");

        protected void Page_Load(object sender, EventArgs e)
        {

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
        private bool HasForeignKeyConstraints(string vendorCode)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM sys.foreign_keys AS fk " +
                                                   "JOIN sys.tables AS t ON fk.parent_object_id = t.object_id " +
                                                   "JOIN sys.columns AS c ON fk.parent_object_id = c.object_id " +
                                                   "WHERE c.name = @VendorCode", Det))
            {
                cmd.Parameters.AddWithValue("@VendorCode", vendorCode);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void ClearForm()
        {
            VendorCode.Text = "";
            VendorName.Text = "";
            Email.Text = "";
            PhoneNo.Text = "";
            Address.Text = "";
            PostCode.Text = "";
            Country.Text = "";
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            { 
                if(string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(VendorName.Text) ||
                    string.IsNullOrEmpty(Address.Text) ||
                    string.IsNullOrEmpty(PostCode.Text) ||
                    string.IsNullOrEmpty(PhoneNo.Text) ||
                    string.IsNullOrEmpty(Email.Text) ||
                    string.IsNullOrEmpty(Country.Text))
                {
                    ErrorLabel.Text = "Please Fill all the Details.";
                }

                else if (!Email.Text.Contains("@") || !Email.Text.Contains("."))
                {
                    ErrorLabel.Text = "Invalid Email";
                }
                else if (PhoneNo.Text.Length > 15)
                {
                    ErrorLabel.Text = "Invalid Phone No.. Enter number less than 15 Digits";
                }
                else if (!PhoneNo.Text.Replace(" ", "").All(c => char.IsDigit(c) || c == '+'))
                {
                    ErrorLabel.Text = "Invalid Phone no.";
                }
                else if (VenorcodeExist(VendorCode.Text))
                {
                    ErrorLabel.Text = "Vendor with the specified ID already exist.";
                }
                else
                {
                    
                    SqlCommand cmd = new SqlCommand("Insert into Suppliers values (@VendorCode, @VendorName, @PhoneNo, @Email, @Address, @PostCode, @Country, @Date)",Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@VendorName", VendorName.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", PhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Email", Email.Text);
                    cmd.Parameters.AddWithValue("@Address", Address.Text);
                    cmd.Parameters.AddWithValue("@PostCode", PostCode.Text);
                    cmd.Parameters.AddWithValue("@Country", Country.Text);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    ClearForm();
                    ErrorLabel.Text = "Vendor Added!";
                }
                    
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An Error occurred: "+ ex.Message ;
            }
            Det.Close();
            
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorCode.Text))
                {
                    ErrorLabel.Text = "Please Enter Vendor code to retrieve data";
                }

                else if (!VenorcodeExist(VendorCode.Text))
                {
                    ErrorLabel.Text = "Vendor with the specified ID does not exist.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Suppliers WHERE VendorCode = @VendorCode", Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            VendorName.Text = reader["VendorName"].ToString();
                            Address.Text = reader["Address"].ToString();
                            PostCode.Text = reader["PostCode"].ToString();
                            PhoneNo.Text = reader["PhoneNumber"].ToString();
                            Email.Text = reader["Email"].ToString();
                            Country.Text = reader["Country"].ToString();
                        }
                    }
                    Add.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An Error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(VendorName.Text) ||
                    string.IsNullOrEmpty(Address.Text) ||
                    string.IsNullOrEmpty(PostCode.Text) ||
                    string.IsNullOrEmpty(PhoneNo.Text) ||
                    string.IsNullOrEmpty(Email.Text) ||
                    string.IsNullOrEmpty(Country.Text))
                {
                    ErrorLabel.Text = "Please Fill all the Details.";
                }
                else if (!VenorcodeExist(VendorCode.Text))
                {
                    ErrorLabel.Text = "Vendor with the specified ID does not exist.";
                }
                else if (!Email.Text.Contains("@") || !Email.Text.Contains("."))
                {
                    ErrorLabel.Text = "Invalid Email";
                }
                else if (PhoneNo.Text.Length > 15)
                {
                    ErrorLabel.Text = "Invalid Phone No.. Enter number less than 15 Digits";
                }

                else if (!PhoneNo.Text.Replace(" ", "").All(c => char.IsDigit(c) || c == '+'))
                {
                    ErrorLabel.Text = "Invalid Phone no.";
                }
                else
                {
                    string trimmedVendorCode = VendorCode.Text.Trim();

                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET VendorName=@VendorName, PhoneNumber=@PhoneNumber, Email=@Email, Address=@Address, PostCode=@PostCode, Country=@Country WHERE VendorCode=@VendorCode", Det);

                    cmd.Parameters.AddWithValue("@VendorCode", trimmedVendorCode);
                    cmd.Parameters.AddWithValue("@VendorName", VendorName.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Email", Email.Text);
                    cmd.Parameters.AddWithValue("@Address", Address.Text);
                    cmd.Parameters.AddWithValue("@PostCode", PostCode.Text);
                    cmd.Parameters.AddWithValue("@Country", Country.Text);

                    cmd.ExecuteNonQuery();

                    ClearForm();
                    ErrorLabel.Text = "Supplier Details Updated!";
                    Add.Enabled = true;
                }
            }

            catch(Exception ex)
            {
                ErrorLabel.Text = "An Error occurred: " + ex.Message;
            }
            Det.Close();

        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorCode.Text))
                {
                    ErrorLabel.Text = "Please Enter Vendor Code to delete Data";
                }
                else if (!VenorcodeExist(VendorCode.Text))
                {
                    ErrorLabel.Text = "Vendor with the specified ID does not exist.";
                }
                else if (HasForeignKeyConstraints(VendorCode.Text))
                {
                    ErrorLabel.Text = "Cannot delete the vendor as it has associated records.";
                }
                else
                {
                    
                    SqlCommand cmd = new SqlCommand("DELETE FROM Suppliers WHERE VendorCode = @VendorCode", Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);

                    cmd.ExecuteNonQuery();

                    ErrorLabel.Text = "Supplier Details Deleted Successfully!";
                    ClearForm();
                }

            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An Error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void MainLogoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}