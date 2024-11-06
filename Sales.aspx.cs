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
    public partial class Sales : System.Web.UI.Page
    {

        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            Description.Enabled = false;
            Price.Enabled = false;
            Retail.Enabled = false;
            TotalPrice.Enabled = false;

            if (!IsPostBack)
            {
                InvoiceNo.Enabled = false;
            }

            if (!IsPostBack || (Request.Form["__EVENTTARGET"] == New.UniqueID && Request.Form["__EVENTARGUMENT"] == ""))
            {
                try
                {
                    Det.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 InvoiceNo FROM InvoiceSale ORDER BY ABS(DATEDIFF(SECOND, Date, GETDATE()));", Det);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            InvoiceNo.Text = reader["InvoiceNo"].ToString();
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
        private bool barcodeexist(string Barcode)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE BarCode = @BarCode", con);
                cmd.Parameters.AddWithValue("@BarCode", Barcode);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool invoiceexist(string InvoiceNo)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM InvoiceSale WHERE InvoiceNo = @InvoiceNo", con);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private int GetRowCountInSales()
        {
            SqlCommand cmdRowCount = new SqlCommand("SELECT COUNT(*) FROM Sales WHERE BarCode = @BarCode AND InvoiceNo = @InvoiceNo", Det);
            cmdRowCount.Parameters.AddWithValue("@Barcode", BarCode.Text);
            cmdRowCount.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
            return Convert.ToInt32(cmdRowCount.ExecuteScalar());
        }

        private bool invoiceinsaleexist(string InvoiceNo)
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
        private bool employeeexist(string employeeid)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Employees WHERE EmployeeId = @EmployeeId", con);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeid);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        private bool SalesInBa(string InvoiceNo, string BarCode)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Sales WHERE InvoiceNo = @InvoiceNo and BarCode = @BarCode", con);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                cmd.Parameters.AddWithValue("@BarCode", BarCode);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private bool onlyinvoice(string InvoiceNo)
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

        protected void Add_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                int currstkqty, stkqty, qty;
                if (string.IsNullOrEmpty(BarCode.Text)||
                    string.IsNullOrEmpty(Qty.Text)||
                    string.IsNullOrEmpty(Price.Text))
                {
                    ErrorLabel.Text = "BarCode and Qty cannot be Empty";
                }
                else if (invoiceexist(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Click New";
                }
                else if (StockQty.Text == "0")
                {
                    ErrorLabel.Text = "Not in Stock";
                }
                else if (StockQty.Text == "0")
                {
                    Currstkqty.Text = "0";
                }
                else if (int.Parse(Qty.Text) > int.Parse(StockQty.Text))
                {
                    ErrorLabel.Text = "Invalid Qty...Only" + StockQty.Text + "pieces left";
                }              
                else if (Qty.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "Qty cannot have letters";
                }
                else if (Disc.Text.Any(c => !char.IsDigit(c) && c != '.'))
                {
                    ErrorLabel.Text = "Disc cannot have letters";
                }
                else if (!(int.TryParse(Currstkqty.Text, out currstkqty) && int.TryParse(StockQty.Text, out stkqty) && int.TryParse(Qty.Text, out qty) &&
                          currstkqty == (stkqty - qty)))
                {
                    ErrorLabel.Text = "Press enter on Qty";
                }
                
                else
                {
                    SqlCommand comm = new SqlCommand("Insert into Sales values (@InvoiceNo, @BarCode, @Description, @RetailPrice, @Qty, @Price, @Disc, @Date)", Det);
                    comm.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                    comm.Parameters.AddWithValue("@BarCode", BarCode.Text);
                    comm.Parameters.AddWithValue("@Description", Description.Text);
                    comm.Parameters.AddWithValue("@RetailPrice", Retail.Text);
                    comm.Parameters.AddWithValue("@Qty", 1);
                    comm.Parameters.AddWithValue("@Price", Price.Text);
                    comm.Parameters.AddWithValue("@Disc", Disc.Text);
                    comm.Parameters.AddWithValue("@Date", DateTime.Now);
                    
                    for (int i = 0; i < qty; i++)
                    {
                        comm.ExecuteNonQuery(); 
                    }

                    SqlCommand cmd = new SqlCommand("Select BarCode, Description, RetailPrice, Qty, Disc, Price from Sales where InvoiceNo = @InvoiceNo ORDER BY Date DESC", Det);
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                    cmd.ExecuteNonQuery();

                    Salesgrid.Visible = true;
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);

                    Salesgrid.DataSource = ds;
                    Salesgrid.DataBind();

                    SqlCommand cmmd = new SqlCommand("SELECT SUM(Price) AS TotalPrice FROM Sales WHERE InvoiceNo = @InvoiceNo", Det);
                    cmmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);

                    using (SqlDataReader reader = cmmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TotalPrice.Text = reader["TotalPrice"].ToString();
                            
                        }
                        else
                        {
                            ErrorLabel.Text = "No data found for the given BarCode.";
                        }
                    }                   

                    SqlCommand kmd = new SqlCommand("Update Stocks set StockQty = @StockQty, SoldQty = SoldQty + @Qty where BarCode = @BarCode", Det);
                    kmd.Parameters.AddWithValue("@StockQty", Currstkqty.Text);
                    kmd.Parameters.AddWithValue("@BarCode", BarCode.Text);
                    kmd.Parameters.AddWithValue("@Qty", Qty.Text);
                    kmd.ExecuteNonQuery();

                    ErrorLabel.Text = "Added!";
                    BarCode.Text = "";
                    Description.Text = "";
                    Qty.Text = "";
                    Price.Text = "";
                    Retail.Text = "";
                    Disc.Text = "";
                    StockQty.Text = "";
                    Currstkqty.Text = "";
                    Save.Enabled = true;
                    Save.Style["background-color"] = null;
                    Save.Style["border"] = null;
                    InvoiceNo.Enabled = false;

                }
                
            }

            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
                    
        }
        private void IncrementInvoiceNumber()
        {
            long Inv = long.Parse(InvoiceNo.Text);
            long incrementedValue = (long)(Inv + 1);
            InvoiceNo.Text = incrementedValue.ToString();
        }

        protected void New_Click(object sender, EventArgs e)
        {
            IncrementInvoiceNumber();
        }

        protected void BarCode_TextChanged(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(BarCode.Text))
                {
                    ErrorLabel.Text = "Enter Barcode";
                }
                else if (!barcodeexist(BarCode.Text))
                {
                    ErrorLabel.Text = "Incorrect Barcode";
                }
                else
                {
                    
                    SqlCommand cmd = new SqlCommand("Select Description, RetailPrice, Disc from Stocks where Barcode = @BarCode", Det);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Description.Text = reader["Description"].ToString();
                            Retail.Text = reader["RetailPrice"].ToString();
                            Price.Text = reader["RetailPrice"].ToString();
                            Disc.Text = reader["Disc"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "No data found for the given BarCode.";
                        }
                    }

                    SqlCommand cmmd = new SqlCommand("Select StockQty from Stocks where BarCode = @BarCode", Det);
                    cmmd.Parameters.AddWithValue("@BarCode", BarCode.Text);

                    using (SqlDataReader reader = cmmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            StockQty.Text = reader["StockQty"].ToString();
                        }
                        else
                        {
                            ErrorLabel.Text = "No data found for the given BarCode.";
                        }
                    }

                    Qty.Text = "1";
                    Qty_TextChanged(null, EventArgs.Empty);
                    Qty.Focus();
                    
                }
                
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred" + ex.Message;
            }
            Det.Close();
        }

        protected void Qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.TryParse(Retail.Text, out decimal rp) &&
                    decimal.TryParse(Disc.Text, out decimal dis) &&
                    decimal.TryParse(TotalPrice.Text, out decimal mtp) &&
                    int.TryParse(Qty.Text, out int qt) &&
                    int.TryParse(StockQty.Text, out int sq))
                {
                    if (Qty.Text.Any(char.IsLetter))
                    {
                        ErrorLabel.Text = "Qty cannot have letters";
                    }
                    else if (int.Parse(Qty.Text) > int.Parse(StockQty.Text))
                    {
                        ErrorLabel.Text = "Invalid Qty...Only " + StockQty.Text + " pieces left";
                    }
                    else if (StockQty.Text == "0")
                    {
                        Currstkqty.Text = "0";
                    }
                    else if (dis == 0.00m)
                    {
                        decimal tp = 1 * rp;
                        Price.Text = tp.ToString();

                        int cursqty = sq - qt;
                        Currstkqty.Text = cursqty.ToString();
                    }
                    else
                    {
                        decimal tp = 1 * rp - (1 * rp * dis / 100);
                        Price.Text = tp.ToString();

                        int cursqty = sq - qt;
                        Currstkqty.Text = cursqty.ToString(); 
                    }

                    Add.Focus();
                }
                else
                {
                    ErrorLabel.Text = "Invalid input. Please enter valid numeric values.";
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(EmployeeId.Text) || string.IsNullOrEmpty(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Enter Employee Id";
                }
                else if (!employeeexist(EmployeeId.Text))
                {
                    ErrorLabel.Text = "Employee dosen't exist";
                }
                else if (MobileNo.Text.Length > 15)
                {
                    ErrorLabel.Text = "Invalid Mobile No. Enter number less than 15 Digits";
                }
                else if (invoiceexist(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Click New";
                }
                else if (!MobileNo.Text.Replace(" ", "").All(c => char.IsDigit(c) || c == '+'))
                {
                    ErrorLabel.Text = "Invalid Mobile No.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into InvoiceSale values (@InvoiceNo, @EmployeeId, @MobileNo, @TotalInvoice, @Date)", Det);
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId.Text);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo.Text);
                    cmd.Parameters.AddWithValue("@TotalInvoice", TotalPrice.Text);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    TotalPrice.Text = "";
                    MobileNo.Text = "";
                    IncrementInvoiceNumber();
                    ErrorLabel.Text = "Purchase done!!";
                    Salesgrid.Visible = false;
                    BarCode.Text = "";
                    Description.Text = "";
                    Retail.Text = "";
                    Disc.Text = "";
                    TotalPrice.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }

            Det.Close();
        }

        protected void Delete_entry_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (!onlyinvoice(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Invalid Invoice manually enter Invoice";
                }

                else if (!SalesInBa(InvoiceNo.Text, BarCode.Text))
                {
                    ErrorLabel.Text = "Invalid Invoice or Barcode";
                }
                
                else if (string.IsNullOrEmpty(BarCode.Text))
                {
                    ErrorLabel.Text = "Enter Barcode";
                }
                else
                {
                    int qtyToDelete = Convert.ToInt32(Qty.Text); 
                    int rowsInSales = GetRowCountInSales(); 

                    if (qtyToDelete > rowsInSales)
                    {
                        ErrorLabel.Text = "Invalid quantity: Enter a number less than " + rowsInSales;
                    }
                    else
                    {
                        SqlCommand cmdDelete = new SqlCommand($"DELETE TOP ({qtyToDelete}) FROM Sales WHERE BarCode = @BarCode AND InvoiceNo = @InvoiceNo", Det);
                        cmdDelete.Parameters.AddWithValue("@Barcode", BarCode.Text);
                        cmdDelete.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);

                        int rowsDeleted = cmdDelete.ExecuteNonQuery(); 

                        if (rowsDeleted > 0)
                        {
                            SqlCommand cmdUpdateStocks = new SqlCommand("UPDATE Stocks SET StockQty = StockQty + @QtyToDelete, SoldQty = SoldQty - @QtyToDelete WHERE Barcode = @Barcode", Det);
                            cmdUpdateStocks.Parameters.AddWithValue("@QtyToDelete", qtyToDelete);
                            cmdUpdateStocks.Parameters.AddWithValue("@Barcode", BarCode.Text);
                            cmdUpdateStocks.ExecuteNonQuery();
                        }

                        InvoiceNo.Enabled = false;
                        ErrorLabel.Text = rowsDeleted > 0 ? $"Deleted {rowsDeleted} entries" : "No rows deleted.";
                        Salesgrid.Visible = true;

                        SqlCommand ced = new SqlCommand("SELECT SUM(Price) AS TotalPrice FROM Sales WHERE InvoiceNo = @InvoiceNo", Det);
                        ced.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);

                        using (SqlDataReader reader = ced.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TotalPrice.Text = reader["TotalPrice"].ToString();

                            }
                            else
                            {
                                ErrorLabel.Text = "No data found for the given BarCode.";
                            }
                        }

                        SqlCommand cmmd = new SqlCommand("SELECT BarCode, Description, RetailPrice, Qty, Price FROM Sales WHERE InvoiceNo = @InvoiceNo ORDER BY Date DESC", Det);
                        cmmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                        cmmd.ExecuteNonQuery();
                        SqlDataAdapter ad = new SqlDataAdapter(cmmd);
                        DataSet ds = new DataSet();
                        ad.Fill(ds);

                        Salesgrid.DataSource = ds;
                        Salesgrid.DataBind();

                        BarCode.Text = "";
                        Description.Text = "";
                        Qty.Text = "";
                        Retail.Text = "";
                        Disc.Text = "";
                        Price.Text = "";

                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void Void_transaction_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                InvoiceNo.Enabled = !InvoiceNo.Enabled;

                if (InvoiceNo.Enabled)
                {
                    ErrorLabel.Text = "Enabled!";
                }
                else if (!invoiceinsaleexist(InvoiceNo.Text))
                {
                    ErrorLabel.Text = "Invoice dosen't exist";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Delete from Sales where InvoiceNo = @InvoiceNo", Det);                    
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo.Text);
                    cmd.ExecuteNonQuery();

                    InvoiceNo.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void Void_Earlier_Entry_Click(object sender, EventArgs e)
        {
            InvoiceNo.Enabled = true;
        }
    }
}