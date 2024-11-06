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
    public partial class Stocks : System.Web.UI.Page
    {
        private readonly SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Save.Enabled = false;
                Stock.Enabled = true;
                Save.Style["background-color"] = "Gray";
                Save.Style["border"] = "none";
                SoldQty.Enabled = false;
                StockQty.Enabled = false;
                VendorName.Enabled = false;
                UnitPrice.Enabled = false;
                RetailPrice.Enabled = false;
                BarCode.Enabled = false;
                Disc.Enabled = false;
                Descrip.Enabled = false;

                Disc_add.Enabled = false;
                Disc_add.Style["background-color"] = "Gray";
                Disc_add.Style["border"] = "none";

                Get.Enabled = false;
                Get.Style["background-color"] = "Gray";
                Get.Style["border"] = "none";
            }

        }

        private bool VenorcodeExistSupplier(string VendorCode)
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

        private void ClearForm()
        {
            VendorCode.Text = "";
            RefNo.Text = "";
            BarCode.Text = "";
            Descrip.Text = "";
            UnitPrice.Text = "";
            RetailPrice.Text = "";
            StockQty.Text = "";
            SoldQty.Text = "";
            VendorName.Text = "";
        }
        private bool VenorcodeRefExist(string VendorCode, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE VendorCode = @VendorCode and RefNo = @RefNo", con);
                ;
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);

                int count = (int)cmd.ExecuteScalar();

                return count > 0;

            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            StockQty.Enabled = false;
            SoldQty.Enabled = false;
            //RetailPrice.Enabled = false;
            Save.Enabled = true;
            Save.Style["background-color"] = null;
            Save.Style["border"] = null;
            Stock.Enabled = false;
            Stock.Style["background-color"] = "Gray";
            Stock.Style["border"] = "none";
            Get.Enabled = true;
            Get.Style["background-color"] = null;
            Get.Style["border"] = null;

        }

        protected void Stock_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                if (
                   string.IsNullOrEmpty(VendorCode.Text) ||
                   string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Please Enter Vendor Code and RefNo.";
                }
                else if (!VenorcodeExistSupplier(VendorCode.Text))
                {
                    ErrorLabel.Text = "Vendor Does not Exist... Check Supplier List or Add New Supplier!";
                }
                else if (!VenorcodeRefExist(VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Vendor with Refno does not Exist!";
                }

                else
                {
                    SqlCommand cmd = new SqlCommand("select * from Stocks where VendorCode=@VendorCode and RefNo =  @RefNo", con);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            BarCode.Text = ds.Tables[0].Rows[i]["BarCode"].ToString();
                            Descrip.Text = ds.Tables[0].Rows[i]["Description"].ToString();
                            UnitPrice.Text = ds.Tables[0].Rows[i]["RetailPrice"].ToString();
                            RetailPrice.Text = ds.Tables[0].Rows[i]["RetailPrice"].ToString();
                            StockQty.Text = ds.Tables[0].Rows[i]["StockQty"].ToString();
                            SoldQty.Text = ds.Tables[0].Rows[i]["SoldQty"].ToString();
                            VendorName.Text = ds.Tables[0].Rows[i]["VendorName"].ToString();
                            Disc.Text = ds.Tables[0].Rows[i]["Disc"].ToString();


                        }

                    }

                    Save.Enabled = false;
                    Save.Style["background-color"] = "Gray";
                    Save.Style["border"] = "none";
                    Add.Enabled = false;
                    Add.Style["background-color"] = "Gray";
                    Add.Style["border"] = "none";
                    Disc.Enabled = true;
                    Disc_add.Enabled = true;
                    Disc_add.Style["background-color"] = null;
                    Disc_add.Style["border"] = null;
                }


            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            con.Close();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                int unitPricePeriodCount = UnitPrice.Text.Count(c => c == '.');
                int retailPricePeriodCount = RetailPrice.Text.Count(c => c == '.');
                if (string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(VendorName.Text) ||
                    string.IsNullOrEmpty(RefNo.Text) ||
                    string.IsNullOrEmpty(UnitPrice.Text) ||
                    string.IsNullOrEmpty(BarCode.Text) ||
                    string.IsNullOrEmpty(RetailPrice.Text) ||
                    string.IsNullOrEmpty(Descrip.Text))
                {
                    ErrorLabel.Text = "Please fill all the required fields.";
                }
                else if (!VenorcodeExistSupplier(VendorCode.Text))
                {
                    ErrorLabel.Text = "Supplier not found. Add new Supplier.";
                }
                else if (VenorcodeRefExist(VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Vendor with RefNo already exist.";
                }
                else if (Disc.Text.Any(c => !char.IsDigit(c) && c != '.'))
                {
                    ErrorLabel.Text = "Disc cannot have letters";
                }
                else if (unitPricePeriodCount > 1 || retailPricePeriodCount > 1 ||
                         !UnitPrice.Text.All(c => char.IsDigit(c) || c == '.') ||
                         !RetailPrice.Text.All(c => char.IsDigit(c) || c == '.'))
                {
                    ErrorLabel.Text = "Unit Price and Retail Price cannot contain letters.";
                }
                else if (BarCode.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "BarCode cannot have letters";                
                }
                else
                {
                    long barcodeValue = long.Parse(BarCode.Text);

                    SqlCommand cmd = new SqlCommand("INSERT INTO Stocks (VendorCode, VendorName, RefNo, UnitPrice, StockQty, BarCode, RetailPrice, SoldQty, Description, Disc, Date) VALUES (@VendorCode, @VendorName, @RefNo, @UnitPrice, @StockQty, @BarCode, @RetailPrice, @SoldQty, @Description, @Disc, @Date)", con);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@VendorName", VendorName.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice.Text);
                    cmd.Parameters.AddWithValue("@StockQty", StockQty.Text);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode.Text);
                    cmd.Parameters.AddWithValue("@RetailPrice", RetailPrice.Text);
                    cmd.Parameters.AddWithValue("@SoldQty", SoldQty.Text);
                    cmd.Parameters.AddWithValue("@Description", Descrip.Text);
                    cmd.Parameters.AddWithValue("@Disc", Disc.Text);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    cmd.ExecuteNonQuery();

                    Stock.Enabled = true;
                    Stock.Style["background-color"] = null;
                    Stock.Style["border"] = null;
                    Save.Enabled = false;
                    Save.Style["background-color"] = "Gray";
                    Save.Style["border"] = "none";
                    Get.Enabled = false;
                    Get.Style["background-color"] = "Gray";
                    Get.Style["border"] = "none";
                    UnitPrice.Enabled = false;
                    RetailPrice.Enabled = false;
                    BarCode.Enabled = false;
                    Descrip.Enabled = false;

                    ErrorLabel.Text = "New item Added successfully!";


                    ClearForm();
                    Disc.Text = "";
                }


            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            con.Close();
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            ClearForm();
            Disc.Text = "";

            Disc_add.Enabled = false;
            Disc_add.Style["background-color"] = "Gray";
            Disc_add.Style["border"] = "none";

            Add.Enabled = true;
            Add.Style["background-color"] = null;
            Add.Style["border"] = null;
            Save.Enabled = false;
            Save.Style["background-color"] = "Gray";
            Save.Style["border"] = "none";
            Stock.Enabled = true;
            Stock.Style["background-color"] = null;
            Stock.Style["border"] = null;


            SoldQty.Enabled = false;
            StockQty.Enabled = false;
            VendorName.Enabled = false;
            UnitPrice.Enabled = false;
            RetailPrice.Enabled = false;
            BarCode.Enabled = false;
            Descrip.Enabled = false;
            Get.Enabled = false;
            Disc.Enabled = false;


        }

        protected void Get_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorCode.Text))
                {
                    ErrorLabel.Text = "Please Give Vendor Code";
                }
                else if (!VenorcodeExistSupplier(VendorCode.Text))
                {
                    ErrorLabel.Text = "Invalid Vendor Code";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT VendorName FROM Suppliers WHERE VendorCode = @VendorCode", con);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            VendorName.Text = reader["VendorName"].ToString();
                        }
                    }
                    UnitPrice.Enabled = true;
                    RetailPrice.Enabled = true;
                    BarCode.Enabled = true;
                    Descrip.Enabled = true;
                    Disc.Enabled = true;
                }
            }

            catch(Exception ex)
            {
                ErrorLabel.Text = "An Error occurred: "+ ex.Message ;
            }
            con.Close();
            
        }

        protected void Disc_add_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                if(string.IsNullOrEmpty(VendorCode.Text)|| string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Enter Vendor and Ref no.";
                }
                else if (!VenorcodeRefExist(VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Incorrect Vendor and Ref no.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Update Stocks set Disc = @Disc where VendorCode = @VendorCode and RefNo = @RefNo", con);
                    cmd.Parameters.AddWithValue("@Disc", Disc.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    cmd.ExecuteNonQuery();

                    ClearForm();
                    Disc.Text = "";
                    Disc.Enabled = false;
                    Disc_add.Enabled = false;
                    Disc_add.Style["background-color"] = "Gray";
                    Disc_add.Style["border"] = "none";

                    ErrorLabel.Text = "Disc added Successfully";

                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void MainLogoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
