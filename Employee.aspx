<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="ERPproject.Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Details</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <script type="text/javascript">
        function disbleFutureDates() {
            var currentDate = new Date();
            var selectedDate = new Date(document.getElementById('Dateofhire.ClientID').value);
            var newdate = new Date(document.getElementById('DateofBirth.ClientID').value);

        if (selectedDate > currentDate) {
            document.getElementById('Dateofhire.ClientID').value = '';
                alert('Please select a date that is not in the future.');
            }
        else if (newdate > currentDate) {
            document.getElementById('DateofBirth.ClientID').value = '';
            alert('Please select a Birth date that is not in the future.');
        }
        }
    </script>
</head>
<body background="MyImages/Background.jpg">
    <form id="form1" autocomplete="off" runat="server">
        <section>          
            <div class="Head-con">
                <h1>THE ERP SOFTWARE</h1>
            </div>
        </section>
        <div class="nav-con">
            <div class="nav-div1">
                <h3><asp:Label ID="UserLogin" runat="server" Text="ERP Things"></asp:Label></h3>
            </div>

            <nav class="navbar navbar-expand-sm sticky-top">
                
                <div class="container-fluid">
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#collapsibleNavbar">  
                    </button>
                    <div class="collapse navbar-collapse" id="collapsibleNavbar">
                        <ul class="navbar-nav">
                                <li class="nav-item">
                                  <a class="nav-link" href="HomeLogin.aspx">Home</a>
                               </li>
                               <li class="nav-item dropdown"> 
                                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                      <span class="caret"></span>
                                       Tools
                                    </a>
                                    <ul class="dropdown-menu" style="background-color:  rgba(76, 110, 115);" aria-labelledby="navbarDropdown"> 
                                        <li class="dropdown-item">
                                            <a href="#">Employee Form &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="EmployeeView.aspx">Employee View</a>
                                                </li>
                                            </ul>
                                        </li> 
                                        <li class="dropdown-item">
                                            <a href="Stocks.aspx">Stocks Form &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="StocksView.aspx">Stocks View</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="dropdown-item">
                                            <a href="Suppliers.aspx">Supplier Form &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="SuppliersView.aspx">Supplier View</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="dropdown-item">
                                            <a href="PurchaseForm.aspx">Purchase Goods &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="PurchaseView.aspx">Purchase View</a>
                                                </li>
                                                 <li>
                                                   <a class="dropdown-item" href="PurchaseReport.aspx">Purchase Report</a>
                                                 </li>
                                            </ul>
                                        </li>           
                                        <li class="dropdown-item">
                                            <a href="ReceiveForm.aspx">Receive Goods &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="ReceiveView.aspx">Receive View</a>
                                                </li>
                                                 <li>
                                                   <a class="dropdown-item" href="#">Receive Report</a>
                                                 </li>
                                            </ul>
                                        </li>
                                        <li class="dropdown-item">
                                            <a href="Sales.aspx">Sales &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="SalesView.aspx">Sales View</a>
                                                </li>
                                            </ul>
                                        </li>
                                   </ul>
                              </li>
                              <li class="nav-item">
                                   <a class="nav-link" href="#fotter">Contact</a>
                              </li>
                               <li class="nav-item">
                                    <a class="nav-link" href="#thrdhead">Performance</a>
                              </li>     
                       </ul>
                         <a  class="btn btn-danger" href="Index.aspx">Logout</a>
                   </div>
              </div>
         </nav>
      </div>
        <div class="head1"><h1>Employee Details!</h1></div>
        <div class="err-lab">
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </div>
        <div class="table-con">

            <div class="tab">
            <table>
                <tr height="50px">
                    <td>Employee ID: </td>
                    <td><asp:TextBox class="text" ID="Employeeid" runat="server"></asp:TextBox> <br /></td>
                </tr>

                <tr height="50px">
                    <td>Name: </td>
                    <td><asp:TextBox class="text" ID="Name" runat="server"></asp:TextBox> <br /></td>
                </tr>
                <tr height="50px">
                    <td>Date-Of-Birth:</td>
                    <td> <asp:TextBox class="text" ID="DateofBirth" TextMode="Date" runat="server" oninput="disbleFutureDates()" ></asp:TextBox> <br /></td>
                </tr>
                <tr height="50px">
                    <td>Gender: </td>
                    <td>
                        <asp:DropDownList class="text" ID="Sex" runat="server">
                            <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        </asp:DropDownList>                    

                    </td>
                </tr>
                <tr height="50px">
                    <td>Nationalitily:</td>
                    <td> <asp:TextBox class="text" ID="Nationality" runat="server"></asp:TextBox> <br /></td>
                </tr>
                <tr height="50px">
                    <td>Phone No.: </td>
                    <td> <asp:TextBox class="text" ID="PhoneNo" runat="server"></asp:TextBox><br /></td>
                </tr>
                <tr height="50px">
                    <td>Email: </td>
                    <td><asp:TextBox class="text" ID="Email" Type="Email" runat="server"></asp:TextBox><br /></td>
                </tr>
                <tr height="50px">
                    <td> Position:</td>
                    <td> 
                        <asp:DropDownList class="text" ID="Position" runat="server" >
                             <asp:ListItem Text="Select" Value ="1"></asp:ListItem>
                             <asp:ListItem Text="HR Manager" Value ="HR Manager"></asp:ListItem>
                            <asp:ListItem Text="IT Technician" Value ="IT Technician"></asp:ListItem>
                            <asp:ListItem Text="Senior Developer" Value ="Senior Delevoper"></asp:ListItem>
                            <asp:ListItem Text="Junior Developer" Value ="Junior Developer"></asp:ListItem>
                            <asp:ListItem Text="DevOps Engineer" Value ="DevOps Engineer"></asp:ListItem>
                            <asp:ListItem Text="Software Architect" Value ="Software Architect"></asp:ListItem>
                             <asp:ListItem Text="Sales Manager" Value ="Sales Manager"></asp:ListItem>
                             <asp:ListItem Text="Purchase Manager" Value ="Purchase Manager"></asp:ListItem>
                             <asp:ListItem Text="Clerk" Value ="Clerk"></asp:ListItem>
                             <asp:ListItem Text="Worker" Value ="Worker"></asp:ListItem>
                         </asp:DropDownList><br />
                    </td>
                </tr>
                <tr height="50px">
                    <td> Hire Date:</td>
                    <td> <asp:TextBox  class="text" ID="Dateofhire" Value="" TextMode="Date" runat="server" oninput="disbleFutureDates()"></asp:TextBox><br /></td>
                </tr>
                
            </table>                
            </div>
            <br />
            <div class="tab">
                <table>
                    <tr height="50px">
                        <td> Status:</td>
                        <td>
                            <asp:DropDownList class="text" ID="Empstatus" runat="server">
                                <asp:ListItem Text="Status" Value="1"></asp:ListItem>      
                                <asp:ListItem Text="Part-time" Value="Part-time"></asp:ListItem>                                
                                <asp:ListItem Text="Full-time" Value="Full-time"></asp:ListItem>
                                <asp:ListItem Text="Remote" Value="Remote"></asp:ListItem>
                            </asp:DropDownList><br />

                        </td>
                    </tr>
                    <tr height="50px">
                        <td> BasicPay: </td>
                        <td><asp:TextBox class="text" ID="BasicPay" runat="server"></asp:TextBox><br /> </td>

                    </tr>

                    <tr height="50px">
                        <td> Housing:</td>
                        <td> <asp:TextBox class="text" ID="Housing" runat="server"></asp:TextBox><br /></td>
                        <td><asp:Button runat="server" height="30px" BackColor="#ffff99" Text="Calc" OnClick="Calc_Click"/></td>
                    </tr>
                    <tr height="50px">
                        <td> Salary:</td>
                        <td> <asp:TextBox class="text" ID="TotalSalary" Enabled="false" runat="server" Width="208px"></asp:TextBox><br /></td>
                    </tr>
                </table>
            </div>
            
        </div>
        <div class="btn-con">
            <asp:Button ID="Save" runat="server" CssClass="mybttn" Text="Save" OnClick="Save_Click"/>
            <asp:Button runat="server" ID="Edit" Text="Edit" CssClass="mybttn" OnClick="Edit_Click"/>
            <asp:Button runat="server" ID="Update" Text="Update" CssClass="mybttn" OnClick="Update_Click"/>
            <asp:Button runat="server" ID="Delete" Text="Delete" CssClass="mybttn" OnClick="Delete_Click"/>                    
         </div>
        
    </form>
</body>
</html>
