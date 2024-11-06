<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseForm.aspx.cs" Inherits="ERPproject.PurchaseForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Goods</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
</head>
<body background="MyImages/Background.jpg">
    <form id="form1" runat="server">
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
                                            <a href="Employee.aspx">Employee Form &raquo;</a>
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
      <div class="head1"><h1>Purchase Order</h1></div>
        <div class="err-lab">
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </div>
        <div class="table-con">
            <div class="tab">
                <table>
                    <tr height="50px">
                        <td>P.O Number:</td>
                        <td><asp:TextBox class="text" ID="PONumber" runat="server" Width="208px" ></asp:TextBox>&nbsp;&nbsp; <asp:Button runat="server" ID="New" height="30px" BackColor="#66ccff" Text="New" OnClick="New_Click" /><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Vendor Code: </td>
                        <td><asp:TextBox class="text" ID="VendorCode" runat="server" Width="208px" OnTextChanged="VendorCode_TextChanged" ></asp:TextBox>&nbsp;&nbsp; <asp:Button runat="server" ID="VenEn" height="30px" BackColor="#66ccff" Text="Con.." OnClick="VenEn_Click" /><br /></td>
                        
                    </tr>
                    <tr height="50px">
                        <td>Ref No.</td>
                        <td><asp:TextBox class="text" ID="RefNo" runat="server" Width="208px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="get" height="30px" BackColor="#66ccff" Text="Get" OnClick="get_Click" /></td>
                        
                    </tr>
                    <tr height="50px">
                        <td>BarCode:</td>
                        <td><asp:TextBox class="text" ID="BarCode" runat="server" Width="208px" ></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Description:</td>
                        <td><asp:TextBox class="area" ID="Description" runat="server" Width="208px" ></asp:TextBox><br /></td>
                    </tr>
                    
                </table>
            </div>
            <div class="tab">
                <table>
                    <tr height="50px">
                        <td>Unit Price:</td>
                        <td><asp:TextBox class="text" ID="UnitPrice" runat="server" Width="208px" ></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Vendor Name:</td>
                        <td><asp:TextBox class="text" ID="VendorName" runat="server" Width="208px" ></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Order Qty:</td>
                        <td><asp:TextBox class="text" ID="OrderQty" runat="server" Width="208px" ></asp:TextBox><br /></td>
                        <td><asp:Button runat="server" ID="calc" height="30px" BackColor="#ffff99" Text="Calc" OnClick="calc_Click" /></td>
                    </tr>
                    <tr height="50px">
                        <td>Total Price:</td>
                        <td><asp:TextBox class="text" ID="TotalPrice" runat="server" Width="208px" ></asp:TextBox><br /></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="btn-con">
            <asp:Button ID="Save" runat="server" CssClass="mybttn" Text="Purchase Order" width="138px" OnClick="Save_Click"/>
            <asp:Button ID="Edit" runat="server" Text="Edit" CssClass="mybttn" OnClick="Edit_Click"/>
            <asp:Button ID="Update" runat="server" Text="Update" CssClass="mybttn" OnClick="Update_Click"/>
            <asp:Button ID="Delete" runat="server" Text="Delete Order" width="130px" CssClass="mybttn" OnClick="Delete_Click1"/>    
            <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="mybttn" OnClick="Clear_Click"/>
        </div>
    </form>
</body>
</html>
