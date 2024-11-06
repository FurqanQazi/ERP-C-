<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveForm.aspx.cs" Inherits="ERPproject.ReceiveForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receive Goods</title>
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
      <div class="head1"><h1>Receive Order</h1></div>
        
        <div class="receive-text">
            <table>
                <tr>
                    <td style="visibility:hidden;"><asp:TextBox class="text" ID="StockQty" runat="server" Text="0" Width="100px" ></asp:TextBox></td>
             
                    <td style="visibility:hidden;"><asp:TextBox class="text" ID="newstkqty" runat="server" Text="0" Width="100px" ></asp:TextBox></td>

                    <td>Received PO: </td>
                    <td><asp:TextBox class="text" ID="ReceivedPO" runat="server" Text="0" Width="100px" ></asp:TextBox></td>                        
                </tr>
            </table>
        </div>
        
         <div class="err-lab">
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </div>
        <div class="table-con">
            <div class="tab">
                <table>
                    <tr height="50px">
                        <td>PO Number:</td>
                        <td><asp:TextBox class="text" ID="PONumber" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Invoice No.</td>
                        <td><asp:TextBox class="text" ID="InvoiceNumber" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Receiving No. </td>
                        <td><asp:TextBox class="text" ID="ReceivingNo" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Vendor Code:</td>
                        <td><asp:TextBox class="text" ID="VendorCode" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Ref No.:</td>
                        <td><asp:TextBox class="text" ID="RefNo" Width="208px" runat="server"></asp:TextBox>&nbsp;&nbsp; </td>
                        <td><asp:Button runat="server" ID="Get" height="30px" BackColor="#66ccff" Text="Get" OnClick="Get_Click"/><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Bar Code:</td>
                        <td><asp:TextBox class="text" ID="BarCode" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Vendor Name:</td>
                        <td><asp:TextBox class="text" ID="VendorName" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Description:</td>
                        <td><asp:TextBox class="area" ID="Description" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                </table>
            </div>
            <div class="tab">
                <table>
                    <tr height="50px">
                        <td>Order Qty:</td>
                        <td><asp:TextBox class="text" Width="208px" ID="OrderQty" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Receive Qty:</td>
                        <td><asp:TextBox class="text" ID="ReceiveQty" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Pending Qty:</td>
                        <td><asp:TextBox class="text" ID="PendingQty" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                </table>
            </div>
            <div class="tab">
                <table>
                    <tr height="50px">
                        <td>Unit Price:</td>
                        <td><asp:TextBox class="text" ID="UnitPrice" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Coeff:</td>
                        <td><asp:TextBox class="text" ID="Coeff" Width="208px" runat="server"></asp:TextBox>&nbsp;&nbsp; </td>
                        <td><asp:Button runat="server" ID="Calc" height="30px" BackColor="#ffff99" Text="Calc" OnClick="Calc_Click" /><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Retail Price:</td>
                        <td><asp:TextBox class="text" ID="RetailPrice" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                    <tr height="50px">
                        <td>Total Invoice:</td>
                        <td><asp:TextBox class="text" ID="TotalInvoice" Width="208px" runat="server"></asp:TextBox><br /></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="btn-con">
            <asp:Button ID="Save" runat="server" CssClass="mybttn" Text="Receive Order" width="138px" OnClick="Save_Click"/>
            <asp:Button ID="Delete" runat="server" Text="Delete Record" width="138px" CssClass="mybttn" OnClick="Delete_Click"/>
            <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="mybttn" OnClick="Clear_Click"/>                    
        </div>
    </form>
</body>
</html>
