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
    public partial class PurchaseForm : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            TotalPrice.Enabled = false;
            UnitPrice.Enabled = false;
            BarCode.Enabled = false;
            VendorName.Enabled = false;
            Description.Enabled = false;
            PONumber.Enabled = false;
            VendorCode.Enabled = false;
            OrderQty.Enabled = false;
            RefNo.Enabled = false;

            if (!IsPostBack || (Request.Form["__EVENTTARGET"] == New.UniqueID && Request.Form["__EVENTARGUMENT"] == "") || (Request.Form["__EVENTTARGET"] == Edit.UniqueID && Request.Form["__EVENTARGUMENT"] == ""))
            {
                try
                {
                    Det.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 PONumber FROM PurchaseOrder ORDER BY ABS(DATEDIFF(SECOND, Date, GETDATE()));", Det);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PONumber.Text = reader["PONumber"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "An Error occurred: " + ex.Message;
                }
                finally
                {
                    Det.Close();
                }
            }

        }
        private bool PONumberinReceive(string PONumber, String VendorCode, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReceiveOrder WHERE PONumber = @PONumber AND VendorCode = @VendorCode AND RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@PONumber", PONumber);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        private bool VendorInStocks(string VendorCode, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE VendorCode = @VendorCode AND RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        private bool JustVendorInStocks(string VendorCode)
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

        private bool POinPO(string PONumber, string VendorCode, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PurchaseOrder WHERE PONumber = @PONumber AND VendorCode = @VendorCode AND RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@PONumber", PONumber);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private void ClearForm()
        {                       
            RefNo.Text = "";
            BarCode.Text = "";
            UnitPrice.Text = "";
            OrderQty.Text = "";
            TotalPrice.Text = "";
            VendorName.Text = "";
            Description.Text = "";
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                decimal unitprice, orderqty, totalprice;

                if (string.IsNullOrEmpty(PONumber.Text) ||
                    string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(RefNo.Text) ||
                    string.IsNullOrEmpty(BarCode.Text) ||
                    string.IsNullOrEmpty(UnitPrice.Text) ||
                    string.IsNullOrEmpty(OrderQty.Text) ||
                    string.IsNullOrEmpty(TotalPrice.Text) ||
                    string.IsNullOrEmpty(VendorName.Text) ||
                    string.IsNullOrEmpty(Description.Text))
                {
                    ErrorLabel.Text = "Please fill all Details!";
                    New.Enabled = true;
                }
                else if (!VendorInStocks(VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Supplier is not assosiated with RefNo or Wrong Vendor code!";
                    New.Enabled = true;
                }
                else if (!UnitPrice.Text.All(c => char.IsDigit(c) || c == '.'))
                {
                    ErrorLabel.Text = "Unit Price cannot contain letters.";
                    New.Enabled = true;
                }
                else if (BarCode.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "BarCode cannot have letters";
                    New.Enabled = true;
                }
                else if (POinPO(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Purchase is already done..Use new PO.Number";
                    New.Enabled = true;
                }
                else if (!(decimal.TryParse(UnitPrice.Text, out unitprice) && decimal.TryParse(OrderQty.Text, out orderqty) && decimal.TryParse(TotalPrice.Text, out totalprice) &&
                           totalprice == (unitprice * orderqty)))
                {
                    ErrorLabel.Text = "Try calc.";
                    New.Enabled = true;
                }

                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into PurchaseOrder values (@PONumber, @VendorCode, @VendorName, @RefNo, @BarCode, @Description, @UnitPrice, @OrderQty, @TotalInvoice, @Date)", Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.Parameters.AddWithValue("@VendorName", VendorName.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode.Text);
                    cmd.Parameters.AddWithValue("@Description", Description.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice.Text);
                    cmd.Parameters.AddWithValue("@OrderQty", OrderQty.Text);
                    cmd.Parameters.AddWithValue("@TotalInvoice", TotalPrice.Text);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);


                    cmd.ExecuteNonQuery();
                    ClearForm();
                    ErrorLabel.Text = "Purchase Completed!";
                    New.Enabled = false;
                }
                
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
            
        }

        protected void get_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(VendorCode.Text) || string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Please provide VendorCode and RefNo.";
                    RefNo.Enabled = true;
                    UnitPrice.Enabled = true;
                    OrderQty.Enabled = true;
                }
                else if (!VendorInStocks(VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Vendor or RefNo does not exist..Check stock report";
                    RefNo.Enabled = true;
                    UnitPrice.Enabled = true;
                    OrderQty.Enabled = true;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT UnitPrice, BarCode, VendorName, Description FROM Stocks WHERE VendorCode = @VendorCode AND RefNo = @RefNo", Det);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            UnitPrice.Text = reader["UnitPrice"].ToString();
                            BarCode.Text = reader["BarCode"].ToString();
                            VendorName.Text = reader["VendorName"].ToString();
                            Description.Text = reader["Description"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "No data found for the given VendorCode and RefNo.";
                        }
                    }
                    OrderQty.Enabled = true;
                    UnitPrice.Enabled = true;
                    RefNo.Enabled = true;


                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void calc_Click(object sender, EventArgs e)
        {
            VendorCode.Enabled = true;
            RefNo.Enabled = true;
            UnitPrice.Enabled = true;
            OrderQty.Enabled = true;

            if (string.IsNullOrEmpty(OrderQty.Text) || string.IsNullOrEmpty(UnitPrice.Text))
            {
                ErrorLabel.Text = "Please enter Unit Price and Order Qty!";
            }
            else if (!UnitPrice.Text.All(c => char.IsDigit(c) || c == '.')|| OrderQty.Text.Any(char.IsLetter))
            {
                ErrorLabel.Text = "OrderQty and Unit Price cannot have letters.";
            }
            else
            {
                decimal UP = decimal.Parse(UnitPrice.Text);
                decimal OQ = decimal.Parse(OrderQty.Text);
                decimal TP = UP * OQ;
                TotalPrice.Text = TP.ToString();

            }


        }

        protected void Edit_Click(object sender, EventArgs e)
        {

            PONumber.Enabled = true;
            VendorCode.Enabled = true;
            RefNo.Enabled = true;            
            OrderQty.Enabled = true;
            New.Enabled = false;
            VenEn.Enabled = false;
            Save.Enabled = false;
            Save.Style["background-color"] = "Gray";
            Save.Style["border"] = "none";


            Det.Open();
            try
            {
                
                if (string.IsNullOrEmpty(PONumber.Text)||
                    string.IsNullOrEmpty(VendorCode.Text)||
                    string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Specify P.O No., Vendor code and RefNo.";
                   
                }
                else if (!POinPO(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Data on P.O No., Vendor code and RefNo dose not exist.";
                   
                }
                else if (PONumberinReceive(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Order Received cannot be changed.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT VendorName, BarCode, Description, UnitPrice, OrderQty, TotalInvoice FROM PurchaseOrder WHERE PONumber = @PONumber AND VendorCode = @VendorCode AND RefNo = @RefNo", Det);

                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            VendorName.Text = reader["VendorName"].ToString();
                            BarCode.Text = reader["BarCode"].ToString();
                            Description.Text = reader["Description"].ToString();
                            UnitPrice.Text = reader["UnitPrice"].ToString();
                            OrderQty.Text = reader["OrderQty"].ToString();
                            TotalPrice.Text = reader["TotalInvoice"].ToString();
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

        protected void Update_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                decimal unitprice, orderqty, totalprice;
                if (string.IsNullOrEmpty(PONumber.Text) ||
                    string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(RefNo.Text) ||
                    string.IsNullOrEmpty(BarCode.Text) ||
                    string.IsNullOrEmpty(UnitPrice.Text) ||
                    string.IsNullOrEmpty(OrderQty.Text) ||
                    string.IsNullOrEmpty(TotalPrice.Text) ||
                    string.IsNullOrEmpty(VendorName.Text) ||
                    string.IsNullOrEmpty(Description.Text))
                {
                    ErrorLabel.Text = "Please fill all Details!";
                }
                else if (!POinPO(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Data on P.O No., Vendor code and RefNo dose not exist.";
                }
                else if (!UnitPrice.Text.All(c => char.IsDigit(c) || c == '.'))
                {
                    ErrorLabel.Text = "Unit Price and cannot contain letters.";
                }
                else if (BarCode.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "BarCode cannot have letters";
                }
                else if (!(decimal.TryParse(UnitPrice.Text, out unitprice) && decimal.TryParse(OrderQty.Text, out orderqty) && decimal.TryParse(TotalPrice.Text, out totalprice) &&
                           totalprice == (unitprice * orderqty)))
                {
                    ErrorLabel.Text = "Try calc.";
                }
                else
                {
                    string trimmedPONumber = PONumber.Text.Trim();
                    string trimmedVendorCode = VendorCode.Text.Trim();
                    string trimmedRefNumber = RefNo.Text.Trim();

                    using (SqlCommand cmd = new SqlCommand("UPDATE PurchaseOrder SET VendorCode=@VendorCode, VendorName=@VendorName, RefNo=@RefNo, BarCode=@BarCode, Description=@Description, UnitPrice=@UnitPrice, OrderQty=@OrderQty, TotalInvoice=@TotalInvoice WHERE PONumber=@PONumber", Det))
                    {
                        cmd.Parameters.AddWithValue("@PONumber", trimmedPONumber);
                        cmd.Parameters.AddWithValue("@VendorCode", trimmedVendorCode);
                        cmd.Parameters.AddWithValue("@VendorName", VendorName.Text);
                        cmd.Parameters.AddWithValue("@RefNo", trimmedRefNumber);
                        cmd.Parameters.AddWithValue("@BarCode", BarCode.Text);
                        cmd.Parameters.AddWithValue("@Description", Description.Text);
                        cmd.Parameters.AddWithValue("@UnitPrice", Convert.ToDecimal(UnitPrice.Text));
                        cmd.Parameters.AddWithValue("@OrderQty", Convert.ToInt32(OrderQty.Text));
                        cmd.Parameters.AddWithValue("@TotalInvoice", Convert.ToDecimal(TotalPrice.Text));


                        cmd.ExecuteNonQuery();
                        ClearForm();
                        ErrorLabel.Text = "Supplier Details Updated!";
                    }
                    Save.Enabled = false;
                    Save.Style["background-color"] = null;
                    Save.Style["border"] = null;

                }
            }


            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message; 
            }

            Det.Close();

        }

        protected void Delete_Click1(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (PONumberinReceive(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Order already received cannot be deleted";
                    VendorCode.Enabled = true;
                    RefNo.Enabled = true;
                }
                else if (string.IsNullOrEmpty(PONumber.Text) || string.IsNullOrEmpty(RefNo.Text)|| string.IsNullOrEmpty(VendorCode.Text))
                {
                    ErrorLabel.Text = "P.O.No., RefNo. and Vendor Code required";
                    VendorCode.Enabled = true;
                    RefNo.Enabled = true;
                }
                else if (!POinPO(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Data on P.O No., Vendor code and RefNo dose not exist.";
                    VendorCode.Enabled = true;
                    RefNo.Enabled = true;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Delete from PurchaseOrder where PONumber = @PONumber AND VendorCode = @VendorCode AND RefNo = @RefNo ", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    cmd.ExecuteNonQuery();

                    ErrorLabel.Text = "Record Deleted!";
                    ClearForm();
                    VendorCode.Enabled = false;
                    RefNo.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void New_Click(object sender, EventArgs e)
        {
            string prefix = "PO";
            string numericPart = PONumber.Text.Substring(prefix.Length);

            int incrementedValue;
            if (int.TryParse(numericPart, out incrementedValue))
            {
                incrementedValue++;
            }
            else
            {
                incrementedValue = 1;
            }
            PONumber.Text = prefix + incrementedValue.ToString().PadLeft(numericPart.Length, '0');

            VendorCode.Enabled = true;
            VendorCode.Focus();

        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            New.Enabled = true;
            VenEn.Enabled = true;
            ClearForm();
            VendorCode.Text = "";
            VendorCode.Enabled = false;
            Save.Enabled = true;
            Save.Style["background-color"] = null;
            Save.Style["border"] = null;
            try
            {
                Det.Open();

                SqlCommand cmd = new SqlCommand("SELECT TOP 1 PONumber FROM PurchaseOrder ORDER BY ABS(DATEDIFF(SECOND, Date, GETDATE()));", Det);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PONumber.Text = reader["PONumber"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An Error occurred: " + ex.Message;
            }
            finally
            {
                Det.Close();
            }
        }

        protected void VendorCode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(VendorCode.Text))
            {
                ErrorLabel.Text = "Please Give Vendor code";
                VendorCode.Enabled = true;
            }
            else if (!JustVendorInStocks(VendorCode.Text))
            {
                ErrorLabel.Text = "Please Give appropriate vendor from stocks";
                VendorCode.Enabled = true;
            }
            else
            {
                VendorCode.Enabled = false;
                RefNo.Enabled = true;
                UnitPrice.Enabled = true;
                OrderQty.Enabled = true;
            }
        }

        protected void VenEn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(VendorCode.Text))
            {
                ErrorLabel.Text = "Please Give Vendor code";
                VendorCode.Enabled = true;
            }
            else if (!JustVendorInStocks(VendorCode.Text))
            {
                ErrorLabel.Text = "Please Give appropriate vendor from stocks";
                VendorCode.Enabled = true;
            }
            else
            {
                VendorCode.Enabled = false;
                RefNo.Enabled = true;
                UnitPrice.Enabled = true;
                OrderQty.Enabled = true;
            }
        }

        protected void MainLogoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}