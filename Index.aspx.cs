using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ERPproject
{
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source = LAPTOP-U47QFCME\MSSQLSERVER03; Database = ERP; User = sa1; Password= 1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            
            try

            {
                if (!string.IsNullOrEmpty(ID.Text) && !string.IsNullOrEmpty(Password.Text))
                { 
                    con.Open();
 
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Username = @Username AND Password = @Password", con);
                    cmd.Parameters.AddWithValue("@Username", ID.Text);
                    cmd.Parameters.AddWithValue("@Password", Password.Text);
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();

                    if (count > 0)
                    {
                        Response.Redirect("HomeLogin.aspx");
                    }
                    else
                    {
                        ErrorLabel.Text = "ID or Password is incorrect";
                    }
                }
                else
                {
                    ErrorLabel.Text = "Username and password cannot be empty.";
                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred: " + ex.Message);
            }
        }
    }
}