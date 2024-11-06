<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stocks.aspx.cs" Inherits="ERPproject.Stocks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stocks</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
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
                                            <a href="Employee.aspx">Employee Form &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="EmployeeView.aspx">Employee View</a>
                                                </li>
                                            </ul>
                                        </li>                                  
                                        <li class="dropdown-item">
                                            <a href="#">Stocks Form &raquo;</a>
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
                                        <li class="dropdown-item"><a href="Sales.aspx">Sales</a></li>
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
      <div class="head1"><h1>Stocks!</h1></div>
        <div class="err-lab">
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </div>
        <section class="table-con">
            <div class="tab">
                <table>
                    <tr height="50px">
                        <td>Vendor Code:</td>
                        <td><asp:TextBox class="text" ID="VendorCode" runat="server" Width="208px"></asp:TextBox><br /></td>  
                        <td><asp:Button runat="server" ID="Get" height="30px" BackColor="#66ccff" Text="Get" OnClick="Get_Click" /></td>
                    </tr>
                    <tr height="50px">
                        <td>VendorName:</td>
                        <td><asp:TextBox class="text" ID="VendorName" runat="server" Width="208px"></asp:TextBox><br /></td>                
                    </tr>
                    <tr height="50px">
                        <td>Ref No.:</td>
                        <td><asp:TextBox class="text" ID="RefNo" runat="server" Width="208px"></asp:TextBox><br /></td>                
                    </tr>
                    <tr height="50px">
                        <td>Unit Price:</td>
                        <td><asp:TextBox class="text" ID="UnitPrice" runat="server" Width="208px"></asp:TextBox><br /></td>                
                    </tr>
                    <tr height="50px">
                        <td>Stock Qty:</td>
                        <td><asp:TextBox class="text" ID="StockQty" runat="server" Width="208px"></asp:TextBox><br /></td>                
                    </tr>
                </table>
            </div>

            <div class="tab">
                <table>
                    <tr height="50px">
                        <td>BarCode:</td>
                        <td><asp:TextBox class="text" ID="BarCode" runat="server" Width="208px"></asp:TextBox><br /></td>                
                    </tr>
                    <tr height="50px">
                        <td>Retail Price:</td>
                        <td><asp:TextBox class="text" ID="RetailPrice" runat="server" Width="208px"></asp:TextBox><br /></td>                
                    </tr>
                    <tr height="50px">
                        <td>SoldQty: </td>
                        <td><asp:TextBox class="text" ID="SoldQty" runat="server" Width="208px"></asp:TextBox><br /></td>                
                    </tr>
                </table>
            </div>
            <div class="tab">
                <table>
                    <tr>
                        <td>Description:</td>
                        <td>
                            <asp:TextBox id="Descrip" runat="server" Width="208px" class="area"></asp:TextBox>
                        </td>
                    </tr>
                    <tr height="50px">
                        <td>Disc:</td>
                        <td>
                            <asp:TextBox id="Disc" runat="server" Width="208px" class="text"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </section>
        <div class="btn-con">
            <asp:Button ID="Save" runat="server" CssClass="mybttn" Text="Save" OnClick="Save_Click"/>
            <asp:Button ID="Add" runat="server" Text="Add" CssClass="mybttn" OnClick="Add_Click"/>
            <asp:Button ID="Stock" runat="server" Text="Stock" CssClass="mybttn" OnClick="Stock_Click"/>
            <asp:Button ID="Disc_add" runat="server" Text="Add Disc" CssClass="mybttn" OnClick="Disc_add_Click"/> 
            <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="mybttn" OnClick="Clear_Click"/>                    
         </div>


    </form>
</body>
</html>
