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
    public partial class ReceiveForm : System.Web.UI.Page
    {
        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            ReceivedPO.Enabled = false;
            BarCode.Enabled = false;
            Description.Enabled = false;
            OrderQty.Enabled = false;
            VendorName.Enabled = false;
            PendingQty.Enabled = false;
            UnitPrice.Enabled = false;
            RetailPrice.Enabled = false;
            TotalInvoice.Enabled = false;
        }

        private bool POrecord(string PONumber, String VendorCode, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PurchaseOrder WHERE PONumber = @PONumber AND VendorCode = @VendorCode AND RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@PONumber", PONumber);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool Receiveorder(string PONumber, String VendorCode, string RefNo)
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
        private bool Receivedorder(string PONumber, string InvoiceNo, string ReceivingNo, String VendorCode, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReceiveOrder WHERE PONumber = @PONumber AND InvoiceNo = @InvoiceNo AND ReceivingNo = @ReceivingNo AND VendorCode = @VendorCode AND RefNo = @RefNo", con);
                cmd.Parameters.AddWithValue("@PONumber", PONumber);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                cmd.Parameters.AddWithValue("@ReceivingNo", ReceivingNo);
                cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private void ClearForm()
        {           
            RefNo.Text = "";
            VendorName.Text = "";
            BarCode.Text = "";
            Description.Text = "";
            UnitPrice.Text = "";
            OrderQty.Text = "";
            ReceiveQty.Text = "";
            PendingQty.Text = "";
            Coeff.Text = "";
            RetailPrice.Text = "";
            TotalInvoice.Text = "";
            ReceivedPO.Text = "";
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                decimal unitprice, totalinv, retail, coeff;
                int receiveqty, orderqty, pendingqty, receivedpo;

                if (string.IsNullOrEmpty(PONumber.Text)||
                    string.IsNullOrEmpty(InvoiceNumber.Text) ||
                    string.IsNullOrEmpty(ReceivingNo.Text) ||
                    string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(RefNo.Text) ||
                    string.IsNullOrEmpty(VendorName.Text) ||
                    string.IsNullOrEmpty(BarCode.Text) ||
                    string.IsNullOrEmpty(Description.Text) ||
                    string.IsNullOrEmpty(UnitPrice.Text) ||
                    string.IsNullOrEmpty(OrderQty.Text) ||
                    string.IsNullOrEmpty(ReceiveQty.Text) ||
                    string.IsNullOrEmpty(PendingQty.Text) ||
                    string.IsNullOrEmpty(Coeff.Text) ||
                    string.IsNullOrEmpty(RetailPrice.Text) ||
                    string.IsNullOrEmpty(TotalInvoice.Text))
                {
                    ErrorLabel.Text = "Please fill all the details!";
                }
                else if(!POrecord(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Purchase not completed";
                }
                else if (!(decimal.TryParse(UnitPrice.Text, out unitprice) && int.TryParse(ReceiveQty.Text, out receiveqty) && decimal.TryParse(TotalInvoice.Text, out totalinv) &&
                          totalinv == (receiveqty * unitprice)))
                {
                    ErrorLabel.Text = "Try Calc!";
                }
                else if (!(decimal.TryParse(UnitPrice.Text, out unitprice) && decimal.TryParse(Coeff.Text, out coeff) && decimal.TryParse(RetailPrice.Text, out retail) &&
                          retail == (coeff * unitprice)))
                {
                    ErrorLabel.Text = "Try Calc!";
                }
                else if (!(int.TryParse(OrderQty.Text, out orderqty) && int.TryParse(ReceiveQty.Text, out receiveqty) && int.TryParse(PendingQty.Text, out pendingqty) && int.TryParse(ReceivedPO.Text, out receivedpo) &&
                          pendingqty == (orderqty - receiveqty - receivedpo)))
                {
                    ErrorLabel.Text = "Try Calc!";
                }
                else if (int.Parse(OrderQty.Text) == int.Parse(ReceivedPO.Text)) 
                {
                    ErrorLabel.Text = "Order already completed";
                }
                else if (Receivedorder(PONumber.Text, InvoiceNumber.Text, ReceivingNo.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "No duplicate record can be inserted.. change Invoice and Receive Number.";
                }
                else if (InvoiceNumber.Text.Any(char.IsLetter)|| ReceivingNo.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "Invoice and Receive can only Have digits";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into ReceiveOrder values(@PONumber, @InvoiceNo, @ReceivingNo, @VendorCode, @RefNo, @VendorName, @BarCode, @Description, @UnitPrice, @OrderQty, @ReceivingQty, @PendingQty, @Coeff, @RetailPrice, @TotalInvoice, @Date) ", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNumber.Text);
                    cmd.Parameters.AddWithValue("@ReceivingNo", ReceivingNo.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    cmd.Parameters.AddWithValue("@VendorName", VendorName.Text);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode.Text);
                    cmd.Parameters.AddWithValue("@Description", Description.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice.Text);
                    cmd.Parameters.AddWithValue("@OrderQty", OrderQty.Text);
                    cmd.Parameters.AddWithValue("@ReceivingQty", ReceiveQty.Text);
                    cmd.Parameters.AddWithValue("@PendingQty", PendingQty.Text);
                    cmd.Parameters.AddWithValue("@Coeff", Coeff.Text);
                    cmd.Parameters.AddWithValue("@RetailPrice", RetailPrice.Text);
                    cmd.Parameters.AddWithValue("@TotalInvoice", TotalInvoice.Text);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmmd = new SqlCommand("UPDATE Stocks SET StockQty = @StockQty, RetailPrice = @RetailPrice WHERE VendorCode = @VendorCode AND RefNo = @RefNo", Det);
                    cmmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmmd.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    cmmd.Parameters.AddWithValue("@StockQty", newstkqty.Text);
                    cmmd.Parameters.AddWithValue("@RetailPrice", RetailPrice.Text);
                    cmmd.ExecuteNonQuery();

                    ErrorLabel.Text = "Order Received!";
                    ClearForm();

                    PONumber.Enabled = false;
                    InvoiceNumber.Enabled = false;
                    ReceivingNo.Enabled = false;
                    VendorCode.Enabled = false;
                    Save.Enabled = true;
                    Save.Style["background-color"] = null;
                    Save.Style["border"] = null;

                    

                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
            
        }
       

        protected void Get_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {

                if (string.IsNullOrEmpty(PONumber.Text) ||
                    string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Please specify PO, Vendor and Ref!";
                }
                else if (!POrecord(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Purchase not completed! Enter Correct PO, Vendor and Ref ";
                }
                else if (Receiveorder(PONumber.Text, VendorCode.Text, RefNo.Text))
                {
                    SqlCommand cmd = new SqlCommand("Select BarCode, Description, UnitPrice, OrderQty, VendorName, Coeff, SUM(ReceivingQty) OVER (Partition By PONumber, VendorCode, RefNo) AS ReceivedPO from ReceiveOrder where PONumber = @PONumber and VendorCode = @VendorCode and RefNo = @RefNo", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            BarCode.Text = reader["BarCode"].ToString();
                            Description.Text = reader["Description"].ToString();
                            UnitPrice.Text = reader["UnitPrice"].ToString();
                            OrderQty.Text = reader["OrderQty"].ToString();
                            VendorName.Text = reader["VendorName"].ToString();
                            Coeff.Text = reader["Coeff"].ToString();
                            ReceivedPO.Text = reader["ReceivedPO"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "No data found for the given PONumber, VendorCode and RefNo.";
                        }
                        Coeff.Enabled = false;
                    }

                    SqlCommand klm = new SqlCommand("Select StockQty from Stocks where VendorCode = @VendorCode and RefNo = @RefNo", Det);
                    klm.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    klm.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    using (SqlDataReader reader = klm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            StockQty.Text = reader["StockQty"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "Invalid Vendor and RefNo";
                        }
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select BarCode, Description, UnitPrice, OrderQty, VendorName from PurchaseOrder where PONumber = @PONumber and VendorCode = @VendorCode and RefNo = @RefNo ", Det);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            BarCode.Text = reader["BarCode"].ToString();
                            Description.Text = reader["Description"].ToString();
                            UnitPrice.Text = reader["UnitPrice"].ToString();
                            OrderQty.Text = reader["OrderQty"].ToString();
                            VendorName.Text = reader["VendorName"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "No data found for the given PONumber, VendorCode and RefNo.";
                        }
                    }

                    SqlCommand klm = new SqlCommand("Select StockQty from Stocks where VendorCode = @VendorCode and RefNo = @RefNo", Det);
                    klm.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    klm.Parameters.AddWithValue("@RefNo", RefNo.Text);
                    using (SqlDataReader reader = klm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            StockQty.Text = reader["StockQty"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "Invalid Vendor and RefNo";
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            ClearForm();

            PONumber.Enabled = true;
            InvoiceNumber.Enabled = true;
            ReceivingNo.Enabled = true;
            VendorCode.Enabled = true;
            Save.Enabled = true;
            Save.Style["background-color"] = null;
            Save.Style["border"] = null;

            PONumber.Text = "";
            InvoiceNumber.Text = "";
            ReceivingNo.Text = "";
            VendorCode.Text = "";
        }

        protected void Calc_Click(object sender, EventArgs e)
        {
            try
            {
                string Coefff = Coeff.Text.Trim();
                if (string.IsNullOrEmpty(Coeff.Text) || string.IsNullOrEmpty(ReceiveQty.Text))
                {
                    ErrorLabel.Text = "ReceiveQty and coeff cannot be empty";
                }
                else if (string.IsNullOrEmpty(OrderQty.Text) || string.IsNullOrEmpty(UnitPrice.Text))
                {
                    ErrorLabel.Text = "Complete the form";
                }
                else if (ReceiveQty.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "Invalid Receive Qty";
                }
                else if (int.Parse(ReceiveQty.Text) > int.Parse(OrderQty.Text))
                {
                    ErrorLabel.Text = "Receive Qty cannot be greater than OrderQty";
                }
                else if (Coefff.Any(c => !char.IsDigit(c) && c != '.'))
                {
                    ErrorLabel.Text = "Coeff cannot have letters";
                }
                else
                {
                    int RQ = int.Parse(ReceiveQty.Text);
                    int OQ = int.Parse(OrderQty.Text);
                    int stkqty = int.Parse(StockQty.Text);
                    int RePO = int.Parse(ReceivedPO.Text);
                    decimal UP = decimal.Parse(UnitPrice.Text);
                    decimal CO = decimal.Parse(Coeff.Text);
                    int sub = OQ - RePO;


                    if (RQ > sub)
                    {
                        ErrorLabel.Text = "ReceiveQty must be less than ReceivedPO";
                    }
                    else if (string.IsNullOrEmpty(ReceivedPO.Text))
                    {
                        int PQ = OQ - RQ;
                        PendingQty.Text = PQ.ToString();

                        int nstkq = RQ + stkqty;
                        newstkqty.Text = nstkq.ToString();

                        decimal TI = RQ * UP;
                        TotalInvoice.Text = TI.ToString();

                        decimal RP = CO * UP;
                        RetailPrice.Text = RP.ToString();


                    }
                    else
                    {

                        int nstkq = RQ + stkqty;
                        newstkqty.Text = nstkq.ToString();

                        decimal TI = RQ * UP;
                        TotalInvoice.Text = TI.ToString();

                        decimal RP = CO * UP;
                        RetailPrice.Text = RP.ToString();

                        int PQ = OQ - RQ - RePO;
                        PendingQty.Text = PQ.ToString();

                    }
                }
            }

            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred"+ ex.Message;
            }
            
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(PONumber.Text) ||
                    string.IsNullOrEmpty(InvoiceNumber.Text) ||
                    string.IsNullOrEmpty(ReceivingNo.Text) ||
                    string.IsNullOrEmpty(VendorCode.Text) ||
                    string.IsNullOrEmpty(RefNo.Text))
                {
                    ErrorLabel.Text = "Provide PO, Invoice, Receiving, Vendor and Ref number.";
                }
                else if (!Receivedorder(PONumber.Text, InvoiceNumber.Text, ReceivingNo.Text, VendorCode.Text, RefNo.Text))
                {
                    ErrorLabel.Text = "Given details doesn't exist";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Delete from ReceiveOrder where PONumber = @PONumber and InvoiceNo = @InvoiceNo and ReceivingNo = @ReceivingNo and VendorCode = @VendorCode and RefNo = @RefNo", Det);
                    cmd.Parameters.AddWithValue("@PONumber",PONumber.Text);
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNumber.Text);
                    cmd.Parameters.AddWithValue("@ReceivingNo", ReceivingNo.Text);
                    cmd.Parameters.AddWithValue("@VendorCode", VendorCode.Text);
                    cmd.Parameters.AddWithValue("@RefNo", RefNo.Text);

                    cmd.ExecuteNonQuery();

                    ErrorLabel.Text = "Record Deleted Successfully!";
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void MainLogoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}