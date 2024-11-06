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
    public partial class Employee : System.Web.UI.Page
    {

        private readonly SqlConnection Det = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1 ;Password=1234;");

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        private bool EmployeeIdExists(string employeeId)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-U47QFCME\MSSQLSERVER03;Database=ERP; User Id=sa1;Password=1234;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Employees WHERE EmployeeId = @EmployeeId", con);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        private string ConvertDateToString(object dateValue)
        {
            if (dateValue != DBNull.Value && DateTime.TryParse(dateValue.ToString(), out DateTime date))
            {
                return date.ToString("yyyy-MM-dd");
            }

            return string.Empty;
        }


        protected void Save_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                decimal basicPay, housing, totalSalary;
                DateTime dateOfBirth;
                DateTime dateOfHire;

                if (
                    string.IsNullOrEmpty(Employeeid.Text) ||
                    string.IsNullOrEmpty(Name.Text) ||
                    string.IsNullOrEmpty(DateofBirth.Text) ||
                    string.IsNullOrEmpty(Nationality.Text) ||
                    string.IsNullOrEmpty(PhoneNo.Text) ||
                    string.IsNullOrEmpty(Email.Text) ||
                    Position.SelectedValue == "1" || 
                    Sex.SelectedValue == "1" ||
                    Empstatus.SelectedValue == "1"||
                    string.IsNullOrEmpty(Dateofhire.Text) ||
                    string.IsNullOrEmpty(BasicPay.Text) ||
                    string.IsNullOrEmpty(Housing.Text) ||
                    string.IsNullOrEmpty(TotalSalary.Text)
                    )
                {
                    ErrorLabel.Text = "Please fill all the fields.";
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
                else if (EmployeeIdExists(Employeeid.Text))
                {
                    ErrorLabel.Text = "Employee with the specified ID already exist.";
                }

                else if (!(decimal.TryParse(BasicPay.Text, out basicPay) && decimal.TryParse(Housing.Text, out housing) && decimal.TryParse(TotalSalary.Text, out totalSalary) &&
                           totalSalary == (basicPay + housing)))
                {
                    ErrorLabel.Text = "Total Salary must be the result of Basic Pay + Housing. Try Calc";
                }
                else if (!BasicPay.Text.All(c => char.IsDigit(c) || c == '.') || !Housing.Text.All(c => char.IsDigit(c) || c == '.'))
                {
                    ErrorLabel.Text = "Basic Pay and Housing cannot contain letters.";
                }
                else if (Employeeid.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "EmployeeId cannot contain letters.";
                    return;
                }

                else if (!DateTime.TryParse(DateofBirth.Text, out dateOfBirth))
                {
                    ErrorLabel.Text = "Invalid Date of Birth format";
                    return;
                }

                
                else if (!DateTime.TryParse(Dateofhire.Text, out dateOfHire))
                {
                    ErrorLabel.Text = "Invalid Date of Hire format";
                    return;
                }


                else
                {

                    SqlCommand Nor = new SqlCommand("INSERT INTO Employees VALUES (@EmployeeId, @Name, @DateofBirth, @Sex, @Nationality, @PhoneNo, @Email, @Position, @Empstatus, @Dateofhire, @BasicPay, @Housing, @TotalSalary, @CurrentDateTime)", Det);
                                       
                    Nor.Parameters.AddWithValue("@EmployeeId", Employeeid.Text);
                    Nor.Parameters.AddWithValue("@Name", Name.Text);
                    Nor.Parameters.AddWithValue("@DateofBirth", dateOfBirth);
                    Nor.Parameters.AddWithValue("@Sex", Sex.SelectedValue);
                    Nor.Parameters.AddWithValue("@Nationality", Nationality.Text);
                    Nor.Parameters.AddWithValue("@PhoneNo", PhoneNo.Text);
                    Nor.Parameters.AddWithValue("@Email", Email.Text);
                    Nor.Parameters.AddWithValue("@Position", Position.SelectedValue);
                    Nor.Parameters.AddWithValue("@Empstatus", Empstatus.SelectedValue);
                    Nor.Parameters.AddWithValue("@Dateofhire", dateOfHire);
                    Nor.Parameters.AddWithValue("@BasicPay", basicPay);
                    Nor.Parameters.AddWithValue("@Housing", housing);
                    Nor.Parameters.AddWithValue("@TotalSalary", totalSalary);
                    Nor.Parameters.AddWithValue("@CurrentDateTime", DateTime.Now); 
                    
                    Nor.ExecuteNonQuery();

                    Clearform();
                    ErrorLabel.Text = "Employee Added!";
                    
                }
                

            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }
        private void Clearform()
        {
            Employeeid.Text = "";
            Name.Text = "";
            DateofBirth.Text = "";
            Sex.ClearSelection();
            Sex.Items.FindByValue("1").Selected = true;
            Nationality.Text = "";
            PhoneNo.Text = "";
            Email.Text = "";
            Dateofhire.Text = "";
            BasicPay.Text = "";
            Housing.Text = "";
            TotalSalary.Text = "";
            Empstatus.ClearSelection();
            Empstatus.Items.FindByValue("1").Selected = true;
            Position.ClearSelection();
            Position.Items.FindByValue("1").Selected = true;
        }

        protected void Calc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(BasicPay.Text) || string.IsNullOrEmpty(Housing.Text))
                {
                    ErrorLabel.Text = "Please fill both the fields!";
                }
                else if (!BasicPay.Text.All(c => char.IsDigit(c) || c == '.') || !Housing.Text.All(c => char.IsDigit(c) || c == '.'))
                {
                    ErrorLabel.Text = "Basic Pay and Housing cannot contain letters.";
                }
                else if (!BasicPay.Text.All(c => char.IsDigit(c) || (c == '.' && BasicPay.Text.Count(ch => ch == '.') == 1))
                 || !Housing.Text.All(c => char.IsDigit(c) || (c == '.' && Housing.Text.Count(ch => ch == '.') == 1)))
                {
                    ErrorLabel.Text = "Basic Pay and Housing cannot contain letters or more than one decimal point.";
                }
                else
                {
                    decimal BP = decimal.Parse(BasicPay.Text);
                    decimal HO = decimal.Parse(Housing.Text);
                    decimal TS = BP + HO;
                    TotalSalary.Text = TS.ToString();
                }
            }

            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: "+ ex.Message;
            }
            
            
                     
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(Employeeid.Text))
                {
                    ErrorLabel.Text = "Please Enter Employee Id to retrieve Data";
                }

                else if (!EmployeeIdExists(Employeeid.Text))
                {
                    ErrorLabel.Text = "Employee with the specified ID does not exist.";
                }
                else
                {
                    SqlCommand exc = new SqlCommand("select* from Employees where EmployeeId = @EmployeeId", Det);

                    exc.Parameters.AddWithValue("@EmployeeId", Employeeid.Text);
                    exc.ExecuteNonQuery();

                    SqlDataAdapter ad = new SqlDataAdapter(exc);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Name.Text = ds.Tables[0].Rows[i]["Name"].ToString();
                            DateofBirth.Text = ConvertDateToString(ds.Tables[0].Rows[i]["Date_of_birth"]);
                            string selectedGender = ds.Tables[0].Rows[i]["Gender"].ToString();
                            Sex.SelectedValue = Sex.Items.FindByText(selectedGender)?.Value;
                            Nationality.Text = ds.Tables[0].Rows[i]["Nationality"].ToString();
                            PhoneNo.Text = ds.Tables[0].Rows[i]["PhoneNumber"].ToString();
                            Email.Text = ds.Tables[0].Rows[i]["Email"].ToString();
                            string selectedPosition = ds.Tables[0].Rows[i]["Designation"].ToString();
                            Position.SelectedValue = Position.Items.FindByText(selectedPosition)?.Value;
                            string selectedStatus = ds.Tables[0].Rows[i]["Status"].ToString();
                            Empstatus.SelectedValue = Empstatus.Items.FindByText(selectedStatus)?.Value;
                            Dateofhire.Text = ConvertDateToString(ds.Tables[0].Rows[i]["HireDate"]);
                            BasicPay.Text = ds.Tables[0].Rows[i]["BasicPay"].ToString();
                            Housing.Text = ds.Tables[0].Rows[i]["Housing"].ToString();
                            TotalSalary.Text = ds.Tables[0].Rows[i]["Salary"].ToString();


                        }
                    }
                    Save.Enabled = false;
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
                decimal basicPay, housing, totalSalary;

                
                if (
                     string.IsNullOrEmpty(Employeeid.Text) ||
                     string.IsNullOrEmpty(Name.Text) ||
                     string.IsNullOrEmpty(DateofBirth.Text) ||
                     string.IsNullOrEmpty(Nationality.Text) ||
                     string.IsNullOrEmpty(PhoneNo.Text) ||
                     string.IsNullOrEmpty(Email.Text) ||
                     Position.SelectedValue == "1" ||
                     Sex.SelectedValue == "1" ||
                     Empstatus.SelectedValue == "1" ||
                     string.IsNullOrEmpty(Dateofhire.Text) ||
                     string.IsNullOrEmpty(BasicPay.Text) ||
                     string.IsNullOrEmpty(Housing.Text) ||
                     string.IsNullOrEmpty(TotalSalary.Text)
                     )
                {
                    ErrorLabel.Text = "Please fill all the fields.";
                }
                else if (!(decimal.TryParse(BasicPay.Text, out basicPay) && decimal.TryParse(Housing.Text, out housing) && decimal.TryParse(TotalSalary.Text, out totalSalary) &&
                           totalSalary == (basicPay + housing)))
                {
                    ErrorLabel.Text = "Total Salary must be the result of Basic Pay + Housing. Try Calc";
                }
                else if (!EmployeeIdExists(Employeeid.Text))
                {
                    ErrorLabel.Text = "Employee with the specified ID does not exist.";
                }
                else if (!BasicPay.Text.All(c => char.IsDigit(c) || c == '.') || !Housing.Text.All(c => char.IsDigit(c) || c == '.'))
                {
                    ErrorLabel.Text = "Basic Pay and Housing cannot contain letters.";
                }
                else if (!PhoneNo.Text.Replace(" ", "").All(c => char.IsDigit(c) || c == '+'))
                {
                    ErrorLabel.Text = "Invalid Phone no.";
                }
                else if (!Email.Text.Contains("@") || !Email.Text.Contains("."))
                {
                    ErrorLabel.Text = "Invalid Email";
                }
                else if (PhoneNo.Text.Length > 15)
                {
                    ErrorLabel.Text = "Invalid Phone No.. Enter number less than 15 Digits";
                }
                else if (Employeeid.Text.Any(char.IsLetter))
                {
                    ErrorLabel.Text = "EmployeeId cannot contain letters.";
                    return;
                }
                else
                {

                    SqlCommand mnd = new SqlCommand("UPDATE Employees SET Name=@Name, Date_of_birth=@DateOfBirth, Gender=@Sex, Nationality=@Nationality, PhoneNumber=@PhoneNo, Email=@Email, Designation=@Position, Status=@Empstatus, HireDate=@DateOfHire, BasicPay=@BasicPay, Housing=@Housing, Salary=@TotalSalary WHERE EmployeeId=@EmployeeId", Det);
                                      
                    mnd.Parameters.AddWithValue("@EmployeeId", Employeeid.Text);
                    mnd.Parameters.AddWithValue("@Name", Name.Text);
                    mnd.Parameters.AddWithValue("@DateOfBirth", DateofBirth.Text);
                    mnd.Parameters.AddWithValue("@Sex", Sex.SelectedValue);
                    mnd.Parameters.AddWithValue("@Nationality", Nationality.Text);
                    mnd.Parameters.AddWithValue("@PhoneNo", PhoneNo.Text);
                    mnd.Parameters.AddWithValue("@Email", Email.Text);
                    mnd.Parameters.AddWithValue("@Position", Position.SelectedValue);
                    mnd.Parameters.AddWithValue("@Empstatus", Empstatus.SelectedValue);
                    mnd.Parameters.AddWithValue("@DateOfHire", Dateofhire.Text);
                    mnd.Parameters.AddWithValue("@BasicPay", basicPay);
                    mnd.Parameters.AddWithValue("@Housing", housing);
                    mnd.Parameters.AddWithValue("@TotalSalary", totalSalary);

                    mnd.ExecuteNonQuery();

                    Clearform();

                    ErrorLabel.Text = "Employee Details Updated!";

                    Save.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
            Det.Close();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Det.Open();
            try
            {
                if (string.IsNullOrEmpty(Employeeid.Text))
                {
                    ErrorLabel.Text = "Please Enter Employee Id to Delete Data";
                }
                else if (!EmployeeIdExists(Employeeid.Text))
                {
                    ErrorLabel.Text = "Employee with the specified ID does not exist.";
                }
                else
                {
                    SqlCommand jkl = new SqlCommand("delete from Employees where Employeeid =" + Employeeid.Text, Det);
                    jkl.ExecuteNonQuery();

                    ErrorLabel.Text = "Employee Details Deleted Successfully!";
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